window.back.mulipleuploader = (function ($, ko, bootbox) {
    //Helpers
    //var Item = function (data) {
    //    var self = this;
    //    data = data || {};
    //    self.id = data.Id;
    //    self.Value = data.url;
  
    //},
    //    importItem = function (paragraphs) {
    //        return $.map(paragraphs || [],
    //            function (paragraphData, i) {
    //                return new Item(paragraphData);
    //            });
    //    };


    var viewmodel,
        itemviewmodel = function () {
            var list = ko.observableArray(),
                addItem = function (data) {
                    list.unshift(data);
                },
                removeItem = function (data) {
                    bootbox.confirm("Are You Sure To Delete item?", function (result) {
                        if (result) {
                            list.remove(data);
                        }
                    });
                    return false;
                },
                initModel = function (data) {
                   // alert(data);
                    // var l = importItem(data);
                    if(data)
                    list(data);
                },
               
                nameForInput = function (index, name) {
                    return 'models[' + index + '].' + name;
                };
            return {
                list: list,
                addItem: addItem,
                init: initModel,
                removeItem: removeItem,
                nameForInput: nameForInput
            };
        },
      init = function (data) {
          viewmodel = new itemviewmodel();
          viewmodel.init(data);
          ko.applyBindings(viewmodel, $("#uploadkocontainer")[0]);
      },
    addItem = function (data) {
        viewmodel.addItem(data);
           
        };


    return { init: init, addItem: addItem };
}(jQuery, ko, bootbox));