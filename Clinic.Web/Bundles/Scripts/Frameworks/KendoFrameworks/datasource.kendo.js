; (function ($) {
    $.appDataSource = function (options) {
        var defaults = {
            dataSourceReadUrl: null,
            dataSourceFields: null,
            dataSourceAggregate: null,
            onRequestStart: function(e){},
            onRequestEnd: function (e) { },
            onChange: function(e){}
        }

        var settings = $.extend({}, defaults, options);

        var dataSourceOptions = {
            transport: {
                read: {
                    url: settings.dataSourceReadUrl,
                    data: {},
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "GET"
                },
                create: {
                    url: "",
                    dataType: "jsonp"
                },
                update: {
                    url: "",
                    dataType: "jsonp"
                },
                destroy: {
                    url: "",
                    dataType: "jsonp"
                },
                parameterMap: function (options, type) {
                    return JSON.stringify(options);
                }
            },
            requestStart: function(e) {
                settings.onRequestStart(e);
            },
            requestEnd: function(e) {
                settings.onRequestEnd(e);
            },
            schema: {
                data: "Data",
                total: "Total",
                model: {
                    id: "Id",
                    fields: settings.dataSourceFields
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
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            change: function (e) {
                var data = this.data();
                settings.onChange(e);
            },
            cancel: function(e) {
                e.preventDefault();
            },
            aggregate: settings.dataSourceAggregate
        }

        var dataSource = new kendo.data.DataSource(dataSourceOptions);

        return dataSource;
    }
}(jQuery))