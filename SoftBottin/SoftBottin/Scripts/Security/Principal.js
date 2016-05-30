
// Daniel Romro 12 de Abril de 2016 se incluye la linea para segurar que el dcumento ya este cargado
$(document).ready(function () {


    $("#tbShoppingCardDetail").on('click', '.remCF', function () {
        $("#numberShoesPick").html((parseInt($("#numberShoesPick").html()) - 1));

        var valueLast = $(this).parent().find("td:nth-child(5)").html();

        var tdValue = $('#tbShoppingCardDetail > tfoot:last >tr>td:nth-child(5)').html();
        var tdValueFinal = tdValue.toString().split('$');

        var tdValueFloat = parseFloat(tdValueFinal[1]) - parseFloat(valueLast);

        var trTotal = '<tr>' +
                          '<td>Total</td>' +
                          '<td class="hidden-xs"></td>' +
                          '<td class="hidden-xs"></td>' +
                          '<td class="hidden-xs"></td>' +
                          '<td>$' + tdValueFloat + '</td>' +
                          '</tr>';

        $('#tbShoppingCardDetail > tfoot:last').html(trTotal);

        $(this).parent().remove();
    });


    $("#btCloseShoppingCard").click(function () {
        $('#divShoppingCardDetail').hide();
    });

    $("#divShoppingCardSee").hover(function () {
        $('#divShoppingCardDetail').show();
    }, function () {
        //$('#divShoppingCardDetail').hide();
    });

    /*Inicio de LogIn */


    $("#tbEmailLogIn").on("input", function () {

        if ($("#tbEmailLogIn").val() != "") {
            if ($("#tbEmailLogIn").val() != "") {
                ChangeFormControlColor("formEmailLogIn", "success");
                ChangeGlyphiconColor("spGlyphiconEmailLogIn", "ok");
                ChangeDivMessageHide("dvMessageEmailLogIn");
            } else {
                ChangeFormControlColor("formEmailLogIn", "error");
                ChangeGlyphiconColor("spGlyphiconEmailLogIn", "remove");
                ChangeDivMessageShow("dvMessageEmailLogIn", "¡Tu correo usuario o electrónico, no es correcto!");
            }
        } else {
            ChangeFormControlColor("formEmailLogIn", "warning");
            ChangeGlyphiconColor("spGlyphiconEmailLogIn", "warning-sign");
            ChangeDivMessageHide("dvMessageEmailLogIn");

        }
    });


    $("#tbPasswordLogIn").on("input", function () {
        if ($("#tbPasswordLogIn").val() != "") {
            ChangeFormControlColor("formPasswordLogIn", "success");
            ChangeGlyphiconColor("spGlyphiconPasswordLogIn", "ok");
            ChangeDivMessageHide("dvMessagePasswordLogIn");
        } else {
            ChangeFormControlColor("formPasswordLogIn", "warning");
            ChangeGlyphiconColor("spGlyphiconPasswordLogIn", "warning-sign");
            ChangeDivMessageHide("dvMessagePasswordLogIn");
        }
    });


    $("#btnSigIn").on("click", function () {
        if ($("#formSigIn")[0].checkValidity()) {
            sigIn($("#tbEmailLogIn").val(), $("#tbPasswordLogIn").val());
            return false;
        } else {
            if ($("#tbEmailLogIn").val() == "") {
                $("#tbEmailLogIn").focus();
                ChangeFormControlColor("formEmailLogIn", "error");
                ChangeGlyphiconColor("spGlyphiconEmailLogIn", "remove");
                ChangeDivMessageShow("dvMessageEmailLogIn", "¡Tu usuario o correo electrónico, es requerido!");
            } else
                if ($("#tbPasswordLogIn").val() == "") {
                    $("#tbPasswordLogIn").focus();
                    ChangeFormControlColor("formPasswordLogIn", "error");
                    ChangeGlyphiconColor("spGlyphiconPasswordLogIn", "remove");
                    ChangeDivMessageShow("dvMessagePasswordLogIn", "¡Tu contraseña, es requerida!");
                }
            return false;
        }
    });

    /*Fin de LogIn */


    /*Inici de SigIn */

    $("#tbFirstName").on("input", function () {
        if ($("#tbFirstName").val() != "") {
            ChangeFormControlColor("formFirstName", "success");
            ChangeGlyphiconColor("spGlyphiconFirstName", "ok");
            ChangeDivMessage("dvMessageFirstName", "¡Tu nombre, es correcto!");
        } else {
            ChangeFormControlColor("formFirstName", "warning");
            ChangeGlyphiconColor("spGlyphiconFirstName", "warning-sign");
            ChangeDivMessageHide("dvMessageFirstName");
        }
    });

    $("#tbLastName").on("input", function () {
        if ($("#tbLastName").val() != "") {
            ChangeFormControlColor("fromLastName", "success");
            ChangeGlyphiconColor("spGlyphiconLastName", "ok");
            ChangeDivMessage("dvMessageLastName", "¡Tu apellido, es correcto!");
        } else {
            ChangeFormControlColor("fromLastName", "warning");
            ChangeGlyphiconColor("spGlyphiconLastName", "warning-sign");
            ChangeDivMessageHide("dvMessageLastName");
        }
    });


    $("#tbEmailSigIn").on("input", function () {
        if ($("#tbEmailSigIn").val() != "") {
            if (isValidEmailAddress($("#tbEmailSigIn").val())) {
                ChangeFormControlColor("formEmailSigIn", "success");
                ChangeGlyphiconColor("spGlyphiconEmailSigIn", "ok");
                ChangeDivMessage("dvMessageEmailSigIn", "¡Tu apellido, es correcto!");
            } else {
                ChangeFormControlColor("formEmailSigIn", "error");
                ChangeGlyphiconColor("spGlyphiconEmailSigIn", "remove");
                ChangeDivMessageShow("dvMessageEmailSigIn", "¡Tu correo electrónico, no es correcto!");
            }
        } else {
            ChangeFormControlColor("fromEmailSigIn", "warning");
            ChangeGlyphiconColor("spGlyphiconEmailSigIn", "warning-sign");
            ChangeDivMessageHide("dvMessageEmailSigIn");
        }
    });


    $("#tbPasswordSigIn").on("input", function () {
        if ($("#tbPasswordSigIn").val() != "") {
            if ($("#tbPasswordSigIn").val().length >= 8 && $("#tbPasswordSigIn").val().length <= 50) {
                if (isValidPassword($("#tbPasswordSigIn").val())) {
                    ChangeFormControlColor("formPasswordSigIn", "success");
                    ChangeGlyphiconColor("spGlyphiconPasswordSigIn", "ok");
                    ChangeDivMessageShow("dvMessagePasswordSigIn", "¡Tu contraseña, es correcta!");
                } else {
                    ChangeFormControlColor("formPasswordSigIn", "error");
                    ChangeGlyphiconColor("spGlyphiconPasswordSigIn", "remove");
                    ChangeDivMessageShow("dvMessagePasswordSigIn", "¡Tu contraseña, debe tener minimo 8 caracteres, 1 letra mayuscula y un número!");
                }
            } else {
                ChangeFormControlColor("formPasswordSigIn", "error");
                ChangeGlyphiconColor("spGlyphiconPasswordSigIn", "remove");
                ChangeDivMessageShow("dvMessagePasswordSigIn", "¡Tu contraseña, debe tener minimo 8 caracteres, 1 letra mayuscula y un número!");
            }
        } else {
            ChangeFormControlColor("formPasswordSigIn", "warning");
            ChangeGlyphiconColor("spGlyphiconPasswordSigIn", "warning-sign");
            ChangeDivMessageHide("dvMessagePasswordSigIn");
        }
    });

    $("#tbPasswordConfirmSigIn").on("input", function () {
        if ($("#tbPasswordConfirmSigIn").val() != "") {
            ChangeFormControlColor("formPasswordConfirmSigIn", "success");
            ChangeGlyphiconColor("spGlyphiconPasswordConfirmSigIn", "ok");
            ChangeDivMessage("dvMessagePasswordConfirmSigIn", "¡Tu apellido, es correcto!");
        } else {
            ChangeFormControlColor("formPasswordConfirmSigIn", "warning");
            ChangeGlyphiconColor("spGlyphiconPasswordConfirmSigIn", "warning-sign");
            ChangeDivMessageHide("dvMessagePasswordConfirmSigIn");
        }
    });


    /*Fin de SigIn */


    /*Inicio de Utilidades */


    function isValidEmailAddress(emailAddress) {
        var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
        return pattern.test(emailAddress);
    };


    function isValidPassword(password) {
        var pattern = /.*[0-9]{1,}.*[A-Z]{1,}.*|.*[A-Z]{1,}.*[0-9]{1,}.*/;
        return pattern.test(password);
    };


    /*Fin de Utilidades */


    $("#btnAddEmail").on("click", function () {
        $.ajax({
            url: window.rootUrl + 'Security/AddNewEmailUser',
            type: 'POST',
            data: "{'sEmail':'" + $("#tbEmailUser").val() + "'}",
            contentType: 'application/json',
            success: function (data) {
                $("#altSuccess").show();
                $("#altSuccess").html(' <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>Registrado con éxito!</strong> Revisa tu correo, encontras mas infomación.');
                $("#altSuccess").fadeTo(5000, 500).slideUp(500, function () {

                    $("#formEmail").removeClass("has-success");
                    $("#formEmail").removeClass("has-warning");
                    $("#formEmail").removeClass("has-error");

                    $("#spGlyphicon").removeClass("glyphicon-ok");
                    $("#spGlyphicon").removeClass("glyphicon-warning-sign");
                    $("#spGlyphicon").removeClass("glyphicon-remove");

                    $("#formEmail").addClass("has-warning");
                    $("#spGlyphicon").addClass("glyphicon-warning-sign");
                    $("#dvMessageEmail").html("¡Por Favor Ingresa Tu Email!");

                    $("#tbEmailUser").val("");

                    $("#altSuccess").hide();
                });
            },
            error: function (dataError) {
                alert(dataError);
            }
        });
    });

    $("#tbEmailUser").on("input", function () {


        $("#formEmail").removeClass("has-success");
        $("#formEmail").removeClass("has-warning");
        $("#formEmail").removeClass("has-error");

        $("#spGlyphicon").removeClass("glyphicon-ok");
        $("#spGlyphicon").removeClass("glyphicon-warning-sign");
        $("#spGlyphicon").removeClass("glyphicon-remove");


        $("#dvMessageEmail").html("");

        if ($("#tbEmailUser").val() == "") {
            $("#formEmail").addClass("has-warning");
            $("#spGlyphicon").addClass("glyphicon-warning-sign");
            $("#dvMessageEmail").html("¡Por Favor Ingresa Tu Email!");
            $("#btnAddEmail").addClass("disabled");
        } else {
            if (isValidEmailAddress($("#tbEmailUser").val())) {
                $("#formEmail").addClass("has-success");
                $("#spGlyphicon").addClass("glyphicon-ok");
                $("#dvMessageEmail").html("¡Tu Email, es correcto!");
                $("#btnAddEmail").removeClass("disabled");
            } else {
                $("#formEmail").addClass("has-error");
                $("#spGlyphicon").addClass("glyphicon-remove");
                $("#dvMessageEmail").html("¡Tu Email, es incorrecto!");
            }
        }

    });

    $('#bt_mdSignIn').on('click', function () {
        // Load up a new modal...
        $('#mdlogIn').modal('hide');
        $('#mdSignIn').modal('show');
    });



    //Inicio API Facebook


    // This is called with the results from from FB.getLoginStatus().
    function statusChangeCallback(response) {
        console.log('statusChangeCallback');
        console.log(response);
        // The response object is returned with a status field that lets the
        // app know the current login status of the person.
        // Full docs on the response object can be found in the documentation
        // for FB.getLoginStatus().
        if (response.status === 'connected') {
            // Logged into your app and Facebook.
            testAPI();
        } else if (response.status === 'not_authorized') {
            // The person is logged into Facebook, but not your app.
            document.getElementById('status').innerHTML = 'Please log ' +
              'into this app.';
        } else {
            // The person is not logged into Facebook, so we're not sure if
            // they are logged into this app or not.
            document.getElementById('status').innerHTML = 'Please log ' +
              'into Facebook.';
        }
    }

    // This function is called when someone finishes with the Login
    // Button.  See the onlogin handler attached to it in the sample
    // code below.
    function checkLoginState() {
        FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });
    }


    window.fbAsyncInit = function () {
        FB.init({
            appId: '994758580598409',
            xfbml: true,
            version: 'v2.5'
        });
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/es_CO/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

    // Here we run a very simple test of the Graph API after login is
    // successful.  See statusChangeCallback() for when this call is made.
    function testAPI() {
        console.log('Welcome!  Fetching your information.... ');
        FB.api('/me',
               { fields: "name, email, birthday ,currency, first_name, last_name, gender, languages, location, timezone " }, function (response) {
                   console.log(response);
                   console.log('Successful login for: ' + response.name);
                   console.log('Successful email for: ' + response.email);
                   console.log('Successful birthday for: ' + response.birthday);
                   console.log('Successful currency for: ' + response.currency);
                   console.log('Successful first_name for: ' + response.first_name);
                   console.log('Successful last_name for: ' + response.last_name);
                   console.log('Successful gender for: ' + response.gender);
                   console.log('Successful languages for: ' + response.languages);
                   console.log('Successful location for: ' + response.location);
                   console.log('Successful timezone for: ' + response.timezone);
                   document.getElementById('status').innerHTML =
                     'Thanks for logging in, ' + response.name + '!';
               });
    }

    //Fin API Facebook


    $("#myCarousel").carousel({
        interval: 5000
    });


});


