
//#region PAGE NEW

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var fillKendoTreeRole = function(elem) {
    $.ajax({
        url: "/role/GetPermissionListAjax",
        data: {},
        type: "GET",
        dataType: "json",
        complete: function (xhr, status) {
            debugger;
            console.log(xhr);
            var responseText = JSON.stringify(xhr.responseJSON);
            var dataSource = toKendoFlatDataSource(responseText);
            dataSource = JSON.parse(dataSource);

            $(elem).kendoTreeView({
                checkboxes: {
                    checkChildren: true
                },
                loadOnDemand: false,
                dataSource: toHierarchicalDataSource(dataSource, "id", "parent", null),
                check: function (e) {
                    var checkedNodes = [];
                    var treeView = $(elem).data("kendoTreeView");
                    var message;

                    checkedNodeIds(treeView.dataSource.view(), checkedNodes);

                    if (checkedNodes.length > 0) {
                        //var permissions = JSON.stringify(checkedNodes);
                        $('#permissions').val(checkedNodes.join(","));
                        //message = "IDs of checked nodes: " + checkedNodes.join(",");
                    } else {
                        //message = "No nodes checked.";
                    }

                    //$("#result").html(message);
                }
            });
        }
    });
}

/**
 * function that gathers IDs of checked nodes
 * @param {} nodes 
 * @param {} checkedNodes 
 * @returns {} 
 */
function checkedNodeIds(nodes, checkedNodes) {
    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].checked) {
            checkedNodes.push(nodes[i].id);
        }

        if (nodes[i].hasChildren) {
            checkedNodeIds(nodes[i].children.view(), checkedNodes);
        }
    }
}

//#endregion PAGE NEW

//#region PAGE GRIDLIST

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var fillKendoGridRole = function (elem) {
    var sortDirection = $("#search").find("#SortDirection").find(":selected").val();
    var sortMember = $("#search").find("#SortMember").find(":selected").val();
    var pageSize = $("#search").find("#PageSize").find(":selected").val();
    var term = $("#search").find("#Term").val();

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/role/getlistajax",
                data: { sortDirection: sortDirection, sortMember: sortMember, pageSize: pageSize, term: term },
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
                    "Name": { type: "string" }
                }
            }
        },
        error: function (e) {
            dangerNoty("خطا در ارسال اطلاعات بوجود آمده است");
        },
        pageSize: pageSize,
        sort: { field: "Id", dir: "desc" },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    });

    $(elem).kendoGrid({
        dataSource: dataSource,
        autoBind: true,
        scrollable: false,
        pageable: true,
        columns: [
            { field: "Name", title: "عنوان", headerAttributes: { style: "text-align: center;" } },
            {
                field: "",
                template: '<div style="display: inline-block;">' +
                    '<a style="margin: 2px; width: 22px;" class="button primary" href="edit/#= Id #" >' +
                    '<i class="fa fa-pencil-square-o"></i>' +
                    '</a>' +
                    '</div>' +
                    '<div style="display: inline-block;">' +
                    '<a style="margin: 2px; width: 22px;" class="button primary" data-param="#= Id #" href="\\#" onClick="deleteRoleConfirm(this)">' +
                    '<i class="fa fa-trash-o"></i>' +
                    '</a>' +
                    '</div>' +
                    '</div>'
            }
        ]
    });
}

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var deleteRoleConfirm = function (elem) {
    var id = $(elem).data('param');

    $("#deleteModal").kendoWindow({
        title: "آیا از حذف این آیتم اطمینان دارید؟",
        visible: false,
        modal: true,
        resizable: false,
        draggable: false,
        height: 50,
        width: 300,
        actions:[]
    });

    $("#deleteModal").html('<div style="display: inline-block;">' +
                                '<button style="margin-left: 10px;" class="button primary" onClick="removeRole(this)" data-param="' + id + '" >' +
                                    '<span>حذف</span>' +
                                '</button>' +
                            '</div>' +
                            '<div style="display: inline-block;">' +
                                '<button style="margin-left: 10px;" class="button primary" onClick="hideDeleteRoleConfirm()">' +
                                    '<span>انصراف</span>' +
                                '</button>' +
                            '</div>');

    $("#deleteModal").data("kendoWindow").center().open();
}

/**
 * 
 * @returns {} 
 */
var hideDeleteRoleConfirm = function() {
    $("#deleteModal").data("kendoWindow").close();
}

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var removeRole = function (elem) {
    var id = $(elem).data('param');

    $.post("/role/deleteajax", { id: id }, function (e) {
        hideDeleteRoleConfirm();
        successNoty("نقش با موفقیت حذف شد");
        onAjaxLoadNavigator();
    });
}

//#endregion PAGE GRIDLIST

//#region EDIT PAGE

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var fillKendoTreeEditRole = function (elem) {
    debugger;
    var roleId = $('#Id').val();

    $.ajax({
        url: "/role/GetRolePermissionListAjax",
        data: { id: roleId },
        type: "GET",
        dataType: "json",
        complete: function (xhr, status) {
            debugger;
            console.log(xhr);
            var responseText = JSON.stringify(xhr.responseJSON);
            var dataSource = toKendoCheckBoxFlatDataSource(responseText);
            dataSource = JSON.parse(dataSource);

            $(elem).kendoTreeView({
                checkboxes: {
                    checkChildren: true
                },
                loadOnDemand: false,
                dataSource: toHierarchicalDataSource(dataSource, "id", "parent", null),
                check: function (e) {
                    var checkedNodes = [];
                    var treeView = $(elem).data("kendoTreeView");
                    var message;

                    checkedNodeIds(treeView.dataSource.view(), checkedNodes);

                    if (checkedNodes.length > 0) {
                        //var permissions = JSON.stringify(checkedNodes);
                        $('#permissions').val(checkedNodes.join(","));
                        //message = "IDs of checked nodes: " + checkedNodes.join(",");
                    } else {
                        //message = "No nodes checked.";
                    }

                    //$("#result").html(message);
                }
            });
        }
    });
}

//#endregion EDIT PAGE