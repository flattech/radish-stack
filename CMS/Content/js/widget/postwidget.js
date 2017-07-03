var emptyGuid = "00000000-0000-0000-0000-000000000000";
window.back.postwidget = (function ($, ko, bootbox) {
    //Helpers
    var col = function (rows, widgets, lg, text) {
        var self = this;
        self.lg = ko.observable(lg);
        self.text = ko.observable(text);
        self.rows = ko.observableArray(rows || []);
        self.widgets = ko.observableArray(widgets || []);
        self.addwidget = function (index) {
            $.get("/widget/mini").done(function (data) {
                bootbox.dialog({
                    message: data,
                    title: "Choose Widget" ,
                    buttons: {
                        success: {
                            label: window.AppResources.Next,
                            className: "btn-success",
                            callback: function () {
                                $("#search_results input[type=checkbox]:checked").each(function (index, element) {
                                    self.widgets.push({
                                        widgetid: $(element).data("itemid"),
                                        title: $(element).data("itemtitle")
                                    });
                                });
                            }
                        }
                    }
                });
            });
        };
        self.removewidget = function (data) {
            self.widgets.remove(data);
        };

        self.addrow = function () {
            self.rows.push(new row([new col([], [], 12, "c2")]));
        };
        self.removerow = function (data) {
            // if (self.rows().length > 1)
            self.rows.remove(data);
        };
    };
    var row = function (cols) {
        var self = this;
        self.cols = ko.observableArray(cols);
        self.addcol = function () {
            self.cols.push(new col([], [], 1, "c2"));
        };
        self.removecol = function (data) {
            //if (self.cols().length > 1)
            self.cols.remove(data);
        };
    };

    var widget = function (data) {
        var self = this;
        self.title = ko.observable(data.title);
    };


    function fillRows(rs) {
        var rows = [];
        for (var i = 0; i < rs.length; i++) {
            var cols = [];
            for (var j = 0; j < rs[i].cols.length; j++) {
                var c = rs[i].cols[j];
                var wds = [];
                for (var h = 0; h < c.widgets.length; h++) {
                    wds.push(new widget(c.widgets[h]));
                }
                cols.push(new col(fillRows(c.rows), wds, c.lg, c.text));
            }
            rows.push(new row(cols));
        }
        return rows;
    };

    var viewmodel,
        itemviewmodel = function () {

            var rows = ko.observableArray(),

                addrow = function (index) {
                    console.log(index);
                    rows.splice(index + 1, 0, new row([new col([], [], 12, "c2")]));
                },

                removerow = function (data) {
                    if (rows().length > 1)
                        rows.remove(data);

                },
                getJson = function () {
                    if (rows().length > 0 && rows()[0].cols().length>0)
                        return ko.toJSON(viewmodel);
                },
                initModel = function (data) {
                    rows(data);
                };
            return {
                getJson: getJson,
                rows: rows,
                addrow: addrow,
                init: initModel,
                removerow: removerow
            };
        },
        callbackForPopUp = function () {
         
        },

        init = function (data) {
            //  console.log(ko.toJSON(viewModel));

            //var dt = [
            //   new row([new col([], [new widget({ title: "slider" })], 12, "c0")]),
            //   new row([new col([], [new widget({ title: "posts" })], 9, "c1.1"), new col([], [new widget({ title: "recommend" })], 3, "c1.2")]),
            //   new row([new col([], [new widget({ title: "footer" })], 12, "c2")])
            //];

            var dt = [
                new row([])
            ];

            var finaldata = dt;
            if (data && data.rows)
                finaldata = fillRows(data.rows);

            //console.log(finaldata);
            viewmodel = new itemviewmodel();
            //var d = JSON.parse(data);
            //console.log(dt);
            viewmodel.init(finaldata);
            ko.applyBindings(viewmodel, $("#itemkocontainer")[0]);
            //window.back.PopUpTitle = "مسلسل الاخبار";
            window.back.finishCallbackForPopUp = callbackForPopUp;
        };


    return { init: init, viewmodel: viewmodel };

}(jQuery, ko, bootbox));