// Adicionar Producto al carrito de compras
function addProductToShoppingCart(shoeReference, shoeName, shoePrice, shoeColor, showSize) {

    var shoe = { reference: shoeReference, price: shoePrice, color: shoeColor, size: showSize };

    $.ajax({
        url: window.rootUrl + 'Security/AddNewProduct',
        type: 'POST',
        data: "{'objShoe':'" + JSON.stringify(shoe) + "'}",
        contentType: 'application/json',
        success: function (data) {
            $("#numberShoesPick").html((parseInt($("#numberShoesPick").html()) + 1));
            $("#altSuccess").show();
            $("#altSuccess").html('<strong>Agregado al carrito con éxito!</strong>');
            $("#altSuccess").fadeTo(2000, 500).slideUp(500, function () {

                $("#altSuccess").hide();
            });


            //var trAdd = '<tr>' +
            //           '<td class="hidden-xs remCF" scope="row">' +
            //           '<span class="glyphicon glyphicon-trash app-glyphicon4" style="margin-top:1%" id="numberShoesPick"></span>' +
            //           '</th>' +
            //           '<td class="hidden-xs">' +
            //           '<img src="' + window.rootUrl + 'Content/Images/Militar/Zapato10.jpg")" class="img-circle img-responsive" width="25" height="25" />' +
            //           '</td>' +
            //           '<td>' +
            //            shoeName +
            //           '</td>' +
            //           '<td class="hidden-xs">' +
            //           '1' +
            //           '</td>' +
            //           '<td>' +
            //           shoePrice
            //'</td>' +
            //'</tr>';


            //var tdValue = $('#tbShoppingCardDetail > tfoot:last >tr>td:nth-child(5)').html();
            //var tdValueFinal = tdValue.toString().split('$');

            //var tdValueFloar = parseFloat(tdValueFinal[1]) + parseFloat(shoePrice);

            //$('#tbShoppingCardDetail > tbody:last').after(trAdd);

            //var trTotal = '<tr>' +
            //              '<td>Total</td>' +
            //              '<td class="hidden-xs"></td>' +
            //              '<td class="hidden-xs"></td>' +
            //              '<td class="hidden-xs"></td>' +
            //              '<td>$' + tdValueFloar + '</td>' +
            //              '</tr>';

            //$('#tbShoppingCardDetail > tfoot:last').html(trTotal);

        },
        error: function (dataError) {
            alert(dataError);
        }
    });

}

