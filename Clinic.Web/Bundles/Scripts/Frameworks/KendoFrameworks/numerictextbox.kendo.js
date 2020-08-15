;(function ($) {
    $.fn.appNumeric = function (options) {
        var self = this;
        var elem = $(this);

        var defaults = {
            onChange: function(value) {

            }
        }

        var settings = $.extend({}, defaults, options);

        var numericOptions = {
            format: "{0:n}",
            decimals: 0,
            step: 1,
            change: function() {
                var value = this.value();
                settings.onChange(value);
            }
        };

        $(self).kendoNumericTextBox(numericOptions);

        return elem;
    }
}(jQuery))