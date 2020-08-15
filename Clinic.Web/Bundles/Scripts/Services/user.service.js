var fillKendoGridUser = function (tag) {
    debugger;
    var sortDirection = $('#search').find('#SortDirection').find(":selected").val();
    var sortMember = $('#search').find('#SortMember').find(":selected").val();
    var pageSize = $('#search').find('#PageSize').find(":selected").val();
    var isActive = $('#search').find('#IsActive').find(":selected").val();
    var isBan = $('#search').find('#IsBan').find(":selected").val();
    var isVerify = $('#search').find('#IsVerify').find(":selected").val();
    var term = $('#search').find('#Term').val();

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: '/user/getlistajax',
                data: { sortDirection: sortDirection, sortMember: sortMember, pageSize: pageSize, term: term, isActive: isActive, isBan: isBan, isVerify: isVerify },
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                type: "GET"
            },
            parameterMap: function (options) {
                return kendo.stringify(options);
            }
        },
        schema: {
            data: "Data",
            total: "Total",
            model: {
                fields: {
                    "Id": { type: "string" },
                    "UserName": { type: "string" },
                    "IsBan": { type: "string" },
                    "BannedReason": { type: "string" },
                    "IsSystemAccount": { type: "string" },
                    "DisplayName": { type: "string" }
                }
            }
        },
        error: function (e) {
            alert(e.errorThrown);
        },
        pageSize: pageSize,
        sort: { field: "Id", dir: "desc" },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    });

    $(tag).kendoGrid({
        dataSource: dataSource,
        autoBind: true,
        scrollable: false,
        pageable: true,
        sortable: true,
        reorderable: true,
        columns: [
        { field: "UserName", title: "نام کاربری" },
        { field: "IsBan", title: "مسدود", template: "# if (IsBan == 'true') {# مسدود شده #} else if (IsBan == 'false') {#مسدود نشده #} else {# #} #" },
        { field: "BannedReason", title: "دلیل مسدود شدن" },
        { field: "DisplayName", title: "نام نمایشی" },
        { field: "IsSystemAccount", title: "اکانت سیستمی", template: "# if (IsSystemAccount == 'true') {# اکانت سیستمی #} else if (IsSystemAccount == 'false') {# اکانت معمولی #} else {#  #} #" },
        {field: "",template: '<div style="display: inline-block;">' +
                '<a style="margin: 2px; width: 22px;" class="button primary" href="edit/#= Id #" >' +
                '<i class="fa fa-pencil-square-o"></i>' +
                '</a>' +
                '</div>'
        }
        ]
    });
}