function goPurchasingSummary() {
    var url = $("#RedirectTo").val();
    location.href = url;
}

function sigIn(piUserName, piPassword) {

    var user = { sUserName: piUserName, sPassword: piPassword };

    $.ajax({
        url: window.rootUrl + 'Security/sigIn',
        type: 'POST',
        data: "{'objUser':'" + JSON.stringify(user) + "'}",
        contentType: 'application/json',
        success: function (data) {
            if (data) {
                window.location = window.rootUrl + "Security/Principal";
            } else {
                //spGlyphiconErrorSigIn
                ChangeFormControlColor("formErorMessageLogIn", "error");
                ChangeGlyphiconColor("spGlyphiconErrorSigIn", "remove");
                ChangeDivMessageShow("dvMessageErrorLogIn", "¡Tu usuario o contraseña son incorrectos!");
                $("#altErrorSigIn").show();
                
            }
            
        },
        error: function (dataError) {
            alert(dataError);
        }
    });
}


// Fin producto al carrito de compras



// Inicio Funciones Colores con Boostrap

// success
// warning
// error
function ChangeFormControlColor(idField, desStatus) {
    $("#" + idField).removeClass("has-success");
    $("#" + idField).removeClass("has-warning");
    $("#" + idField).removeClass("has-error");
    $("#" + idField).addClass("has-" + desStatus);
}

// ok
// warning-sign
// remove
function ChangeGlyphiconColor(idField, desStatus) {
    $("#" + idField).removeClass("glyphicon-ok");
    $("#" + idField).removeClass("glyphicon-warning-sign");
    $("#" + idField).removeClass("glyphicon-remove");
    $("#" + idField).addClass("glyphicon-" + desStatus);
}


function ChangeDivMessageShow(idField, Message) {
    $("#" + idField).removeClass("hidden");
    $("#" + idField).html(Message);
}

function ChangeDivMessageHide(idField) {
    $("#" + idField).addClass("hidden");
}


//Fin Funciones Colores con Boostrap