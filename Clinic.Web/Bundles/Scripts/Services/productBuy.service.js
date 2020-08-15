/**
 *
 * @returns {}
 */

var PrepareProductBuyListGrid_OnLoad = function (elem) {

    var columns = [
        {field: "CompanyTitle",title: "نام شرکت",width: 200},
        {field: "Title",title: "نام محصول",width: 200},
        {field: "BoughtCount",title: "تعداد خریداری شده",width: 200},
        {field: "UnitPrice",title: "قیمت خریداری شده",width: 200}
    ];

    var dataSourceFields = {
                    "Id": { type: "string" },
                    "CompanyTitle": { type: "string" },
                    "Title": { type: "string" },
                    "BoughtCount": { type: "string" },
                    "UnitPrice": { type: "string" }
    }
    
    $(elem).appGrid({
        columns: columns,
        dataSourceUrl: "/productBuy/GetListAjax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "productBuyListNew_OnClick",
        editEventHandler: "productBuyListEdit_OnClick",
        deleteEventHandler: "productBuyListDelete_OnClick"
    });
}

function refreshGrid() {
    $(".k-pager-refresh.k-link").click();
}

var productBuyListNew_OnClick = function (e) {
    window.location.href = '/ProductBuy/create';
}

var productBuyListEdit_OnClick = function (e) {
    var entityGrid = $("#ProductBuysGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    var s = selectedItem.Id;
    window.location.href = '/ProductBuy/edit/' + selectedItem.Id;
}

var productBuyListDelete_OnClick = function (e) {
    var entityGrid = $("#ProductBuysGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: 'POST',
            url: '/ProductBuy/deleteajax',
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

var fillKendoTreeProductBuy = function () {
    $.ajax({
        url: "/product/Tree",
        data: {},
        type: "GET",
        dataType: "json",
        complete: function (xhr, status) {
            var dataText = JSON.stringify(xhr.responseJSON);
            var dataSource = toKendoFlatDataSource(dataText);
            dataSource = JSON.parse(dataSource);

            // the tree for visualizing data
            $("#treeProductBuyContainer").kendoTreeView({
                dataSource: processTable(dataSource, "id", "parent", "00000000-0000-0000-0000-000000000000"),
                loadOnDemand: false,
                select: function (e) {
                    var productId = $("#treeProductBuyContainer").getKendoTreeView().dataItem(e.node).id;

                    $("#ProductId").val(productId);
                }
            });
           // if ($("#ProductId").val() !== "")
               // expandAndSelectNode("#treeProductBuyContainer", $("#ProductId").val());
        }
    });
}  