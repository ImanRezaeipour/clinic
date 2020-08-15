var loadNewProductList = function (elem) {
    $.ajax({
        url: "/product/LastProductItemListAjax",
        data: {},
        type: "GET",
        dataType: "json",
        complete: function (xhr, status) {

            $(elem).append(xhr.responseJSON.Data);
            $(elem).children().addClass('landing-column');
            $(elem).children().wrap("<div class='landing-items'></div>");
            $(elem).children().wrapAll("<div class='landing-column-wrapper'></div>");

            (function ($) {
                $('.tooltip').tooltipster({
                    animation: 'grow',
                    delay: 100,
                    position: 'bottom'
                });
            })(jQuery);
            onAjaxLoadNavigatorAgain();
            $(".tooltipster").each(function () {
                simpleTooltip(this);
            });
        }
    });
}

var searchTerm = function (e, elem) {
    e = e || window.event;
    if (e.keyCode === 13) {
        var term = $(elem).val();
        if (document.location === '/product/search') {
            document.location = setUrlEncodedKey("t", term);
            onAjaxLoadNavigator();
        }
        else {
            document.location = '/product/search';
            document.location = setUrlEncodedKey("t", term);
            onAjaxLoadNavigator();
        }
        return false;
    }
    return true;
}

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var changeSearchTerm = function (e, elem) {
    debugger;
    e = e || window.event;
    if (e.keyCode === 13) {
        var term = $('#navSearchProduct').val();
        var cat = $('#navCategories').val();
        var newLink = '/product/search' + '?t=' + term + '&c=' + cat;
        window.location = newLink;
        return false;
    }
    return true;
}

var showDatePicker_OnClick = function (elem) {
    var jalaliToday = toJalaali(new Date());
    var normalTodat = jalaliToday.jy + "/" + jalaliToday.jm + "/" + jalaliToday.jd;
    PersianDatePicker.Show(elem, normalTodat);
}

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var searchOnLanding = function(elem) {
    var term = $('#navSearchProduct').val();
    var cat = $('#navCategories').val();
    var newLink = '/product/search' + '?t=' + term + '&c=' + cat;

    $(elem).attr('href', newLink);
}

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var loadCategories = function (elem) {
    $.ajax({
        url: '/Category/GetMainCategoriesAjax',
        data: {},
        type: 'GET',
        dataType: 'json',
        complete: function (xhr, status) {
            $.each(xhr.responseJSON.Data, function (key, value) {
                $(elem).append('<option value="' + value.Id + '">' + value.Title + '</option>');
            });
        }
    });
}


var goUp = function () {
    $('#goUpBtn').click(function (e) {
        $('body,html').animate({ scrollTop: 0 }, 600);
    });
    $(window).scroll(function () {
        if ($(this).scrollTop() > 150) {
            $('#goUpBtn').addClass('goUpAnimate');
        } else {
            $('#goUpBtn').removeClass('goUpAnimate');
        }
    });
};

var navMove = function () {
    $(window).scroll(function () {
        var x = 0;
        if ($(this).scrollTop() > 100) {
            x = 1;
        }
        if (x == 1) {
            $('.main-menu-wrap').addClass('navMenuFixed');
            $('.rendered-body').css('margin-top', '50px');
        } else {
            $('.main-menu-wrap').removeClass('navMenuFixed');
            $('.rendered-body').css('margin-top', '0');
        }
    });
};
