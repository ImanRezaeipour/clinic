; (function ($) {
    $.fn.appTab = function (options) {
        var self = this;
        var elem = $(this);

        var defaults = {
            contentUrl: "",
            title: ""
        }

        var settings = $.extend({}, defaults, options);

        $.ajax({
            url: settings.contentUrl,
            dataType: "html",
            complete: function (xhr, status) {
                $(self).empty().append(xhr.responseText);
                $(self).appNavigate();
                buildWindow();
            }
        });

        function buildWindow() {
            var windowOptions = {
                actions: ["Close"],
                iframe: false,
                modal: true,
                width: window.innerWidth *(80/100), 
                height: window.innerHeight * (80/100),
                draggable: false,
                resizable: false,
                title: settings.title,
                scrollable: false,
                autoFocus: true,
                pinned: true
            };

            $(self).wrap("<div class='k-rtl'></div>");
            $(self).kendoWindow(windowOptions);

            var dialog = $(self).data("kendoWindow");
            dialog.center().open();
        }
       
        return elem;
    }
}(jQuery))