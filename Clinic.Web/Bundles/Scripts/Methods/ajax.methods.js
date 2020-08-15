$(document).ready(function () {
    onAjaxLoadNavigator();
});

function onAjaxLoadNavigator() {
    $("[data-on-load='initLike(this)']").each(function () {
        initLike(this);
    });

    $("[data-on-load='justLoad(this)']").each(function () {
        justLoad(this);
    });

    $("[data-replace]").each(function () {
        if ($(this).data("replace-event") === "load")
            ajaxReplace(this);
    });

    $("[data-inner]").each(function () {
        debugger;
        var e = $(this).data("inner-event");
        if (e === "load")
            ajaxInner(this);
    });
}

function ajaxReplace(elem) {
    $(elem).data("replace-event", "loaded");
    var action = $(elem).data("replace");
    $.get(action, function(data) {
        $(elem).replaceWith(data);
    });
    onAjaxLoadNavigator();
    //onKendoLoadNavigator();
}

function ajaxInner(elem) {
    debugger;
    $(elem).data("inner-event", "loaded");
    var e = $(elem).data('inner-event');
    var action = $(elem).data('inner');
    $.ajax({
        url: action,
        data: {},
        type: 'Post',
        dataType: 'html',
        complete:function(xhr, status) {
            $(elem).empty();
            $(elem).append(xhr.responseText);
            onAjaxLoadNavigator();
            //onKendoLoadNavigator();
        }
    });
    //onAjaxLoadNavigator();
    //onKendoLoadNavigator();
}



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
        data:{id:id},
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
        data:{id:id},
        type: "POST",
        dataType: "json",
        success: function(data) {
            $(tag).toggleClass("fa-heart-o fa-heart");

            $.ajax({
                url: count,
                data: { id: id },
                type:"POST",
                dataType: "json",
                success: function (data) {
                    $(container).text(data);
            }
            });

        }

})
}

function setMap(tag) {
    var container = $(tag).data('container');
    var latitude = $(tag).data('latitude');
    var longitude = $(tag).data('longitude');
    var hiddenLatitude = $(tag).data('hidden-latitude');
    var hiddenLongitude = $(tag).data('hidden-longitude');
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

    $(hiddenLatitude).val(location.lat);
    $(hiddenLongitude).val(location.lng);




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
                $(hiddenLatitude).val(pos.lat);
                $(hiddenLongitude).val(pos.lng);
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
        $(hiddenLatitude).val(event.latLng.lat);
        $(hiddenLongitude).val(event.latLng.lng);
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

function justLoad(tag) {
    var url = $(tag).data('url');
    var container = $(tag).data('container');
    $(container).load(url);
}

function justLoadParam(tag) {
    debugger;
    var url = $(tag).data('url');
    $.get(url, function(data) {
            $(tag).replaceWith(data);
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

function addSpecOption(event, tag) {
        event.preventDefault();
        debugger;
        var items = $(tag).data('items');
        var item = $(tag).data('item');
        var index = $(item).last().data("index");

        $(item).first().clone().appendTo(items);
        $(item).last().attr("data-index", (parseInt(index) + 1));

        $(item).last().find('*').each(function (key, value) {
            if ($(this).attr('value') != null)
                $(this).val('');
            if ($(this).attr('name') != null)
                $(this).attr('name', $(this).attr('name').replace('\[0\]', '\[' + (parseInt(index) + 1) + '\]'));
            if ($(this).attr('id') != null)
                $(this).attr('id', $(this).attr('id').replace('_0_', '_' + (parseInt(index) + 1) + '_'));
            if ($(this).attr('for') != null)
                $(this).attr('for', $(this).attr('for').replace('_0_', '_' + (parseInt(index) + 1) + '_'));
            if ($(this).attr('data-valmsg-for') != null)
                $(this).attr('data-valmsg-for', $(this).attr('data-valmsg-for').replace('\[0\]', '\[' + (parseInt(index) + 1) + '\]'));
            if ($(this).attr('onclick') != null)
                $(this).attr('onclick', $(this).attr('onClick').replace('0', (parseInt(index) + 1)));
        });
    }

function GetDropDownData(tag) {
        var url = $(tag).data('url');
        $.ajax({
            type: "GET",
            url: Variables.GetDropDown,
            data: { categoryId: selectedNode },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (res) {
                $.each(res, function (key, value) {
                    for (var i = 0; i < res.Result.length; i++) {
                        var resu = res.Result[i];
                        $('#SpecificationId').append($("<option></option>").attr("value", resu.Id).text(resu.Title));
                    }
                });
            }
            ,
            failure: function (res) {
                alert("Failed!");
            },
            error: function (res) {
                alert('Error!');
            }
        });
    };

function deleteTag(event, index, selector) {
        event.preventDefault();
        if(index != 0)
            $(selector).remove("[data-index='" + index + "']");
    }

