function Login_Account() {
    $("#loginError").html("");
    var loginDetail = $('#addForm').serialize();
    $(".preloader").show();
    $.ajax({
        url: '/Login/ValidateLogin',
        type: 'Post',
        datatype: 'Json',
        data: loginDetail
    }).done(function (result) {
        if (result == "Success") {
            window.location.href = "/Home/Index";
        }
        else {
            $("#loginError").html(result);
        }
        $(".preloader").hide();
        $("#closeError").show();
    }).fail(function (error) {
        $("#loginError").html(error.statusText);
        $(".preloader").hide();
        $("#closeError").show();
        $(".preloader").hide();
    });

    return false;
}

function SaveDetailForm() {
    $("#loginError").html("");
    var loginDetail = $('#addRegisterForm').serialize();
    $(".preloader").show();
    $.ajax({
        url: '/Login/SaveDetail',
        type: 'Post',
        datatype: 'Json',
        data: loginDetail
    }).done(function (result) {
        if (result == "Success") {
            window.location.href = "/Home/Index";
        }
        else {
            $("#loginError2").html(result);
        }
        $(".preloader").hide();
        $("#closeError").show();
    }).fail(function (error) {
        $("#loginError").html(error.statusText);
        $(".preloader").hide();
        $("#closeError").show();
        $(".preloader").hide();
    });

    return false;
}