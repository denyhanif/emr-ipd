let interval;
// default interval count, value for start
let intervalCount = 0;
// parameter for max long interval
// asumed 30 = 30 second
let maxIntervalCount = 40;

// Generate QR and set interval 
function GetQR() {
    $('#qr-container').LoadingOverlay("show", {
        image: '/Images/Background/loading-beat.gif',
        imageAutoResize: false,
        imageAnimation: "2000ms none"
    })
    // show loading Generate QR
    $('#btnRefreshQR').prop('disabled', true);
    // make sure ajax request done, and take a lookup of response
    // if response failed then, stop interval
    $.when(GenerateQR()).done(function (response) {
        // Set Timeout
        if (response.d[0].QRCodeResponse[0].message_code === 0) {
            clearInterval(interval);
            //CloseModalQR();

            // hide loading screen 
            $('#qr-container').LoadingOverlay("hide")
            toastr.info("error when generate QR !");
            return;
        }
        // hide loading screen 
        $('#qr-container').LoadingOverlay("hide")
        // set interval
        interval = setInterval(SetQRInterval, 1000);
    });
}

// Ajax generate QR
function GenerateQR() {
    return $.ajax({
        url: 'Login.aspx/GetQRCode',
        type: 'Post',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            $("#imgQR").attr("src", "data:image/jpg;base64," + response.d[0].base64StringQR);
            $("#imgQR").css("visibility", "visible");
        },
        error: function (err) {
            toastr.info("Generate fail : ", err.responseText);
        }
    })
}
// set interval with long pooling 
function SetQRInterval() {
    if (intervalCount <= maxIntervalCount) {
        CheckIsLogin();
        intervalCount++;
    }
    else {
        // set intervalCount
        intervalCount = 0;
        clearInterval(interval);
        ClearSecretKey();
        ShowQrRefreshBox();
    }
}
// ajax for clear session
function ClearSecretKey() {
    $.ajax({
        url: 'Login.aspx/ClearSecretKey',
        type: 'Post',
        contentType: 'application/json; charset=utf-8',
        error: function (err) {
            toastr.info("Clear session error", 'Warning');
        }
    })
}
// Generate new QR after time is end
function RefreshQR() {
    $('#box-qr').LoadingOverlay("hide");
    GetQR();
}

// check login
function CheckIsLogin() {
    const status = {
        Fail: "Fail",
        Success:"Success"
    };

    $.ajax({
        url: 'Login.aspx/CheckIsLogin',
        type: 'Post',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            var retval = response.d;
            if (retval.code == 200) {
                StopQR();
                $('#modalQR').modal('toggle');
                $('#btnSignInQR').trigger('click');
            }
            else if (retval.code != 666)
            {
                StopQR();
                toastr.info(retval.message, 'Info');
            }
        }
    })
}

// stop interval
function StopQR() {
    clearInterval(interval);
    //$('#box-qr').LoadingOverlay("hide");
    //$("#imgQR").attr("src", "");
    //$("#imgQR").css("visibility", "hidden");
    ShowQrRefreshBox();
    ClearSecretKey();
}

// close modal
function CloseModalQR() {
    $('#modalQR').modal('hide');
    ClearSecretKey();
}

// show modal tutorial
function ShowTutorial() {
    $('#modalTutorial').modal('toggle');
    var video = document.getElementById("videoControl");
    if (video.src === '' || video.src === null)  {
        video.src = '../../Images/Tutorial Login Ke EMR.mp4';
    }
    video.load();
    video.play();
}

function ShowQrRefreshBox() {
    $('#box-qr').LoadingOverlay("show",
    {
        image: false,
        propgress: false,
        text: false,
        custimeAutoResize: false,
        customResizeFactor: 1,
        custom: '<button type="button" class="btn btn-app" style="border:0; margin:0;" onclick="RefreshQR()" id="btnRefreshQR" disabled="disabled"><i class= "fa fa-repeat fa-3x" id="icon-btn-refresh"></i>Click to Reload QR Code</button >',
        minSize: 150,
        maxSize: false,
    });
    $('#btnRefreshQR').prop('disabled', false);
}


$(document).ready(function () {

    // Get QR on Load
    GetQR();

    // Modal Video Tutorial On Close, stop video playback
    $("#modalTutorial").on("hidden.bs.modal", function () {
        // stop video on modal close
        var video = $('#videoControl');
        video[0].pause();
        video[0].currentTime = 0;
    });


});

