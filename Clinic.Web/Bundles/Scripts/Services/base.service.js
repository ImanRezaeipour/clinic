function isGuid(value) {
    var regex = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
    var match = regex.exec(value);
    return match != null;
}

function lg(e) {
    console.log(e);
}

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
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

function toKendoCheckBoxFlatDataSource(data) {
    data = JSON.parse(data);
    var temp = JSON.parse('[]');
    $.each(data, function (key, value) {
        temp.push({
            id: value.Id,
            text: value.Title,
            parent: value.ParentId,
            checked: value.IsSelect
        });
    });
    return JSON.stringify(temp);
}

function toKendoRtl(elem) {
    $(elem).wrap('<div class="k-rtl"></div>');
}

function setKendoCulture(elem) {
    kendo.culture("fa-IR");
}

function ajaxReplace(elem) {
    var action = $(elem).data("replace");
    $.ajax({
        url: action,
        data: {},
        type: "POST",
        dataType: "html",
        complete: function (xhr, status) {
            $(elem).replaceWith(xhr.responseText);
            onAjaxLoadNavigator();
        }
    });
}

function ajaxInner(elem) {
    var action = $(elem).data('inner');
    $.ajax({
        url: action,
        data: {},
        type: 'Post',
        dataType: 'html',
        complete: function (xhr, status) {
            $(elem).empty();
            $(elem).append(xhr.responseText);
            onAjaxLoadNavigator();
        }
    });
}

function redirect(tag) {
    var id = $(tag).data('id');
    var url = $(tag).data('url');
    var dd = url + '/' + id;
    window.open(url + '/' + id);
}

function callAjax(tag) {
    var id = $(tag).data('id');
    var url = $(tag).data('url');
    var message = $(tag).data('message');
    var callbackurl = $(tag).data('callback-url');

    $.ajax({
        url: url,
        data: { id: id },
        type: "Get",
        dataType: "Json",
        success: function (data) {
            window.location.href = callbackurl;
            successNoty(message);
        }
    });
}

function toHierarchicalDataSource(data, idField, foreignKey, rootLevel) {
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

//function expandAndSelectNode(treeView, id) {
//    var kendoTreeview = $(treeView).data("kendoTreeView");
//    kendoTreeview.expandTo(id);
//    var dataItem = kendoTreeview.dataSource.get(id);
//    var element = kendoTreeview.findByUid(dataItem.uid);
//    kendoTreeview.select(element);
//}

function loadMap() {
    $("[data-on-load='fillMap(this)']").each(function () {
        var view = $(this).data('view');
        if (view === 'setMap')
            setMap(this);
        if (view === 'getMap')
            getMap(this);
    });
}

function initLike(tag) {
    var loadurl = $(tag).data('load-url');
    var container = $(tag).data('container');
    var count = $(tag).data('count-url');
    var id = $(tag).data('id');
    $.ajax({
        url: loadurl,
        data: { id: id },
        type: "Post",
        dataType: "json",
        success: function (data) {
            if (data === true) {
                $(tag).addClass('fa-heart');
            }
            else {
                $(tag).addClass('fa-heart-o');
            }
            $.ajax({
                url: count,
                data: { id: id },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    $(container).text(data);
                }
            });
        }
    });
}

function toggleLike(tag) {
    debugger;
    var toggleUrl = $(tag).data('toggle-url');
    var container = $(tag).data('container');
    var count = $(tag).data('count-url');
    var id = $(tag).data('id');

    $.ajax(
    {
        url: toggleUrl,
        data: { id: id },
        type: "POST",
        dataType: "json",
        success: function (data) {
            $(tag).toggleClass("fa-heart-o fa-heart");

            $.ajax({
                url: count,
                data: { id: id },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    $(container).text(data);
                }
            });

        }

    })
}

