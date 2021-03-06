$(document).ready(function () {
    $('input[type=radio][name=PayMode]').change(function () {

        if (this.value == 'wallet') {
            $("#WalletPayment").show();
            $("#WalletPay_checkUser_div").show();
        }

        else if (this.value == 'bank') {
            $("#WalletPayment").hide();
        }
    });

    $('#GenerateOtp').click(function (e) {
        GenerateOtp(this);
    });
    $('#btnProceedWallet').click(function (e) {
        ValidateOtp(this);
    });
    $('#ResendOtp').click(function (e) {
        GenerateOtp(this);
    });
});

function AddToCartProductPurchaseDetail() {
    $(".preloader").show();
    debugger;
    var Qty = parseInt(document.getElementById("qtyTextBox").value);
    if (Qty > 10) {
        alert("Quantity Should not Greater than 10!");
        return false;
    }
    var prodDetail = $('#prodDetailForm').serialize();
    $.ajax({
        url: '/Home/AddToCart',
        type: 'Post',
        datatype: 'Json',
        data: prodDetail
    }).done(function (result) {
        if (result == null || result == undefined || result == "") {
            alert("Something went wrong, Please try again later.");
        }
        else if (result.Status == false) {
            if (result.Message == null) {
                window.location.href = "/Login/Index";
            }
            else {
                alert(result.Message);
                GetHeaderCartDetailView();
            }
        }
        else {
            alert(result.Message);
            $("#cartCount").html(result.CartProductCount);
        }
        $(".preloader").hide();
    }).fail(function (error) {
        alert(error.statusText);
        $(".preloader").hide();
    });
    return false;
}

function SaveBillingAddress() {
    $(".preloader").show();

    var prodDetail = $('#checkoutPage').serialize();
    $.ajax({
        url: '/Home/SaveBillingAddressDetail',
        type: 'Post',
        datatype: 'Json',
        data: prodDetail
    }).done(function (result) {
        if (result == null || result == undefined || result == "") {
            alert("Something went wrong, Please try again later.");
        }
        else if (result.Status == false) {
            if (result.Message == "Login") {
                window.location.href = "/Login/Index";
            }
            else {
                alert(result.Message);
            }
        }
        else {
            window.location.href = "/Home/GetCartRelatedListView?pageName=CartDetailWithPayment&orderId=" + result.OrderId + "";
        }

        $(".preloader").hide();
    }).fail(function (error) {
        alert(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function ValidateTransaction() {
    $("#TransactionloginError").html("");
    var loginDetail = $('#WalletPay_checkUser').serialize();
    $(".preloader").show();
    $.ajax({
        url: '/Home/ValidateTransaction',
        type: 'Post',
        datatype: 'Json',
        data: loginDetail
    }).done(function (result) {
        if (result.response == "FAILED") {
            $("#TransactionloginError").show;
            $("#TransactionloginError").html(result.msg);
        }
        else {
            $("#WalletPay_checkUser_div").hide();
            $("#BalanceAmount").html(result.walletBalance);
            $("#WalletPay_OTP").show();
        }
        $(".preloader").hide();

    }).fail(function (error) {
        $("#TransactionloginError").html(error.statusText);
        $(".preloader").hide();
    });
    return false;
}


function GenerateOtp() {
    $("#otpMessage").html("");
    $(".preloader").show();
    $.ajax({
        url: '/Home/GenerateOtpDetail',
        type: 'Post',
        datatype: 'Json',
        data: {}
    }).done(function (result) {
        $("#WalletPay_OTP").hide();
        $("#Verify_OTP").show();
        $("#otpMessage").html(result);
        $(".preloader").hide();


    }).fail(function (error) {
        $("#loginError").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}

function ValidateOtp() {
    $("#ValidateOtpError").html("");
    var loginDetail = $('#WalletPay_ValidateOTP').serialize();
    $(".preloader").show();
    $.ajax({
        url: '/Home/ValidateOtp',
        type: 'Post',
        datatype: 'Json',
        data: loginDetail
    }).done(function (result) {
        $("#ValidateOtpError").html(result);
        if (result == "Success") {
            //$('#btnProceedWallet').show();
            createOrder();
        }
        // { $('#btnProceedWallet').hide(); }
        $(".preloader").hide();

    }).fail(function (error) {
        $("#loginError1").html(error.statusText);
        $(".preloader").hide();
    });

    return false;
}
function createOrder() {
    $(".preloader").show();
    var orderId = $('#OrderNo').val();
    $.ajax({
        url: '/Home/ManageOrderWithWallet',
        type: 'Post',
        datatype: 'Json',
        data: { "orderId": orderId }
    }).done(function (result) {
        if (result == null || result == undefined || result == "") {
            alert("Something went wrong, Please try again later.");
        }
        else if (result.Status == false) {
            if (result.Message == "Login") {
                window.location.href = "/Login/Index";
            }
            else {
                alert(result.Message);
            }
        }
        else {
            window.location.href = "/Home/GetCartRelatedListView?pageName=CartDetailWithPayment&orderId=" + result.OrderId + "";
        }

        $(".preloader").hide();
    }).fail(function (error) {
        alert(error.statusText);
        $(".preloader").hide();
    });

    return false;
}
