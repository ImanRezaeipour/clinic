/**
 *
 * @returns {}
 */
var PreparePresenterListGrid_OnLoad = function (elem) {

    var columns = [
        {field: "FirstName",title: "نام",width: 200},
        {field: "LastName",title: "نام خانوادگی",width: 200},
        {field: "MobileNumber",title: "موبایل",width: 200},
        {field: "PhoneNumber",title: "تلفن",width: 200}
    ];

    var dataSourceFields=  {
                    "Id": { type: "string" },
                    "FirstName": { type: "string" },
                    "LastName": { type: "string" },
                    "PhoneNumber": { type: "string" },
                    "MobileNumber": { type: "string" }
                }
            
    $(elem).appGrid({
        columns: columns,
        dataSourceUrl: "/Presenter/GetListAjax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "presenterListNew_OnClick",
        editEventHandler: "presenterListEdit_OnClick",
        deleteEventHandler: "presenterListDelete_OnClick"
    });
}

function refreshGrid() {
    $(".k-pager-refresh.k-link").click();
}

var presenterListNew_OnClick = function (e) {
    window.location.href = '/Presenter/create';
}

var presenterListEdit_OnClick = function (e) {
    var entityGrid = $("#PresentersGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    var s = selectedItem.Id;
    window.location.href = '/Presenter/edit/' + selectedItem.Id;
}

var presenterListDelete_OnClick = function (e) {
    var entityGrid = $("#PresentersGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: 'POST',
            url: '/Presenter/deleteajax',
            data: { id: id },
            dataType: 'json',
            complete: function (xhr, status) {
                refreshGrid();
            }
        });
    }
    return false;
}

var multipelid = function () {
    var entityGrid = $("#EntitesGrid").data("kendoGrid");
    var rows = entityGrid.select();
    rows.each(function (index, row) {
        var selectedItem = entityGrid.dataItem(row);
        // selectedItem has EntityVersionId and the rest of your model
    });
}


var preparePresenterCombobox_OnLoad = function (elem) {
    var comboboxOptions = {
        dataSourceUrl: "/presenter/GetSelectListAjax"
    }

    $(elem).appCombobox(comboboxOptions);
}
