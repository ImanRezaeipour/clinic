var prepareDocumentSaleProductListGrid_OnLoad = function (elem) {
    var dataSource = $.appLocalDataSource({
        model: {
            Id: { type: "string" },
            ProductTitle: { type: "string" },
            ProductId: { type: "string" },
            ProductPrice: { type: "string", editable: false, validation: { required: true, min: 1 } },
            ProductCount: { type: "number", defaultValue: 1 },
            TotalPrice: { type: "number", editable: false, validation: { required: true } },
            Description: { type: "string" },
            IsReturn: { type: "boolean" }
        },
        aggregate: [
          { field: "ProductTitle", aggregate: "count" },
          { field: "TotalPrice", aggregate: "sum" }
        ]
    });

    $(elem).appGrid({
        dataSource: dataSource,
        isSimple: true,
        height: 360,
        columns: [
          { field: "Product", title: "محصول", template: "#= ProductTitle #", footerTemplate: "تعداد: #= data.ProductTitle.count #" },
          { field: "ProductPrice", title: "قیمت" },
          { field: "ProductCount", title: "تعداد" },
          { field: "TotalPrice", title: "مجموع قیمت", footerTemplate: "مجموع: #= data.TotalPrice.sum #" },
          { field: "Description", title: "توضیحات" },
          { field: "IsReturn", title: "برگشتی" }
        ],
        toolbar: [
          {
              template: "<button class='k-button' onclick='documentSaleProductListCreate_OnClick(event)'>افزودن</button>",
              text: "افزودن"
          },
          {
              template: "<button id='documentSaleProductListEdit' class='k-button' onclick='documentSaleProductListEdit_OnClick(event)'>ویرایش</button>",
              text: "ویرایش"
          },
          {
              template: "<button class='k-button' onclick='documentSaleProductListDelete_OnClick(event)'>حذف</button>",
              text: "حذف"
          },
          {
              template: "<button class='k-button' onclick='documentSaleProductListSave_OnClick(event)'>ثبت</button>",
              text: "ثبت"
          },
          {
              template: "<button class='k-button' onclick='documentSaleProductListCancel_OnClick(event)'>لغو</button>",
              text: "لغو"
          },
          {
              template: "<button class='k-button' onclick='documentSaleProductListRefresh_OnClick(event)'>بازسازی</button>",
              text: "بازسازی"
          }
        ]
    });
}

var documentSaleProductListCreate_OnClick = function (e) {
    e.preventDefault();
    $("#saleDialog").appWindow({
        contentUrl: "/Document/FactorProduct",
        title: "افزودن محصول",
        widthPercent: 40,
        heigthPercent: 40
    });
}

var documentSaleProductListEdit_OnClick = function (e) {
    e.preventDefault();
    var grid = $("#DocumentSaleProductsGridContainer").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    if (selectedItem != null) {
        $("#saleDialog").appWindow({
            contentUrl: "/Document/FactorProduct",
            title: "افزودن محصول",
            widthPercent: 40,
            heigthPercent: 40,
            onComplete: function() {
                $("#SaleProductCreate_Id").val(selectedItem.Id);
                $("#SaleProductCreate_ProductId").val(selectedItem.ProductId);
                $("#SaleProductCreate_Price").val(selectedItem.ProductPrice);
                $("#SaleProductCreate_Count").val(selectedItem.ProductCount);
                $("#SaleProductCreate_TotalPrice").val(selectedItem.TotalPrice);
                $("#SaleProductCreate_Description").val(selectedItem.Description);
                $("#SaleProductCreate_IsReturn").val(selectedItem.IsReturn);
            }
        });
    }
    $("#documentSaleProductListEdit").attr("disabled", "disabled");
}

var documentSaleProductListDelete_OnClick = function (e) {
    e.preventDefault();
    var grid = $("#DocumentSaleProductsGridContainer").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    if (selectedItem != null)
        grid.dataSource.remove(selectedItem);
}

var documentSaleProductListRefresh_OnClick = function (e) {
    e.preventDefault();
    var grid = $("#DocumentSaleProductsGridContainer").data("kendoGrid");
    grid.refresh();
}