using System;
using System.Collections.Generic;
using System.Linq;
using Core.DB;
using Core.Extentions;
using Dapper;

namespace Core.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
#if  DEBUG
        private string ConnectionString { get { return "Data Source=.;Initial Catalog=RadishStackDB;Integrated Security=True;MultipleActiveResultSets=True"; } }
#else
        private string ConnectionString { get { return "Data Source=148.72.232.166;Initial Catalog=RadishStackDB;User Id=radishstack;Password=Radish@AI123;MultipleActiveResultSets=True"; } }
#endif
        protected string BaseColumns = "Id,CreationDate,PublicId,Status,";
        protected string SqlSelect;
        protected string SqlInsert;
        protected string SqlUpdate;
        protected string SqlDelete;
        protected string TableName;
        protected string Columns;

        protected void Init(string table, string columns)
        {
            TableName = table;
            Columns = BaseColumns + columns;
            SqlSelect = "SELECT " + Columns.AddBraces() + " FROM [" + TableName + "]";
            SqlInsert = Columns.GenerateInsertQuery(TableName);
            SqlUpdate = Columns.GenerateUpdateQuery(TableName, "Id", "Id,PublicId,CreationDate");
            SqlDelete = string.Format("UPDATE {0} SET Status=-100 WHERE Id=@Id; ", TableName);
        }

#region common functions: Getall, getbyid, save update insert, delete
        public T Get(Guid id)
        {
            if (string.IsNullOrEmpty(SqlSelect))
                return default(T);
            return Query<T>(SqlSelect + " WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            if (string.IsNullOrEmpty(SqlSelect))
                return default(IEnumerable<T>);
            return Query<T>(SqlSelect);
        }
        public T Get(int id)
        {
            if (string.IsNullOrEmpty(SqlSelect))
                return default(T);
            return Query<T>(SqlSelect + " WHERE PublicId=@Id", new { Id = id }).FirstOrDefault();
        }
        public IEnumerable<T> GetAll(string key, string value)
        {
            if (string.IsNullOrEmpty(SqlSelect))
                return default(IEnumerable<T>);
            return Query<T>(SqlSelect + " WHERE [" + key + "]=@Key", new { Key = value });
        }
        public T Get(string key, string value)
        {
            if (string.IsNullOrEmpty(SqlSelect))
                return default(T);
            return Query<T>(SqlSelect + " WHERE [" + key + "]=@Key", new { Key = value }).SingleOrDefault();
        }
        //public T Get(int id)
        //{
        //    if (string.IsNullOrEmpty(SqlSelect))
        //        return default(T);

        //    return Query<T>(SqlSelect + " WHERE PublicId=@PublicId", new { PublicId = id }).SingleOrDefault();
        //}
        public int GetCount(string where = "")
        {
            if (!string.IsNullOrEmpty(where))
                where = "AND " + where;
            return ExecuteScaler("Select count(*) from " + TableName + " WHERE Status!=-100 " + where);
        }

        public IEnumerable<T> GetAllItems(string where = "", string orderBy = "CreationDate Desc")
        {
            if (!string.IsNullOrEmpty(where))
                where = "AND " + where;
            if (string.IsNullOrEmpty(SqlSelect))
                return default(IEnumerable<T>);

            return
                Query<T>(SqlSelect + " WHERE Status!=-100 " + where + " order by " +orderBy);
        }
        public IEnumerable<T> GetAll(int size, int page = 1, string where = "", string orderBy = "CreationDate Desc", bool excludeDeleted = false)
        {
            if (!string.IsNullOrEmpty(where))
                where = "AND " + where;
            if (string.IsNullOrEmpty(SqlSelect))
                return default(IEnumerable<T>);

            return
                Query<T>(SqlSelect + " WHERE Status!=-100 " + where + string.Format(" ORDER BY {2} OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (page - 1) * size, size,
                             orderBy));
        }

        public Guid Save(T model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                model.CreationDate = DateTime.Now;
                Insert(model);
            }
            else
                Update(model);

            return model.Id;
        }
        private void Insert(IEnumerable<T> models)
        {
            if (SqlInsert.IsNotNullOrEmpty())
                Execute(SqlInsert, models);
        }
        public void Add(T model)
        {
            Insert(model);
        }
        private void Insert(T model)
        {
            if (SqlInsert.IsNotNullOrEmpty())
                Execute(SqlInsert, model);
        }
        public void Update(T model)
        {
            if (SqlUpdate.IsNotNullOrEmpty())
                Execute(SqlUpdate, model);
        }
        public void Delete(Guid id)
        {
            if (SqlDelete.IsNotNullOrEmpty())
                Execute(SqlDelete, new { Id = id });
        }
        public void UpdateColumn(string column, string value, string id)
        {
            Execute("UPDATE " + TableName + " SET " + column + "=@value WHERE Id=@id", new { value, id });
        }

#endregion
#region Dapper access

        public int Execute(string sql, dynamic param = null)
        {
            return ConnectionManager.GetConnection(ConnectionString).Execute(sql, param);
        }
        public int ExecuteScaler(string sql, dynamic param = null)
        {
            return ConnectionManager.GetConnection(ConnectionString).ExecuteScaler(sql, param);
        }


        protected IEnumerable<T> Query<T>(string sql, dynamic param = null)
        {
            var connection = ConnectionManager.GetConnection(ConnectionString);
            return connection.Query<T>(sql, param);
        }
        protected IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null)
        {
            return ConnectionManager.GetConnection(ConnectionString).Query<TFirst, TSecond, TReturn>(sql, map, param);
        }
        protected SqlMapper.GridReader QueryMultiple(string sql, object param = null)
        {
            return ConnectionManager.GetConnection(ConnectionString).QueryMultiple(sql, param);
        }
#endregion
    }
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int PublicId { get; set; }
        public int Status { get; set; }

        public StatusEnum StatusEnum
        {
            get { return (StatusEnum)Status; }
        }
    }
    public class SearchCriteria
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Query { get; set; }
        public string OrderBy { get; set; }
    }
}
