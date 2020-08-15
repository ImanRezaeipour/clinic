; (function ($) {
    $.fn.appUpload = function (options) {

        // capture this element
        var self = this;
        var elem = $(this);

        // define defaults
        var defaults = {
        }

        var settings = $.extend({}, defaults, options);

        // do work on element
        var uploadOptions = {
            showFileList: true,
            multiple: true,
            //template: kendo.template($('#fileTemplate').html())
        };
        $(self).kendoUpload(uploadOptions);

    }
}(jQuery))