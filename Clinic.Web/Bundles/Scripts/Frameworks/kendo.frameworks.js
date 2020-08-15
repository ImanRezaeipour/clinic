var setPersianCulture = function() {
    kendo.culture("fa-IR");
}


var toHierarchicalDataSource = function(data, idField, foreignKey, rootLevel) {
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

var toKendoFlatDataSource = function(data) {
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

var expandAndSelectNode = function(treeView, id) {
    var kendoTreeview = $(treeView).data("kendoTreeView");
    kendoTreeview.expandTo(id);
    var dataItem = kendoTreeview.dataSource.get(id);
    var element = kendoTreeview.findByUid(dataItem.uid);
    kendoTreeview.select(element);
}

//function fillKendoTreeCategory(tag) {
//    debugger;
//    var url = $(tag).data('url');
//    var container = $(tag).data('container');
//    var hidden = $(tag).data('hidden');
//    var id = $(hidden).val();
//    $.ajax({
//        url: url,
//        data: {},
//        type: "GET",
//        dataType: "json",
//        complete: function (xhr, status) {
//            console.log(xhr);
//            var dataSource = toKendoFlatDataSource(xhr.responseText);
//            dataSource = JSON.parse(dataSource);
//            $(container).kendoTreeView({
//                loadOnDemand: false,
//                dataSource: toHierarchicalDataSource(dataSource, "id", "parent", null),
//                select:
//                    function (e) {
//                        $(hidden).val($(container).getKendoTreeView().dataItem(e.node).id);
//                    }
//            });
//            if (id !== "")
//                expandAndSelectNode(container, id);
//        }
//    });
//}




//function deleteConfirm(tag) {
//    var id = $(tag).data('id');
//    var url = $(tag).data('url');
//    var modal = $(tag).data('modal');
//    $(modal).kendoWindow({
//        title: "تایید حذف",
//        visible: false,
//        modal: true,
//        resizable: false,
//        scrollable: false,
//        draggable: false,
//        actions: [
//            "Close"
//        ]
//    });
//    $.get(url, { id: id }, function (data) {
//        $(modal).html(data);
//        $(modal).data("kendoWindow").center().open();
//    });
//}

//function showImageModal(tag) {
//    var image = $(tag).attr('src');
//    var modal = $(tag).data('modal');
//    $(modal).kendoWindow({
//        title: "نمایش عکس",
//        visible: false,
//        modal: true,
//        resizable: false,
//        scrollable: false,
//        draggable: false,
//        actions: [
//            "Close"
//        ]
//    });
//    $(modal).html('<div class="panel panel-default">' +
//                        '<div class="panel-body">' +
//                           '<div class="row">' +
//                                '<img src="' + image + '" height="400px" width="400px"/>' +
//                            '</div>' +
//                        '</div>' +
//                    '</div>');
//    $(modal).data("kendoWindow").center().open();
//}

//function remove(tag) {
//    var url = $(tag).data('url');
//    var callbackUrl = $(tag).data('callback-url');
//    var id = $(tag).data('id');
//    var message = $(tag).data('message');
//    $.get(url, { id: id, deleted: true }, function () {
//        window.location.href = callbackUrl;
//        successNoty(message);
//    });
//}

//function fillKendoEditor(tag) {
//    //var hidden = $(tag).data('hidden');
//    // var value1 = $(tag).data('value');
//    // var value = $(value1).val();
//    $(tag).kendoEditor({
//        tools: [
//            "bold", "italic", "underline", "strikethrough",
//            "justifyLeft", "justifyCenter", "justifyRight", "justifyFull",
//            "insertUnorderedList", "insertOrderedList", "indent", "outdent",
//            "createLink", "unlink", "insertImage", "insertFile",
//            "subscript", "superscript",
//            "createTable", "addRowAbove", "addRowBelow", "addColumnLeft", "addColumnRight", "deleteRow", "deleteColumn",
//            "print"
//        ],
//        // name:'Body',
//        //value:value,
//        //encoded: true,
//        imageBrowser: {
//                messages: {
//                    dropFilesHere: "فایل‌های خود را به اینجا کشیده و رها کنید"
//                    },
//            transport: {
//                read: {
//                    url: "/File/ListFromUpload",
//                    dataType: "json",
//                    contentType: 'application/json; charset=utf-8',
//                    type: 'GET',
//                    cache: false
//                },
//                destroy: {
//                    url: "/File/DeleteFromUpload",
//                    type: "POST"
//                },
//                create: {
//                    url: "/File/CreateFromUpload",
//                    type: "POST"
//                },
//                thumbnailUrl: "/File/GetFileFromThumb",
//                uploadUrl: "/File/SaveFromUpload",
//                imageUrl: "/File/GetFileFromUpload?path={0}"
//            }
//        },
//        fileBrowser: {
//            messages: {
//                dropFilesHere: "فایل‌های خود را به اینجا کشیده و رها کنید"
//            },
//            transport: {
//                read: {
//                    url: "/File/ListFromUpload",
//                    dataType: "json",
//                    contentType: 'application/json; charset=utf-8',
//                    type: 'GET',
//                    cache: false
//                },
//                destroy: {
//                    url: "/File/DeleteFromUpload",
//                    type: "POST"
//                },
//                create: {
//                    url: "/File/CreateFromUpload",
//                    type: "POST"
//                },
//                uploadUrl: "/File/SaveFromUpload",
//                fileUrl: "/File/GetFileFromUpload?path={0}"
//            }
//        }
//    });

//}

//function fillKendoGridCategory(tag) {
//    debugger;
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var imageUrl = $(tag).data('image-url');
//    var imagePath = $(tag).data('image-path');
//    var imageModal = $(tag).data('image-modal');

//    var search = $(tag).data('search');
//    var sortDirection = $(search).find('#SortDirection').find(":selected").val();
//    var sortMember = $(search).find('#SortMember').find(":selected").val();
//    var pageSize = $(search).find('#PageSize').find(":selected").val();

//    var term = $(search).find('#Term').val();


//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                data: { sortDirection: sortDirection, sortMember: sortMember, term: term },
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "ImageFileName": { type: "string" },
//                    "Title": { type: "string" },
//                    "IsActive": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: pageSize,
//        //sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });
//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: false,
//        reorderable: false,
//        columns: [

//            {
//                field: "",
//                template:
//                        '<img src="'+imagePath+'//#= ImageFileName #" height="40px" width="40px" onClick=showImageModal(this) ' +
//                        'data-modal="\\' + imageModal + '" ' +
//                            '/>'
//                , title: "عکس"
//            },
//            { field: "Title", title: "عنوان" },
//            {
//                field: "IsActive", title: "وضعیت",
//                template: '<input type="checkbox" checked="#= IsActive #" />'
//            },
//            {
//                field: "",
//                template:
//                    '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//            }
//        ]
//    });
//}

//function fillKendoUpload(elem) {
//    debugger;
//    $(elem).data('file-event', 'loaded');
//    var action = $(elem).data('file');
//    var multiple = $(elem).data('file-multiple');
//    var field = $(elem).data('file-for');
//    var preview = $(elem).data('file-preview');
//    var saveUrl = $(elem).data('save-url');
//    var removeUrl = $(elem).data('remove-url');
//    var name = $(elem).attr('name');
//    $.ajax({
//        url: action,
//        data: {},
//        type: "GET",
//        dataType: "json",
//        success: function (data) {
//            var json = JSON.stringify(data);
//            var newJson = json.replace(/"([\w]+)":/g, function ($0, $1) {
//                return ('"' + $1.toLowerCase() + '":');
//            });
//            var files = JSON.parse(newJson);
//            if(files === [])
//                files = null;
//            $(elem).kendoUpload({
//                name: name,
//                multiple: multiple,
//                showFileList: true,
//                async: {
//                    saveUrl: saveUrl,
//                    removeUrl: removeUrl,
//                    autoUpload: true
//                },
//                files: files,
//                select: function (e) {
//                    if (multiple === false)
//                        $(preview).empty();
//                    var fileReader = new FileReader();
//                    fileReader.onload = function (event) {
//                        var mapImage = event.target.result;
//                        $(preview).append('<img id="' + e.files[0].uid + '" src="' + mapImage + '" width="110px" height="110px">');
//                    }
//                    fileReader.readAsDataURL(e.files[0].rawFile);
//                },
//                success: function (e) {
//                    if (e.operation !== 'remove')
//                        if (multiple === false)
//                            $(preview).append('<input type="hidden" name="' + field + '" id="' + e.files[0].uid + '" value="' + e.response[0].Name + '">');
//                        else {
//                            if ($(preview).find('input').length === 0) {
//                                $(preview).append('<input data-index="0" type="hidden" id="' + e.files[0].uid + '" name="' + field.replace('*', '0') + '" value="' + e.response[0].Name + '">');
//                            }
//                            else {
//                                var lastIndex = $(preview).find('input').last().data('index');
//                                var index = parseInt(lastIndex) + 1;
//                                $(preview).append('<input data-index="' + index + '" type="hidden" id="' + e.files[0].uid + '" name="' + field.replace('*', index) + '" value="' + e.response[0].Name + '">');
//                            }
//                        }
//                    else {
//                        $('input').remove('[id="' + e.files[0].id + '"]');
//                        $('input').remove('[id="' + e.files[0].uid + '"]');
//                    }
//                },
//                remove: function (e) {
//                    $('img').remove('[id="' + e.files[0].id + '"]');
//                    $('img').remove('[id="' + e.files[0].uid + '"]');
//                }
//            });
//            $.each(files, function (i, file) {
//                $(preview).append('<img id="' + file.id + '"  src="' + file.path + '"  width="110px" height="110px">');
//                if (multiple === false)
//                    $(preview).append('<input type="hidden" id="' + file.id + '" name="' + field + '" value="' + file.name + '">');
//                else {
//                    if ($(preview).child('input').last().data('index') == null) {
//                        $(preview).append('<input data-index="0" type="hidden" id="' + file.id + '" name="' + field.replace('*', '0') + '" value="' + file.name + '">');
//                    }
//                    else {
//                        var lastIndex = $(preview).child('input').last().data('index');
//                        var index = parseInt(lastIndex) + 1;
//                        $(preview).append('<input data-index="' + index + '" type="hidden" id="' + file.id + '" name="' + field.replace('*', index) + '" value="' + file.name + '">');
//                    }
//                }
//            });
//        }
//    });
//}

//function fillKendoGridCompany(tag) {
//    debugger;
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    //var parameter = $(tag).data('parameter');
//    var deleteModal = $(tag).data('delete-modal');
//    var imageModal = $(tag).data('image-modal');
//    var imagePath = $(tag).data('image-path');
//    var detailUrl = $(tag).data('detail-url');
//    var search = $(tag).data('search');
//    var sortDirection = $(search).find('#SortDirection').find(":selected").val();
//    var sortMember = $(search).find('#SortMember').find(":selected").val();
//    var pageSize = $(search).find('#PageSize').find(":selected").val();
//    var state = $(search).find('#State').find(":selected").val();
//    var term = $(search).find('#Term').val();
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                data: { sortDirection: sortDirection, sortMember: sortMember, term: term, state: state },
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "LogoFileName": { type: "string" },
//                    "Title": { type: "string" },
//                    "PhoneNumber": { type: "string" },
//                    "MobileNumber": { type: "string" },
//                    "State": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: pageSize,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        {
//            field: "",
//            template:
//                    '<img src="' + imagePath + '//#= LogoFileName #" height="40px" width="40px" onClick=showImageModal(this) ' +
//                    'data-modal="\\' + imageModal + '" ' +
//                        '/>'
//                , title: "عکس"
//        },
//        { field: "Title", title: "برند" },
//        { field: "PhoneNumber", title: "تلفن" },
//        { field: "MobileNumber", title: "موبایل" },
//        { field: "State", title: "وضعیت" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                         '</button>'+
//                    '<button style="margin: 2px;" class="btn btn-md btn-warning-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-url="' + detailUrl + '" ' +
//                        'onClick="redirect(this)">' +
//                        '<i class="ti ti-eye"></i>' +
//                    '</button>'

//        }
//        ]
//    });
//}

//function fillKendoGridCategoryReview(tag) {
//    debugger;
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var search = $(tag).data('search');
//    var sortDirection = $(search).find('#SortDirection').find(":selected").val();
//    var sortMember = $(search).find('#SortMember').find(":selected").val();
//    var pageSize = $(search).find('#PageSize').find(":selected").val();
//    var isActive = $(search).find('#IsActive').find(":selected").val();
//    var term = $(search).find('#Term').val();
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                data: { sortDirection: sortDirection, sortMember: sortMember, term: term, isActive: isActive },
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "CategoryTitle": { type: "string" },
//                    "Title": { type: "string" },
//                    "IsActive": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: pageSize,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        { field: "CategoryTitle", title: "نام پاساژ" },
//        { field: "Title", title: "عنوان" },
//        { field: "IsActive", title: "فعال" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function fillKendoGridCompanyAttachment(tag) {
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var imageModal = $(tag).data('image-modal');
//    var callurl = $(tag).data('call-url');
//    var rejecturl = $(tag).data('reject-url');
//    var message = $(tag).data('message');
//    var callbackurl = $(tag).data('callback-url');
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "FullName": { type: "string" },
//                    "Mobile": { type: "string" },
//                    "UserName": { type: "string" },
//                    "State": { type: "string" },
//                    "RejectDescription": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: 20,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//            {
//                field: "",
//                template:
//                        '<img src="/Files/#= FileName #" height="40px" width="40px" onClick=showImageModal(this) ' +
//                        'data-modal="\\' + imageModal + '" ' +
//                            '/>'
//                , title: "عکس"
//            },
//        { field: "FullName", title: "نام و نام خانوادگی" },
//            { field: "Mobile", title: "شماره تماس" },
//            { field: "UserName", title: "فعال" },
//            { field: "State", title: "وضعیت" },
//             { field: "RejectDescription", title: "توضیحات عدم تائید" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-green-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-url="' + callurl + '" ' +
//                        'data-message="' + message + '" ' +
//                        'data-callback-url="' + callbackurl + '" ' +
//                        'onClick="callAjax(this)">' +
//                        '<i class="ti ti-check"></i>' +
//                    '</button>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-red-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-url="' + rejecturl + '" ' +
//                        'data-message="' + message + '" ' +
//                        'data-callback-url="' + callbackurl + '" ' +
//                        'onClick="callAjax(this)">' +
//                        '<i class="ti ti-power-off"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function fillKendoGridCompanyImage(tag) {
//    debugger
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var imageModal = $(tag).data('image-modal');
//    var imagePath = $(tag).data('image-path');
//    var callurl = $(tag).data('call-url');
//    var rejecturl = $(tag).data('reject-url');
//    var message = $(tag).data('message');
//    var callbackurl = $(tag).data('callback-url');
//    var search = $(tag).data('search');
//    var sortDirection = $(search).find('#SortDirection').find(":selected").val();
//    var sortMember = $(search).find('#SortMember').find(":selected").val();
//    var pageSize = $(search).find('#PageSize').find(":selected").val();
//    var state = $(search).find('#State').find(":selected").val();
//    var term = $(search).find('#Term').val();

//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                data: { sortDirection: sortDirection, sortMember: sortMember, term: term, state: state },
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "Title": { type: "string" },
//                    "FileName": { type: "string" },
//                    "FullName": { type: "string" },
//                    "CompanyMobileNumber": { type: "string" },
//                    "CompanyTitle": { type: "string" },
//                    "State": { type: "string" },
//                    "RejectDescription": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: pageSize,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//            {
//                field: "",
//                template:
//                        '<img src="' + imagePath + '/#= FileName #" height="40px" width="40px" onClick=showImageModal(this) ' +
//                        'data-modal="\\' + imageModal + '" ' +
//                            '/>'
//                , title: "عکس"
//            },
//             { field: "Title", title: "عنوان عکس" },
//             { field: "FullName", title: "نام و نام خانوادگی" },
//             { field: "CompanyTitle", title: "نام شرکت" },
//             { field: "CompanyMobileNumber", title: "شماره تماس" },
//             { field: "State", title: "وضعیت" },
//             { field: "RejectDescription", title: "توضیحات عدم تائید" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                           '</button>'+
//                    '<button style="margin: 2px;" class="btn btn-md btn-green-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-url="' + callurl + '" ' +
//                        'data-message="' + message + '" ' +
//                        'data-callback-url="' + callbackurl + '" ' +
//                        'onClick="callAjax(this)">' +
//                        '<i class="ti ti-check"></i>' +
//                    '</button>'+
//                    '<button style="margin: 2px;" class="btn btn-md btn-red-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-url="' + rejecturl + '" ' +
//                        'data-message="' + message + '" ' +
//                        'data-callback-url="' + callbackurl + '" ' +
//                        'onClick="callAjax(this)">' +
//                        '<i class="ti ti-power-off"></i>' +
//                    '</button>'


//        }
//        ]
//    });
//}

//function deleteConfirm(tag) {
//    var id = $(tag).data('id');
//    var url = $(tag).data('url');
//    var modal = $(tag).data('modal');
//    $(modal).kendoWindow({
//        title: "تایید حذف",
//        visible: false,
//        modal: true,
//        resizable: false,
//        scrollable: false,
//        draggable: false,
//        actions: [
//            "Close"
//        ]
//    });
//    $.get(url, { id: id }, function (data) {
//        $(modal).html(data);
//        $(modal).data("kendoWindow").center().open();
//    });
//}

//function redirect(tag) {
//    var id = $(tag).data('id');
//    var url = $(tag).data('url');
//    var dd = url + '/' + id;
//    window.open(url+'/'+id) ;



//}

//function callAjax(tag) {
//    var id = $(tag).data('id');
//    var url = $(tag).data('url');
//    var message = $(tag).data('message');
//    var callbackurl = $(tag).data('callback-url');


//    $.ajax({
//        url: url,
//        data: { id: id },
//        type: "Get",
//        dataType: "Json",
//        success: function (data) {
//            window.location.href = callbackurl;
//            successNoty(message);
//        }

//    });


//}

//function fillKendoGridCompanyQuestion(tag) {
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model:
//            {
//                fields:
//                        {
//                            "Id": { type: "string" },
//                            "Title": { type: "string" },
//                            "Body": { type: "string" },
//                            "State": { type: "string" },
//                            "CreatedOn": { type: "string" },
//                            "CreatorFullName": { type: "string" }
//                        }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: 20,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        { field: "Title", title: "عنوان" },
//        { field: "Body", title: "توضیحات" },
//        { field: "State", title: "وضعیت" },
//        { field: "CreatedOn", title: "تاریخ ایجاد" },
//        { field: "CreatorFullName", title: "نام و نام خانوادگی" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function fillKendoGridCompanyReview(tag) {
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "CompanyTitle": { type: "string" },
//                    "Active": { type: "string" },
//                    "State": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: 20,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        { field: "CategoryTitle", title: "نام پاساژ" },
//        { field: "Title", title: "عنوان" },
//        { field: "Active", title: "توضیح" },
//         { field: "State", title: "وضعیت" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function fillKendoGridProduct(tag) {
//    debugger;

//    var url = $(tag).data('url');
//    var parameter = $(tag).data('parameter');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var search = $(tag).data('search');
//    var sortDirection = $(search).find('#SortDirection').find(":selected").val();
//    var sortMember = $(search).find('#SortMember').find(":selected").val();
//    var pageSize = $(search).find('#PageSize').find(":selected").val();
//    var stateType = $(search).find('#StateType').find(":selected").val();
//    var term = $(search).find('#Term').val();
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                data: { sortDirection: sortDirection, sortMember: sortMember, term: term, stateType: stateType },
//                dataType: "json",
//                data:parameter,
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "Title": { type: "string" },
//                    "MobileNumber": { type: "string" },
//                    "PhoneNumber": { type: "string" },
//                    "TitleCategory": { type: "string" },
//                    "CompanyTitle": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: pageSize,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//           { field: "Title", title: "عنوان" },
//        { field: "CompanyTitle", title: "شرکت" },
//        { field: "MobileNumber", title: "شماره همراه" },
//        { field: "PhoneNumber", title: "شماره تلفن" },
//        { field: "TitleCategory", title: "محصول" },
//            {
//                field: "",
//                template:
//                     '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                        '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                            'data-id="#= Id #" ' +
//                            'data-modal="\\' + deleteModal + '" ' +
//                            'data-url="' + deleteUrl + '" ' +
//                            'onClick="deleteConfirm(this)">' +
//                            '<i class="ti ti-trash"></i>' +
//                        '</button>'
//            }
//        ]
//    });
//}

//function fillKendoGridSpecification(tag) {
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var search = $(tag).data('search');
//    var sortDirection = $(search).find('#SortDirection').find(":selected").val();
//    var sortMember = $(search).find('#SortMember').find(":selected").val();
//    var pageSize = $(search).find('#PageSize').find(":selected").val();
//    var term = $(search).find('#Term').val();
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                data: { sortDirection: sortDirection, sortMember: sortMember, term: term },
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" }, //تعیین نوع فیلد برای جستجوی پویا مهم است
//                    "CategoryTitle": { type: "string" },
//                    "Title": { type: "string" },
//                    "Type": { type: "string" },
//                    "Description": { type: "string" },
//                    "Order": { type: "number" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: pageSize,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        { field: "CategoryTitle", title: "دسته" },
//        { field: "Title", title: "عنوان" },
//        { field: "Type", title: "نوع" },
//        { field: "Description", title: "توضیح" },
//        { field: "Order", title: "الویت" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function fillKendoGridSpecificationOption(tag) {
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "CategoryTitle": { type: "string" },
//                    "SpecificationTitle": { type: "string" },
//                    "Title": { type: "string" },
//                    "Description": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: 20,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        { field: "CategoryTitle", title: "دسته" },
//            { field: "SpecificationTitle", title: "ویژگی" },
//            { field: "Title", title: "عنوان" },
//            { field: "Description", title: "توضیح" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function fillKendoGridUser(tag) {
//    debugger;
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var search = $(tag).data('search');
//    var sortDirection = $(search).find('#SortDirection').find(":selected").val();
//    var sortMember = $(search).find('#SortMember').find(":selected").val();
//    var pageSize = $(search).find('#PageSize').find(":selected").val();
//    var isActive = $(search).find('#IsActive').find(":selected").val();
//    var isBan = $(search).find('#IsBan').find(":selected").val();
//    var isVerify = $(search).find('#IsVerify').find(":selected").val();
//    var term = $(search).find('#Term').val();
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                data: { sortDirection: sortDirection, sortMember: sortMember, term: term, isActive: isActive, isBan: isBan, isVerify: isVerify },
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "UserName": { type: "string" },
//                    "IsBan": { type: "string" },
//                    "BannedReason": { type: "string" },
//                    "IsSystemAccount": { type: "string" },
//                    "DisplayName": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: pageSize,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        { field: "UserName", title: "نام کاربری" },
//            { field: "IsBan", title: "مسدود" },
//             { field: "BannedReason", title: "دلیل مسدود شدن" },
//             { field: "DisplayName", title: "نام نمایشی" },
//            { field: "IsSystemAccount", title: "اکانت سیستمی" },


//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function fillKendoGridUserMeta(tag) {
//    var url = $(tag).data('url');
//    var deleteUrl = $(tag).data('delete-url');
//    var deleteModal = $(tag).data('delete-modal');
//    var dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: url,
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                type: "GET"
//            },
//            parameterMap: function (options) {
//                return kendo.stringify(options);
//            }
//        },
//        schema: {
//            data: "Data",
//            total: "Total",
//            model: {
//                fields: {
//                    "Id": { type: "string" },
//                    "AvatarFileName": { type: "string" },
//                    "FullName": { type: "string" },
//                    "Gender": { type: "string" },
//                    "DisplayName": { type: "string" }
//                }
//            }
//        },
//        error: function (e) {
//            alert(e.errorThrown);
//        },
//        pageSize: 20,
//        sort: { field: "Id", dir: "desc" },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $(tag).kendoGrid({
//        dataSource: dataSource,
//        autoBind: true,
//        scrollable: false,
//        pageable: true,
//        sortable: true,
//        reorderable: true,
//        columns: [
//        { field: "AvatarFileName", title: "عکس" },
//            { field: "Gender", title: "جنسیت" },
//             { field: "FullName", title: "نام و نام خانوادگی" },
//            { field: "IsSystemAccount", title: "اکانت سیستمی" },
//        {
//            field: "",
//            template:
//                 '<a style="margin: 2px;" class="btn btn-md btn-primary-alt" href="Edit/#= Id #" ><i class="ti ti-pencil"></i></a>' +
//                    '<button style="margin: 2px;" class="btn btn-md btn-danger-alt" ' +
//                        'data-id="#= Id #" ' +
//                        'data-modal="\\' + deleteModal + '" ' +
//                        'data-url="' + deleteUrl + '" ' +
//                        'onClick="deleteConfirm(this)">' +
//                        '<i class="ti ti-trash"></i>' +
//                    '</button>'
//        }
//        ]
//    });
//}

//function justSearch() {

//}