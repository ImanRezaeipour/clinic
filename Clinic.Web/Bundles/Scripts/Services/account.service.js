var refreshCaptcha = function(e, elem) {
    e.preventDefault();
    
    var d = new Date();
    var ticks = ((d.getTime() * 10000) + 621355968000000000);
    $('#imageCaptcha').attr('src', '/home/CaptchaImage/' + ticks);
    //$.ajax({
    //    url: '/home/CaptchaImage',
    //    data: { rndDate: ticks },
    //    type: 'GET',
    //    dataType: 'json',
    //    complete: function (xhr, status) {
    //        console.log(xhr); alert('yas');
    //        $('#imageCaptcha').attr('src', xhr.responseText);
    //    }
    //});


}