function setMap(tag) {
    var latitude = parseFloat($('#Address_Latitude').val());
    var longitude = parseFloat($('#Address_Longitude').val());

    //alert(latitude);
    
    if (latitude === "0")
        latitude = 0;

    if (longitude === "0")
        longitude = 0;

    var infoWindow = new google.maps.InfoWindow({

    });
    var location = {
        lat: latitude,
        lng: longitude
    };
    var markers = [];
    var map = new google.maps.Map(document.getElementById('UserMetaMap'), {
        center: location,
        zoom: 18
    });
    var marker = new google.maps.Marker({
        position: location,
        map: map
    });

    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
    markers.push(marker);

    $('#Address_Latitude').val(location.lat);
    $('#Address_Longitude').val(location.lng);




    if (latitude === 0) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                map.setCenter(pos);
                var marker = new google.maps.Marker({
                    position: pos,
                    map: map
                });
                for (var i = 0; i < markers.length; i++) {
                    markers[i].setMap(null);
                }
                markers = [];
                markers.push(marker);
                $('#Address_Latitude').val(pos.lat);
                $('#Address_Longitude').val(pos.lng);
            },
                function () {
                    infoWindow.setPosition(map.getCenter());
                    infoWindow.setContent('Error: The Geolocation service failed.');
                });
        } else {
            infoWindow.setPosition(map.getCenter());
            infoWindow.setContent('Error: Your browser doesn\'t support geolocation.');
        }
    }



    map.addListener('click', function (event) {
        debugger;
        var marker = new google.maps.Marker({
            position: event.latLng,
            map: map
        });
        for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(null);
        }
        markers = [];
        markers.push(marker);
        
        $('#Address_Latitude').val(event.latLng.lat());
        $('#Address_Longitude').val(event.latLng.lng());
    }
    );
}

function getMap(tag) {
    var container = $(tag).data('container');
    var latitude = $(tag).data('latitude');
    var longitude = $(tag).data('longitude');
    var infoWindow = new google.maps.InfoWindow({
    });
    var location = {
        lat: latitude,
        lng: longitude
    };
    var markers = [];
    var map = new google.maps.Map(document.getElementById(container), {
        center: location,
        zoom: 18
    });
    var marker = new google.maps.Marker({
        position: location,
        map: map
    });
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
    markers.push(marker);
    if (latitude === 0) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                map.setCenter(pos);
                var marker = new google.maps.Marker({
                    position: pos,
                    map: map
                });
                for (var i = 0; i < markers.length; i++) {
                    markers[i].setMap(null);
                }
                markers = [];
                markers.push(marker);
            },
                function () {
                    infoWindow.setPosition(map.getCenter());
                    infoWindow.setContent('Error: The Geolocation service failed.');
                });
        } else {
            infoWindow.setPosition(map.getCenter());
            infoWindow.setContent('Error: Your browser doesn\'t support geolocation.');
        }
    }
}

function getMapCluster(tag) {
    var locations = $(tag).data('locations');
    var locationMap = [];
    var map = new google.maps.Map(document.getElementById('markerClusterMapCanvas'),
    {
        zoom: 10,
        center: { lat: 35.6988196, lng: 51.3924643 }
    });
    var markers = locationMap.map(function (location, i) {
        return new google.maps.Marker({
            position: location
        });
    });
    var markerCluster = new MarkerClusterer(map,
        markers,
        { imagePath: '/Content/GoogleMap/m' });
    locations.forEach(function (entry) {
        locationMap.push({ lat: x, lng: y });
    });
}

function justPaging(btn) {
    var getData = function (data) {
        data.PageIndex = $(btn).data('page');
        return data;
    }
    var progress = $(btn).data('progress');
    $(btn).closest('.row').hide();
    $(progress).css("display", "block");
    $.ajax({
        type: "POST",
        url: $(btn).data('load-url'),
        data: JSON.stringify(getData($(btn).data('json'))),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (xhr, status) {
            var data = xhr.responseText;
            if (xhr.status == 403) {
                window.location = '/Account/Login';
            } else if (status === 'error' || !data) {
                dangerNoty('خطایی در بارگذاری بوجود آمده است');
            } else {
                if (data == "no-more-info") {
                    infoNoty("اطلاعات بیشتری یافت نشد");
                } else {
                    var $boxes = $(data);
                    $($(btn).data('container')).append($boxes);
                    $(btn).data("page", $(btn).data("page") + 1);
                    $(btn).closest('.row').show();

                }
                $(progress).css("display", "none");
                $("[data-toggle='tooltip']").tooltip();
            }
        }
    });
}



