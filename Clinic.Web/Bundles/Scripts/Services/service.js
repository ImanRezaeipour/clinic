//#region STARTUP

/**
 * متد اصلی اجرا کننده ی اسکریپت های برنامه
 */
$(document).ready(function () {
    onLoadNavigator();
});

//#endregion STARTUP

/**
 * 
 * @returns {} 
 */
var onLoadNavigator = function () {
    $("[data-on-load]").each(function () {
        var functionName = $(this).data("on-load");
        var functionInvoker = window[functionName + "_OnLoad"];
        if (typeof functionInvoker === 'function') {
            functionInvoker(this);
        }
    });

    $("[data-on-click]").each(function () {
        var functionName = $(this).data("on-click");
        $(this).click(function() {
            var functionInvoker = window[functionName + "_OnClick"];
            if (typeof functionInvoker === 'function') {
                functionInvoker(this);
            }
        });
    });
}