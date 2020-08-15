; (function ($) {
    $.fn.appWindow = function (options) {
        var self = this;
        var elem = $(this);

        var defaults = {
            contentUrl: "",
            title: "",
            widthPercent: 80,
            heigthPercent: 80,
            onComplete: function() { }
        }

        var settings = $.extend({}, defaults, options);

        $.ajax({
            url: settings.contentUrl,
            dataType: "html",
            complete: function (xhr, status) {
                $(self).empty().append(xhr.responseText);
                $(self).appNavigate();
                settings.onComplete();
                buildWindow();
            }
        });

        function buildWindow() {
            var windowOptions = {
                actions: ["Close"],
                iframe: false,
                modal: true,
                width: window.innerWidth *(settings.widthPercent/100), 
                height: window.innerHeight * (settings.heigthPercent/100),
                draggable: false,
                resizable: false,
                title: settings.title,
                scrollable: true,
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