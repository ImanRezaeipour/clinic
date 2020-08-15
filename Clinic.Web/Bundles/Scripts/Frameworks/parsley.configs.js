window.parsleyOptions = {
    namespace: 'data-val-',
    excluded: "input[type=button], input[type=submit], input[type=reset], input[type=hidden], input.exclude_validation",
    trigger: "change",
    errorsWrapper: '<div class="parsley-errors-list"></div>',
    errorTemplate: "<span></span>",
    errorClass: "md-input-danger",
    successClass: "md-input-success",
    errorsContainer: function (e) {
        var i = e.$element;
        return i.closest(".form-field");
    },
    classHandler: function (e) {
        var i = e.$element;
        return i.is(":checkbox") || i.is(":radio") || i.parent().is("label") || $(i).is("[data-md-selectize]")
            ? i.closest(".form-field")
            : void 0;
    }
}

jQuery.fn.extend({
    simpleValidate: function(options) {
        var i = $(this);
        i.parsley(window.parsleyOptions)
            .on("form:validated",
                function() {
                    altair_md.update_input(i.find(".md-input-danger"));
                })
            .on("field:validated",
                function(i) {
                    $(i.$element).hasClass("md-input") && altair_md.update_input($(i.$element));
                });

            window.Parsley.on("field:validate",
                function() {
                    var i = $(this.$element).closest(".md-input-wrapper").siblings(".error_server_side");
                    i && i.hide();
                });
    }
})