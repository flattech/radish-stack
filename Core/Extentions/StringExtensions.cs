﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Extentions
{
    public static class StringExtensions
    {
        public static T FromJson<T>(this string str1)
        {
            if (string.IsNullOrEmpty(str1))
                return default(T);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str1);
        }

        public static int[] ToIntegers(this string str1)
        {
            if (string.IsNullOrEmpty(str1))
                return null;
            return str1.Split(',')
                        .Select(z => Convert.ToInt32(z)).ToArray();
        }


        public static Guid[] ToGuids(this string str1)
        {
            if (string.IsNullOrEmpty(str1))
                return null;
            return str1.Split(',')
                        .Select(z => Guid.Parse(z)).ToArray();
        }
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        #region Sql
        public static StringBuilder And(this StringBuilder builder, string with)
        {
            builder.Append(" and ").Append(with);
            return builder;
        }

        public static StringBuilder Or(this StringBuilder builder, string with)
        {
            builder.Append(" or ").Append(with);
            return builder;

        }
        public static string And(this string builder, string with)
        {
            return builder + " and " + with;
        }

        public static string Or(this string builder, string with)
        {
            return builder + " or " + with;
        }
        public static string CleanSql(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Replace("'", "''");
        }

        public static string AddPrefix(this string columns, string prefix, bool removeIdentity = false, string excludedColumns = null)
        {
            var columnsArr = columns.Replace(" ", "").Split(',');
            if (removeIdentity)
                columnsArr = columnsArr.Where(x => excludedColumns == null || !excludedColumns.Split(',').Contains(x)).ToArray();
            return string.Join(", ", columnsArr.Select(col => prefix + col));
        }
        public static string AddBraces(this string columns, string excludedColumns = null)
        {
            var columnsArr = columns.Replace(" ", "").Split(',').Where(x => excludedColumns == null || !excludedColumns.Split(',').Contains(x));
            return string.Join(", ", columnsArr.Select(col => string.Format("[{0}]", col)));
        }
        public static string GenerateUpdateQuery(this string columns, string table, string key, string excludedColumns = "PublicId,Id,Status,CreationDate")
        {
            var columnsArr = columns.Replace(" ", "").Split(',').Where(x => !excludedColumns.Split(',').Contains(x));
            return string.Format("UPDATE [{0}] SET {1} WHERE {2}=@{2};", table, string.Join(", ", columnsArr.Select(col => string.Format("[{0}]=@{0}", col))), key);
        }
        public static string GenerateInsertQuery(this string columns, string table, string excludedColumns = "PublicId")
        {
            return string.Format("INSERT INTO [{0}] ({1}) VALUES({2});", table, columns.AddBraces(excludedColumns), columns.AddPrefix("@", true, excludedColumns));
        }

        public static string GetIdColumn(this string id)
        {
            Guid guid;
            int publicid;
            if (Guid.TryParse(id, out guid))
            {
                return "Id";
            }
            if (int.TryParse(id, out publicid))
            {
                return "PublicId";
            }
            return "";
        }
        #endregion 
    }
    public static class EnumExtensions
    {
        public static List<T> GetAllList<T>() where T : struct
        {
            return (from T enumValue in Enum.GetValues(typeof(T))
                    where (Convert.ToInt32(enumValue)) != -1
                    select enumValue)
                      .ToList();
        }

        public static Array GetAllList(string typeStr)
        {
            var type = Type.GetType(typeStr);
            if (type == null)
                return null;
            return Enum.GetValues(type);
        }
        public static string DisplayName<T>(this T enumValue)
        {
            var type = enumValue.GetType();
            if (type.IsEnum)
            {
                return enumValue.ToString();
            }
            return "";
        }

        public static string Key<T>(this T enumValue)
        {
            return enumValue.ToString().ToLower();
        }

        public static int Value<T>(this T enumValue)
        {
            var genericType = enumValue.GetType();
            if (genericType.IsEnum)
            {
                var eenum = Enum.Parse(enumValue.GetType(), enumValue.ToString()) as Enum;
                return Convert.ToInt32(eenum);
            }
            return -1;
        }

        public static IEnumerable<dynamic> SelectList<T>() where T : struct
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>();
            return values.Select(x => new { Value = x.Value(), Text = x.DisplayName() }).ToArray();
        }

        public static T FromKey<T>(string key)
        {
            return (T)Enum.Parse(typeof(T), key.Trim());
        }
    }
}