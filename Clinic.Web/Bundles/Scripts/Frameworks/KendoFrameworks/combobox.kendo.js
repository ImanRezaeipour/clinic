; (function ($) {
    $.fn.appCombobox = function (options) {
        var self = this;
        var elem = $(this);

        var defaults = {
            dataSourceUrl: "",
            onSelect: function(e) {}
        }

        var settings = $.extend({}, defaults, options);

        var comboboxOptions = {
            dataTextField: "Text",
            dataValueField: "Value",
            filter: "contains",
            suggest: true,
            index: 0,
            select: function(e) {
                settings.onSelect(e);
            }
        };

        var ajaxOptions = {
            url: settings.dataSourceUrl,
            complete: function (xhr, status) {
                comboboxOptions.dataSource = xhr.responseJSON.Data;
                //$(self).wrap("<div class='k-rtl'></div>");
                $(self).kendoComboBox(comboboxOptions);
            }
        }

        $.ajax(ajaxOptions);

    }
}(jQuery))