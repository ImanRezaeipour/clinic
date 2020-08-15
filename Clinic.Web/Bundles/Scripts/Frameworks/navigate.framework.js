; (function ($) {
    $.fn.appNavigate = function ($options) {
        var $self = this;
        var $elem = $(this);

        var $defaults = {
            debug: false
        }

        var $settings = $.extend({}, $defaults, $options);

        function toTitleCase(str) {
            return str.replace(/(?:^|\s)\w/g, function (match) {
                return match.toUpperCase();
            });
        }

        function callFunction(elem, name, evt) {
            var functionFullName =  name + "_On" + toTitleCase(evt);
            var functionInvoker = window[functionFullName];
            if (typeof functionInvoker === "function") {
                if ($settings.debug === true) {
                    console.log(functionFullName + " for " + elem + " invoked.");
                }
                if (evt === "load") {
                    functionInvoker(elem);
                } else {
                    $(elem).bind(evt, function (event) {
                        functionInvoker(elem, event);
                    });
                }
            }
        }

        var $navigatorOptions = {
            debug: $settings.debug,
            events: ["load", "click", "change", "keypress", "mouseleave", "mouseenter", "mouseover"],
            tags: ["input", "video"]
        };

        $.each($navigatorOptions.events, function (index, value) {
                $($self).find("[data-on-" + value + "]").each(function() {
                    var functionName = $(this).data("on-" + value);
                    callFunction(this, functionName, value);
                });
            });

        $.each($navigatorOptions.tags, function (tagIndex, tagValue) {
            $($self).find(tagValue).each(function () {
                var $selfTag = this;
                $.each($navigatorOptions.events, function (index, value) {
                    var functionName = tagValue + "Tags";
                    callFunction($selfTag, functionName, value);
                });
            });
        });

        return $elem;
    }
}(jQuery))