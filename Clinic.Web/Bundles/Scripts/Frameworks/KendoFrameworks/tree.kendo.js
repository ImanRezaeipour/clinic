
var onSelectKendoTreeDocument = function (elem) {
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

function expandAndSelectNode(treeView, id) {
    var kendoTreeview = $(treeView).data("kendoTreeView");
    kendoTreeview.expandTo(id);
    var dataItem = kendoTreeview.dataSource.get(id);
    var element = kendoTreeview.findByUid(dataItem.uid);
    kendoTreeview.select(element);
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



var fillTreeDocument_OnLoad = function () {
    debugger;
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
            $("#treeDocumentContainer").kendoTreeView({
                dataSource: processTable(dataSource, "id", "parent", "00000000-0000-0000-0000-000000000000"),
                loadOnDemand: false,
                select: function (e) {
                    var parentId = $("#treeDocumentContainer").getKendoTreeView().dataItem(e.node).id;

                    $("#ParentId").val(parentId);
                }
            });
            if ($("#ParentId").val() !== "")
                expandAndSelectNode("#treeDocumentContainer", $("#ParentId").val());
        }
    });
}

