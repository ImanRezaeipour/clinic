var prepareDocumentDoctorCombobox_OnLoad = function (elem) {
    $(elem).appCombobox({
        dataSourceUrl: "/Doctor/GetSelectListAjax"
    });
}

var prepareDocumentPresenterCombobox_OnLoad = function (elem) {
    $(elem).appCombobox({
        dataSourceUrl: "/Presenter/GetSelectListAjax"
    });
}

var prepareDocumentImageUploader_OnLoad = function (elem) {
    $(elem).appUpload({});
}

var prepareDocumentSaleListGrid_OnLoad = function (elem) {
    var dataSourceFields = {
        Id: { type: "string" },
        Description: { type: "string" },
        DiscountPercent: { type: "string" },
        TotalOtherPrice: { type: "string" },
        Products: {}
    }

    $(elem).appGrid({
        isSimple: true,
        height: 250,
        columns: [
          { field: "Description", title: "توضیحات", width: 100 },
          { field: "DiscountPercent", title: "درصد تخفیف", width: 100 },
          { field: "TotalOtherPrice", title: "جمع دیگر", width: 100 },
        ],
        dataSourceUrl: "/DocumentSale/GetListAjax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "documentSaleListCreate_OnClick",
        editEventHandler: "documentSaleListEdit_OnClick",
        deleteEventHandler: "documentSaleListDelete_OnClick",
        detailTemplate: kendo.template($("#template").html()),
        onDetailInit: function (e) {
            documentSaleListDetailRow_OnClick(e);
        }
    });
}

var documentSaleListDetailRow_OnClick = function (e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });
    detailRow.find(".orders").kendoGrid({
        dataSource: new kendo.data.DataSource({
            data: e.data.Products,
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { type: "string" },
                        ProductTitle: { type: "string" },
                        ProductId: { type: "string" },
                        ProductPrice: { type: "string" },
                        TotalPrice: { type: "string" },
                        ProductCount: { type: "string" },
                        Description: { type: "string" },
                        IsReturn: { type: "string" }
                    }
                }
            }
        }),
        height: 100,
        columns: [
            { field: "Product", title: "محصول", template: "#= ProductTitle #", footerTemplate: "تعداد: #= data.ProductTitle.count #" },
            { field: "ProductPrice", title: "قیمت" },
            { field: "ProductCount", title: "تعداد" },
            { field: "TotalPrice", title: "مجموع قیمت", footerTemplate: "مجموع: #= data.TotalPrice.sum #" },
            { field: "Description", title: "توضیحات" },
            { field: "IsReturn", title: "برگشتی" }
        ]
    });
}

var documentSaleListCreateSave_OnClick = function (e) {
    var saleProduct = $("#DocumentSaleProductsGridContainer").data("kendoGrid");
    var dataSource = {
        Description: $("#SaleDescription").val(),
        Products: saleProduct.dataSource.data()
    }
    var entityGrid = $("#DocumentsSaleGridContainer").data("kendoGrid");
    entityGrid.dataSource.add(dataSource);

    $("#SalesJson").val(JSON.stringify(entityGrid.dataSource.data()));
}

var documentSaleListCreate_OnClick = function (e) {
    e.preventDefault();
    $("#dialog").appWindow({
        contentUrl: "/Document/Factor",
        title: "ایجاد فاکتور",
        widthPercent: 85,
        heigthPercent: 85
    });
}

var documentSaleListEdit_OnClick = function (e) {
    var entityGrid = $("#DocumentsalesGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    var s = selectedItem.Id;
    window.location.href = '/DocumentSale/edit/' + selectedItem.Id;
}

var documentSaleListDelete_OnClick = function (e) {
    var entityGrid = $("#DocumentSalesGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: 'POST',
            url: '/DocumentSale/deleteajax',
            data: { id: id },
            dataType: 'json',
            complete: function (xhr, status) {
                refreshGrid();
            }
        });
    }
    return false;
}