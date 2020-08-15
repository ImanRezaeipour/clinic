; (function ($) {
    $.appLocalDataSource = function (options) {
        var defaults = {
            model: null,
            aggregate: null,
            data: [],
            onRequestStart: function(e){},
            onRequestEnd: function (e) { },
            onChange: function(e){}
        }

        var settings = $.extend({}, defaults, options);

        var dataSourceOptions = {
            data: settings.data,
            transport: {
                read: function (o) {
                    o.success(settings.data);
                },
                create: function (o) {
                    var item = o.settings.data;
                    //assign a unique ID and return the record
                    //counter++;
                    //item.ProductID = counter;
                    o.success(settings.data);
                },
                update: function (o) {
                    o.success();
                },
                destroy: function (o) {
                    o.success();
                }
            },
            requestStart: function(e) {
                settings.onRequestStart(e);
            },
            requestEnd: function(e) {
                settings.onRequestEnd(e);
            },
            schema: {
                //data: "Data",
                //total: "Total",
                model: {
                    id: "Id",
                    fields: settings.model
                }
            },
            error: function (e) {
                //alert(e.errorThrown);
            },
            pageSize: 10,
            sort: {
                field: "CreatedOn",
                dir: "desc"
            },
            //serverPaging: true,
            //serverFiltering: true,
            //serverSorting: true,
            change: function (e) {
                //var data = this.data();
                settings.onChange(e);
            },
            cancel: function(e) {
                e.preventDefault();
            },
            aggregate: settings.aggregate
        }

        var dataSource = new kendo.data.DataSource(dataSourceOptions);

        return dataSource;
    }
}(jQuery))