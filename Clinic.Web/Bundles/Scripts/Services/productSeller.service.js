/**
 *
 * @returns {}
 */
var PrepareProductSellerListGrid_OnLoad = function (elem) {

    var columns = [
        
        {field: "CompanyTitle",title: "عنوان فروشنده",width: 200},
        {field: "FirstName",title: "نام",width: 200},
        {field: "LastName",title: "نام خانوادگی",width: 200},
        {field: "MobileNumber",title: "موبایل",width: 200},
        {field: "PhoneNumber",title: "تلفن",width: 200}
    ];
    var dataSourceFields = {
                    "Id": { type: "string" },
                    "CompanyTitle": { type: "string" },
                    "FirstName": { type: "string" },
                    "LastName": { type: "string" },
                    "PhoneNumber": { type: "string" },
                    "MobileNumber": { type: "string" }
    }

    $(elem).appGrid({
        columns: columns,
        dataSourceUrl: "/productSeller/GetListAjax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "productSellerListNew_OnClick",
        editEventHandler: "productSellerListEdit_OnClick",
        deleteEventHandler: "productSellerListDelete_OnClick"
    });
      }

function refreshGrid() {
    $(".k-pager-refresh.k-link").click();
}

var productSellerListNew_OnClick = function (e) {
    window.location.href = '/ProductSeller/create';
}

var productSellerListEdit_OnClick = function (e) {
    var entityGrid = $("#ProductSellersGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    var s = selectedItem.Id;
    window.location.href = '/ProductSeller/edit/' + selectedItem.Id;
}

var productSellerListDelete_OnClick = function (e) {
    var entityGrid = $("#ProductSellersGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: 'POST',
            url: '/ProductSeller/deleteajax',
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

var prepareProductSellerList_OnLoad = function(elem) {
    var comboOptions= {
        dataSourceUrl: "/productseller/GetSelectListAjax"
    }
    $(elem).appCombobox(comboOptions);
}
