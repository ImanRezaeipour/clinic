var prepareDocumentFactorProductCombobox_OnLoad = function (elem) {
    $(elem).appCombobox({
        dataSourceUrl: "/Product/GetSelectListAjax",
        onSelect: function (e) {
            var productId = e.dataItem.Value;
            $.ajax({
                url: "/Product/GetPrice",
                data: { id: productId },
                dataType: "json",
                type: "GET",
                complete: function (xhr, status) {
                    $("#SaleProductCreate_Price").val(xhr.responseJSON.Data);
                }
            });
        }
    });
}

var simpleNumeric_OnLoad = function (elem) {
    $(elem).appNumeric({
        onChange: function (value) {
            var mul = $("#SaleProductCreate_Count").val() * $("#SaleProductCreate_Price").val();
            $("#SaleProductCreate_TotalPrice").val(mul);
        }
    });
}

var documentSaleProductCreateSave_OnClick = function(elem) {
    var grid = $("#DocumentSaleProductsGridContainer").data("kendoGrid");
    var ds = {
        Id: $("#SaleProductCreate_Id").val(),
        ProductTitle: $("#SaleProductCreate_ProductId").data("kendoComboBox").text(),
        ProductId: $("#SaleProductCreate_ProductId").val(),
        ProductPrice: $("#SaleProductCreate_Price").val(),
        ProductCount: $("#SaleProductCreate_Count").val(),
        TotalPrice: $("#SaleProductCreate_TotalPrice").val(),
        Description: $("#SaleProductCreate_Description").val(),
        IsReturn: $("#SaleProductCreate_IsReturn").val()
    }
    if (ds.Id !== '') {
        var entity = grid.dataSource.get(ds.Id);
        var index = grid.dataSource.indexOf(entity);
        grid.dataSource.data()[index].Description = ds.Description;
        grid.refresh();
    } else {
        ds.Id = uuidv4();
        grid.dataSource.add(ds);
    }
    $(elem).closest(".k-window-content").data("kendoWindow").close();
    $("#documentSaleProductListEdit").removeAttr("disabled");
}