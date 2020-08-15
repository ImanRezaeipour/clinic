
/**
 *
 * @returns {}
 */
var prepareReportListGrid_OnLoad = function () {
    var columns = [
        {field: "Title",title: "نام",width: 200},
        {field: "Description",title: "عنوان",width: 200}
        
    ];

    var dataSourceFields = {
        "Id": {type: "string"},
        "Title": {type: "string"},
        "Description": {type: "string"}
    }


    $("#ReportsGridContainer").appGrid({
        columns: columns,
        dataSourceUrl: "/Report/getlistajax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "ReportListNewOnClick",
        editEventHandler: "ReportListEditOnClick",
        deleteEventHandler: "ReportListDeleteOnClick",
        detailEventHandler: "ReportListDetailOnClick"
    });
}

function refreshGrid() {
    $(".k-pager-refresh.k-link").click();
}

var ReportListNewOnClick = function (e) {
    window.location.href = "/Report/create";
}

var ReportListEditOnClick = function (e) {
    var entityGrid = $("#ReportsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    window.location.href = "/Report/edit/" + selectedItem.Id;
}

var ReportListDetailOnClick = function (e) {
    var entityGrid = $("#ReportsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    var windowOptions= {
        contentUrl: "/Report/FromDateToDate/" + selectedItem.Id
    }
    $('#dialog').appWindow(windowOptions);
    //$('body').appNavigate();
}

var ReportListDeleteOnClick = function (e) {
    var entityGrid = $("#ReportsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: "POST",
            url: "/Report/deleteajax",
            data: { id: id },
            dataType: "json",
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

var validateNewReportForm = function () {
    //$("#newReport").simpleValidate();
}

var fillMaskedInput = function () {
    $("#BirthDayOn").kendoMaskedTextBox({
        mask: "1300/00/00"
    });
}


//var isExistNationalCode = function () {
//    var nationalCode = $('#NationalCode').val();
//    $.ajax({
//        url: '/Patient/IsExistNationalCodeAjax',
//        data: { nationalCode: nationalCode },
//        dataType: "json",
//        type: "GET",
//        complete: function (xhr, status) {
//            console.log(xhr);
//            if (xhr.responseJSON.Data === true) {
//                window.location.href = "/Patient/edit/" + nationalCode;
//            };
//        }
//    });
//}