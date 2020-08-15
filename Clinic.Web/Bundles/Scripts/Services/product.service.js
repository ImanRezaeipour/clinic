/**
 *
 * @returns {}
 */
var PrepareProductListGrid_OnLoad = function (elem) {
  
    var columns = [
        {field: "Title",title: "عنوان",width: 200},
        {field: "AvailableCount",title: "موجودی",width: 200},
        {field: "UnitPrice",title: "قیمت",width: 200}
    ];

    var dataSourceFields = {
                    "Id": { type: "string" },
                    "Title": { type: "string" },
                    "AvailableCount": { type: "string" },
                    "UnitPrice": { type: "string" }
    }

    $(elem).appGrid({
        columns: columns,
        dataSourceUrl: "/product/GetListAjax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "productListNew_OnClick",
        editEventHandler: "productListEdit_OnClick",
        deleteEventHandler: "productListDelete_OnClick"
    });
}

function refreshGrid() {
    $(".k-pager-refresh.k-link").click();
}

var productListNew_OnClick = function (e) {
    window.location.href = '/Product/create';
}

var productListEdit_OnClick = function (e) {
    var entityGrid = $("#ProductsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    var s = selectedItem.Id;
    window.location.href = '/Product/edit/' + selectedItem.Id;
}

var productListDelete_OnClick = function (e) {
    var entityGrid = $("#ProductsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: 'POST',
            url: '/Product/deleteajax',
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

var fillKendoTreeProduct2_OnLoad = function (tag) {
    debugger;
    $.ajax({
        url: "/product/tree",
        data: {},
        type: "GET",
        dataType: "json",
        complete: function (xhr, status) {
            console.log(xhr);
            var dataText = JSON.stringify(xhr.responseJSON.Data);
            var dataSource = toKendoFlatDataSource(dataText);
            dataSource = JSON.parse(dataSource);
            $(tag).kendoTreeView({
                loadOnDemand: false,
                dataSource: toHierarchicalDataSource(dataSource, "id", "parent", "00000000-0000-0000-0000-000000000000"),
                select: function (e) {
                    var productId = $(tag).getKendoTreeView().dataItem(e.node).id;
                    $("#ParentId").val(productId);
                    onSelectKendoTreeProduct(tag);
                }
            });
            //if ($("#ParentId").val() !== "")
            //    expandAndSelectNode(tag, $("#ParentId").val());
        }
    });
}

var fillKendoTreeProduct = function () {
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
            $("#treeProductContainer").kendoTreeView({
                dataSource: processTable(dataSource, "id", "parent", "00000000-0000-0000-0000-000000000000"),
                loadOnDemand: false,
                select: function (e) {
                    var parentId = $("#treeProductContainer").getKendoTreeView().dataItem(e.node).id;

                    $("#ParentId").val(parentId);
                }
            });
            //if ($("#ParentId").val() !== "")
            //    expandAndSelectNode("#treeProductContainer", $("#ParentId").val());
        }
    });
}

function toKendoFlatDataSource(data) {
    data = JSON.parse(data);
    var temp = JSON.parse('[]');
    $.each(data, function (key, value) {
        temp.push({
            id: value.Id,
            text: value.Title,
            parent: value.ParentId
        });
    });
    return JSON.stringify(temp);
}

function processTable(data, idField, foreignKey, rootLevel) {
    var hash = {};

    for (var i = 0; i < data.length; i++) {
        var item = data[i];
        var id = item[idField];
        var parentId = item[foreignKey];

        hash[id] = hash[id] || [];
        hash[parentId] = hash[parentId] || [];

        item.items = hash[id];
        hash[parentId].push(item);
    }

    return hash[rootLevel];
}

var onSelectKendoTreeProduct = function (elem) {
    $("[data-inner-event='select']").each(function () {
        var name = $(elem).attr('name');
        var by = $(this).data('inner-by');
        if (name === by) {
            var action = $(this).data('inner');
            $(this).empty();
            $(this).load(action);
        }
    });
}

