; (function ($) {
    $.appAjax = function (options) {

        // capture this element
        var self = this;
        var elem = $(this);

        // define defaults
        var defaults = {
            url: "",
            data: {},
            type: "GET",
            complete: function (jqXhr, textStatus) { },
            error: function (jqXhr, textStatus, errorThrown) { },
            success: function (data, textStatus, jqXhr) { },
            beforeSend: function (jqXhr, configs) { }
        }

        var settings = $.extend({}, defaults, options);

        // do work on element
        var ajaxOptions = {
            async: true,
            url: window.appCultureRoute + settings.url,
            data: settings.data,
            type: settings.type,
            method: settings.type,
            dataType: "json",
            beforeSend: function (jqXhr, configs) {
                settings.beforeSend(jqXhr, configs);
            },
            cache: true,
            complete: function (jqXhr, textStatus) {
                settings.complete(jqXhr, textStatus);
            },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            error: function (jqXhr, textStatus, errorThrown) {
                settings.error(jqXhr, textStatus, errorThrown);
            },
            global: true,
            headers: {},
            statusCode: {
                404: function () {
                    alert("Method Not Allowed.");
                }
            },
            success: function (data, textStatus, jqXhr) {
                settings.success(data, textStatus, jqXhr);
            }
        };

        $.ajax(ajaxOptions);

        // return element
        return elem;
    }
}(jQuery))