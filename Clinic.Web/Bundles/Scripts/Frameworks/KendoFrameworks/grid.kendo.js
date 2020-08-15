;(function($) {
    $.fn.appGrid = function(options) {
        var self = this;
        var elem = $(this);

        var defaults = {
          debug: false,
            isSimple: false,
            allowCopy: { delimeter: "," },
            autoBind: true,
            filterable: { extra: true, mode: "menu" },
            groupable: { enabled: true, showFooter: true },
            editable: { mode: "popup", update: true, createAt: "bottom" },
            toolbar: null,
            height: 550,
            pageable: { refresh: true, pageSizes: true, previousNext: true, numeric: true, buttonCount: 5, info: true, input: false },
            scrollable: { virtual: true },
            selectable: "row",
            sortable: { allowUnsort: false, showIndexes: false, initialDirection: "desc", mode: "single" },
            columns: [],
            dataSource: null,
            dataSourceUrl: '',
            dataSourceFields: null,
            dataSourceOnChange: function(e){},
            onChange: function(e) {},
            createEventHandler: '',
            editEventHandler: '',
            deleteEventHandler: '',
            onRequestEnd: function (e) { },
            detailTemplate: null,
            onDetailInit: null,
            onDetailExpand: null,
            onDetailCollapse: null
        }

        var settings = $.extend({}, defaults, options);

        var defaultToolbar = [
            {
                name: "create",
                template: "<button class='k-button' onclick='" + settings.createEventHandler + "(event)'>افزودن</button>",
                text: "افزودن"
            },
            {
                name: "edit", template: "<button class='k-button' onclick='" + settings.editEventHandler + "(event)'>ویرایش</button>",
                text: "ویرایش"
            },
            {
                name: "delete",
                template: "<button class='k-button' onclick='" + settings.deleteEventHandler + "(event)'>حذف</button>",
                text: "حذف"
            },
            {
                name: "detail",
                template: "<button class='k-button' onclick='" + settings.detailEventHandler + "(event)'>جزئیات</button>",
                text: "جزئیات"
            }
        ];

        function getDataSource() {
            var dataSourceOptions = {
                dataSourceReadUrl: settings.dataSourceUrl,
                dataSourceFields: settings.dataSourceFields,
                onChange: function(e) {
                    settings.dataSourceOnChange(e);
                } 
            }
            return $.appDataSource(dataSourceOptions);
        }

        function refreshGrid() {
            $(".k-pager-refresh.k-link").click();
        }

        function multipelid() {
            var entityGrid = $("#EntitesGrid").data("kendoGrid");
            var rows = entityGrid.select();
            rows.each(function(index, row) {
                var selectedItem = entityGrid.dataItem(row);
                // selectedItem has EntityVersionId and the rest of your model
            });
        }

        function grid() {
            return $(self).data("kendoGrid");
        }

        function debug(data) {
          if (settings.debug)
            console.log(data);
        }

        var kendoGridOptions = {
            allowCopy: settings.allowCopy,
            autoBind: settings.autoBind,
            filterable: settings.isSimple === false ? settings.filterable : false,
            groupable: settings.isSimple === false ? settings.groupable : false,
            height: settings.height,
            pageable: settings.isSimple === false ? settings.pageable : false,
            scrollable: settings.scrollable,
            selectable: settings.selectable,
            sortable: settings.isSimple === false ? settings.sortable : false,
            toolbar: settings.toolbar || defaultToolbar,
            columns: settings.columns,
            dataSource: settings.dataSource || getDataSource(),
            editable: {
                update: true,
                destroy: true,
                mode: "popup"
            },
            change: function (e) {
              debug(e);
                 settings.onChange(e);
            },
            beforeEdit: function(e) {
              debug(e);
            },
            edit: function(e) {
              debug(e);
            },
            remove: function(e) {
              debug(e);
            },
            saveChanges: function(e) {
              debug(e);
            },
            save: function(e) {
              debug(e);
            },
            cancel: function(e) {
              debug(e);
            },
            cellClose: function(e) {
              debug(e);
            },
            columnHide: function(e) {
              debug(e);
            },
            detailTemplate: settings.detailTemplate,
            detailInit: settings.onDetailInit === null ? null : function (e) {
              debug(e);
                settings.onDetailInit(e);
            },
            detailExpand: settings.onDetailExpand === null ? null : function (e) {
              debug(e);
                settings.onDetailExpand(e);
            },
            detailCollapse: settings.onDetailCollapse === null ? null : function (e) {
              debug(e);
                settings.onDetailCollapse(e);
            },
            dataBound: function () {
              debug();
                //this.expandRow(this.tbody.find("tr.k-master-row").first());
            }
        }

        $(self).wrap("<div class='k-rtl'></div>");
        $(self).kendoGrid(kendoGridOptions);

        return elem;
    }
}(jQuery));