function fillKendoEditor(tag) {
    //var hidden = $(tag).data('hidden');
    // var value1 = $(tag).data('value');
    // var value = $(value1).val();
    $(tag).kendoEditor({
        tools: [
            "bold", "italic", "underline", "strikethrough",
            "justifyLeft", "justifyCenter", "justifyRight", "justifyFull",
            "insertUnorderedList", "insertOrderedList", "indent", "outdent",
            "createLink", "unlink", "insertImage", "insertFile",
            "subscript", "superscript",
            "createTable", "addRowAbove", "addRowBelow", "addColumnLeft", "addColumnRight", "deleteRow", "deleteColumn",
            "print"
        ],
        // name:'Body',
        //value:value,
        //encoded: true,
        imageBrowser: {
            messages: {
                dropFilesHere: "فایل‌های خود را به اینجا کشیده و رها کنید"
            },
            transport: {
                read: {
                    url: "/File/ListFromUpload",
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    type: 'GET',
                    cache: false
                },
                destroy: {
                    url: "/File/DeleteFromUpload",
                    type: "POST"
                },
                create: {
                    url: "/File/CreateFromUpload",
                    type: "POST"
                },
                thumbnailUrl: "/File/GetFileFromThumb",
                uploadUrl: "/File/SaveFromUpload",
                imageUrl: "/File/GetFileFromUpload?path={0}"
            }
        },
        fileBrowser: {
            messages: {
                dropFilesHere: "فایل‌های خود را به اینجا کشیده و رها کنید"
            },
            transport: {
                read: {
                    url: "/File/ListFromUpload",
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    type: 'GET',
                    cache: false
                },
                destroy: {
                    url: "/File/DeleteFromUpload",
                    type: "POST"
                },
                create: {
                    url: "/File/CreateFromUpload",
                    type: "POST"
                },
                uploadUrl: "/File/SaveFromUpload",
                fileUrl: "/File/GetFileFromUpload?path={0}"
            }
        }
    });

}

function fillKendoUpload(elem) {
    debugger;
    $(elem).data('file-event', 'loaded');
    var action = $(elem).data('file');
    var multiple = $(elem).data('file-multiple');
    var field = $(elem).data('file-for');
    var preview = $(elem).data('file-preview');
    var saveUrl = $(elem).data('save-url');
    var removeUrl = $(elem).data('remove-url');
    var name = $(elem).attr('name');
    $.ajax({
        url: action,
        data: {},
        type: "GET",
        dataType: "json",
        success: function (data) {
            var json = JSON.stringify(data);
            var newJson = json.replace(/"([\w]+)":/g, function ($0, $1) {
                return ('"' + $1.toLowerCase() + '":');
            });
            var files = JSON.parse(newJson);
            if (files === [])
                files = null;
            $(elem).kendoUpload({
                name: name,
                multiple: multiple,
                showFileList: true,
                async: {
                    saveUrl: saveUrl,
                    removeUrl: removeUrl,
                    autoUpload: true
                },
                files: files,
                select: function (e) {
                    if (multiple === false)
                        $(preview).empty();
                    var fileReader = new FileReader();
                    fileReader.onload = function (event) {
                        var mapImage = event.target.result;
                        $(preview).append('<img id="' + e.files[0].uid + '" src="' + mapImage + '" width="110px" height="110px">');
                    }
                    fileReader.readAsDataURL(e.files[0].rawFile);
                },
                success: function (e) {
                    if (e.operation !== 'remove')
                        if (multiple === false)
                            $(preview).append('<input type="hidden" name="' + field + '" id="' + e.files[0].uid + '" value="' + e.response[0].Name + '">');
                        else {
                            if ($(preview).find('input').length === 0) {
                                $(preview).append('<input data-index="0" type="hidden" id="' + e.files[0].uid + '" name="' + field.replace('*', '0') + '" value="' + e.response[0].Name + '">');
                            }
                            else {
                                var lastIndex = $(preview).find('input').last().data('index');
                                var index = parseInt(lastIndex) + 1;
                                $(preview).append('<input data-index="' + index + '" type="hidden" id="' + e.files[0].uid + '" name="' + field.replace('*', index) + '" value="' + e.response[0].Name + '">');
                            }
                        }
                    else {
                        $('input').remove('[id="' + e.files[0].id + '"]');
                        $('input').remove('[id="' + e.files[0].uid + '"]');
                    }
                },
                remove: function (e) {
                    $('img').remove('[id="' + e.files[0].id + '"]');
                    $('img').remove('[id="' + e.files[0].uid + '"]');
                }
            });
            $.each(files, function (i, file) {
                $(preview).append('<img id="' + file.id + '"  src="' + file.path + '"  width="110px" height="110px">');
                if (multiple === false)
                    $(preview).append('<input type="hidden" id="' + file.id + '" name="' + field + '" value="' + file.name + '">');
                else {
                    if ($(preview).child('input').last().data('index') == null) {
                        $(preview).append('<input data-index="0" type="hidden" id="' + file.id + '" name="' + field.replace('*', '0') + '" value="' + file.name + '">');
                    }
                    else {
                        var lastIndex = $(preview).child('input').last().data('index');
                        var index = parseInt(lastIndex) + 1;
                        $(preview).append('<input data-index="' + index + '" type="hidden" id="' + file.id + '" name="' + field.replace('*', index) + '" value="' + file.name + '">');
                    }
                }
            });
        }
    });
}

