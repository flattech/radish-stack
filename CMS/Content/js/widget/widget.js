var emptyGuid = '00000000-0000-0000-0000-000000000000';
window.back.widgets = (function ($, ko, bootbox) {
    //Helpers
    var Item = function(data) {
            var self = this;
            data = data || {};
            self.id = data.Id;
            //self.creationdate = data.CreatedDate || '';
            self.title = data.Title;
            self.image = data.Photo;
        },
        importItem = function(paragraphs) {
            return $.map(paragraphs || [],
                function(paragraphData, i) {
                    return new Item(paragraphData);
                });
        };

    
    var viewmodel,
        itemviewmodel = function () {
            var list = ko.observableArray(),
                addItem = function (data) {
                    //if (data.Image) {
                        var isfound = ko.utils.arrayFirst(list(), function(item) {
                            return item.id === data.Id;
                        });
                        if (isfound)
                            window.back.alert("Exists Before" + ":(" + data.Title + ")");
                        else
                            list.unshift(new Item(data, true));
                    //} else {
                    //    window.back.alert(data.Title + " بحاجة لصورة");
                    //}
                },
                removeSlide = function(data) {
                    bootbox.confirm("Are You Sure To Delete item?", function (result) {
                        if (result) {
                            list.remove(data);
                        }
                    });
                    return false;
                },
                initModel = function(data) {
                    var l = importItem(data);
                    list(l);
                },
                 getIds = ko.computed(function() {
                     var ids = [];
                     for (var i = 0; i < list().length; i++) {
                         ids.push(list()[i].id);
                     }
                     return ids.join();
                 }, this),
                nameForInput = function(index, name) {
                    return 'models[' + index + '].' + name;
                };
            return {
                list: list,
                addItem: addItem,
                init: initModel,
                getIds:getIds,
                removeSlide: removeSlide,
                nameForInput: nameForInput
            };
        },
        callbackForPopUp = function() {
            $("#search_results input[type=checkbox]:checked").each(function(index, element) {
                viewmodel.addItem({
                    Id: $(element).data("itemid"),
                    Title: $(element).data("itemtitle"),
                    Photo: $(element).data("itemimage")
                });
            });
        },
        init = function(data) {
            viewmodel = new itemviewmodel();
            viewmodel.init(data);
            ko.applyBindings(viewmodel, $("#itemkocontainer")[0]);
            //window.back.PopUpTitle = "مسلسل الاخبار";
            window.back.finishCallbackForPopUp = callbackForPopUp;
        };
 
 
    return { init: init, viewmodel: viewmodel };
}(jQuery, ko, bootbox));