function fillKendoUploadMulti(elem) {
    var action = "/admin/category/GetImageListAjax";
    var preview = "#" + "imagePreview";
    var saveUrl = "/file/SaveFromImageWeb";
    var removeUrl = "/file/remove";
    var field = $(elem).data("param-field");
    var name = $(elem).attr("name");

    $.ajax({
        url: action,
        data: {},
        type: "GET",
        dataType: "json",
        success: function (data) {
            var json = JSON.stringify(data);
            var newJson = json.replace(/"([\w]+)":/g, function ($0, $1) {
                return ('"' + $1.toLowerCase() + '":');
            });
            var files = JSON.parse(newJson);
            if (files === [])
                files = null;

            $(elem).kendoUpload({
                name: name,
                multiple: true,
                showFileList: true,
                async: {
                    saveUrl: saveUrl,
                    removeUrl: removeUrl,
                    autoUpload: true
                },
                files: files,
                select: function (e) {
                    var fileReader = new FileReader();
                    fileReader.onload = function (event) {
                        var mapImage = event.target.result;
                        $(preview).append('<img id="' + e.files[0].uid + '" src="' + mapImage + '" width="110px" height="110px">');
                    }
                    fileReader.readAsDataURL(e.files[0].rawFile);
                },
                success: function (e) {
                    if (e.operation !== 'remove') {
                        if ($(preview).find('input').length === 0) {
                            $(preview).append('<input data-index="0" type="hidden" id="' + e.files[0].uid + '" name="' + field.replace('*', '0') + '" value="' + e.response[0].Name + '">');
                        }
                        else {
                            var lastIndex = $(preview).find('input').last().data('index');
                            var index = parseInt(lastIndex) + 1;
                            $(preview).append('<input data-index="' + index + '" type="hidden" id="' + e.files[0].uid + '" name="' + field.replace('*', index) + '" value="' + e.response[0].Name + '">');
                        }
                    }
                    else {
                        $('input').remove('[id="' + e.files[0].id + '"]');
                        $('input').remove('[id="' + e.files[0].uid + '"]');
                    }
                },
                remove: function (e) {
                    $('img').remove('[id="' + e.files[0].id + '"]');
                    $('img').remove('[id="' + e.files[0].uid + '"]');
                }
            });

            $.each(files, function (i, file) {
                $(preview).append('<img id="' + file.id + '"  src="' + file.path + '"  width="110px" height="110px">');
                if ($(preview).child('input').last().data('index') == null) {
                    $(preview).append('<input data-index="0" type="hidden" id="' + file.id + '" name="' + field.replace('*', '0') + '" value="' + file.name + '">');
                }
                else {
                    var lastIndex = $(preview).child('input').last().data('index');
                    var index = parseInt(lastIndex) + 1;
                    $(preview).append('<input data-index="' + index + '" type="hidden" id="' + file.id + '" name="' + field.replace('*', index) + '" value="' + file.name + '">');
                }
            });
        }
    });
}

function searchGrid(e, elem) {
    // look for window.event in case event isn't passed in
    e = e || window.event;
    if (e.keyCode === 13) {
        onAjaxLoadNavigator();
        return false;
    }
    return true;
}


function getUrlParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}

getUrlEncodedKey = function (key, query) {
    if (!query)
        query = window.location.search;
    var re = new RegExp("[?|&]" + key + "=(.*?)&");
    var matches = re.exec(query + "&");
    if (!matches || matches.length < 2)
        return "";
    return decodeURIComponent(matches[1].replace("+", " "));
}

setUrlEncodedKey = function (key, value, query) {

    query = query || window.location.search;
    var q = query + "&";
    var re = new RegExp("[?|&]" + key + "=.*?&");
    if (!re.test(q))
        q += key + "=" + encodeURI(value);
    else
        q = q.replace(re, "&" + key + "=" + encodeURIComponent(value) + "&");
    q = q.trimStart("&").trimEnd("&");
    return q[0] == "?" ? q : q = "?" + q;
}

String.prototype.trimEnd = function (c) {
    if (c)
        return this.replace(new RegExp(c.escapeRegExp() + "*$"), '');
    return this.replace(/\s+$/, '');
}
String.prototype.trimStart = function (c) {
    if (c)
        return this.replace(new RegExp("^" + c.escapeRegExp() + "*"), '');
    return this.replace(/^\s+/, '');
}

String.prototype.escapeRegExp = function () {
    return this.replace(/[.*+?^${}()|[\]\/\\]/g, "\\$0");
};



var checkNumber = (function (element) {
    $(element).keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
})







