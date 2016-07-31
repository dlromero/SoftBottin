
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


    $("#btnLogIn").on("click", function () {
        $("#loading").show();
        if ($("#formLogIn")[0].checkValidity()) {
            LogIn($("#tbEmailLogIn").val(), $("#tbPasswordLogIn").val());
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
            $("#loading").hide();
            return false;
        }
    });

    /*Fin de LogIn */


    /*Inicii de SignIn */

    $("#tbFirstNameSignIn").on("input", function () {
        if ($("#tbFirstNameSignIn").val() != "") {
            ChangeFormControlColor("formFirstNameSignIn", "success");
            ChangeGlyphiconColor("spGlyphiconFirstNameSignIn", "ok");
            ChangeDivMessageShow("dvMessageFirstNameSignIn", "¡Tu nombre, es correcto!");
        } else {
            ChangeFormControlColor("formFirstNameSignIn", "warning");
            ChangeGlyphiconColor("spGlyphiconFirstNameSignIn", "warning-sign");
            ChangeDivMessageHide("dvMessageFirstNameSignIn");
        }
    });

    $("#tbLastNameSignIn").on("input", function () {
        if ($("#tbLastNameSignIn").val() != "") {
            ChangeFormControlColor("fromLastNameSignIn", "success");
            ChangeGlyphiconColor("spGlyphiconLastNameSignIn", "ok");
            ChangeDivMessageShow("dvMessageLastNameSignIn", "¡Tu apellido, es correcto!");
        } else {
            ChangeFormControlColor("fromLastNameSignIn", "warning");
            ChangeGlyphiconColor("spGlyphiconLastNameSignIn", "warning-sign");
            ChangeDivMessageHide("dvMessageLastNameSignIn");
        }
    });


    $("#tbEmailSignIn").on("input", function () {
        if ($("#tbEmailSignIn").val() != "") {

            if (isValidEmailAddress($("#tbEmailSignIn").val())) {
                ChangeFormControlColor("formEmailSignIn", "success");
                ChangeGlyphiconColor("spGlyphiconEmailSignIn", "ok");
                ChangeDivMessageShow("dvMessageEmailSignIn", "¡Tu correo electrónico, es correcto!");
            } else {
                ChangeFormControlColor("formEmailSignIn", "error");
                ChangeGlyphiconColor("spGlyphiconEmailSignIn", "remove");
                ChangeDivMessageShow("dvMessageEmailSignIn", "¡Tu correo electrónico, no es correcto!");
            }
        } else {
            ChangeFormControlColor("fromEmailSignIn", "warning");
            ChangeGlyphiconColor("spGlyphiconEmailSignIn", "warning-sign");
            ChangeDivMessageHide("dvMessageEmailSignIn");
        }
    });


    $("#tbEmailSignIn").on("blur", function () {
        if ($("#tbEmailSignIn").val() != "") {
            $.ajax({
                url: window.rootUrl + 'Security/CheckEmail',
                type: 'POST',
                data: "{'sEmail':'" + $("#tbEmailSignIn").val() + "'}",
                contentType: 'application/json',
                success: function (data) {
                    if (data) {
                        ChangeFormControlColor("formEmailSignIn", "success");
                        ChangeGlyphiconColor("spGlyphiconEmailSignIn", "ok");
                        ChangeDivMessageShow("dvMessageEmailSignIn", "¡Tu correo electrónico, es correcto!");
                    } else {
                        ChangeFormControlColor("formEmailSignIn", "error");
                        ChangeGlyphiconColor("spGlyphiconEmailSignIn", "remove");
                        ChangeDivMessageShow("dvMessageEmailSignIn", "¡Ya existe una cuenta asociada a este correo, intenta con uno diferente!");
                        $("#tbEmailSignIn").focus();
                    }

                },
                error: function (dataError) {
                    alert(dataError);
                }
            });

        } else {
            ChangeFormControlColor("fromEmailSignIn", "warning");
            ChangeGlyphiconColor("spGlyphiconEmailSignIn", "warning-sign");
            ChangeDivMessageHide("dvMessageEmailSignIn");
        }
    });





    $("#tbPasswordSignIn").on("input", function () {
        if ($("#tbPasswordSignIn").val() != "") {
            if ($("#tbPasswordSignIn").val().length >= 8 && $("#tbPasswordSignIn").val().length <= 50) {
                if (isValidPassword($("#tbPasswordSignIn").val())) {
                    ChangeFormControlColor("formPasswordSignIn", "success");
                    ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "ok");
                    ChangeDivMessageShow("dvMessagePasswordSignIn", "¡Tu contraseña, es correcta!");
                } else {
                    ChangeFormControlColor("formPasswordSignIn", "error");
                    ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "remove");
                    ChangeDivMessageShow("dvMessagePasswordSignIn", "¡Tu contraseña, debe tener minimo 8 caracteres, 1 letra mayuscula y un número!");
                }
            } else {
                ChangeFormControlColor("formPasswordSignIn", "error");
                ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "remove");
                ChangeDivMessageShow("dvMessagePasswordSignIn", "¡Tu contraseña, debe tener minimo 8 caracteres, 1 letra mayuscula y un número!");
            }
        } else {
            ChangeFormControlColor("formPasswordSignIn", "warning");
            ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "warning-sign");
            ChangeDivMessageHide("dvMessagePasswordSignIn");
        }
    });

    $("#tbPasswordConfirmSignIn").on("input", function () {
        if ($("#tbPasswordConfirmSignIn").val() != "") {
            if ($("#tbPasswordConfirmSignIn").val() == $("#tbPasswordSignIn").val()) {
                ChangeFormControlColor("formPasswordConfirmSignIn", "success");
                ChangeGlyphiconColor("spGlyphiconPasswordConfirmSignIn", "ok");
                ChangeDivMessageShow("dvMessagePasswordConfirmSignIn", "¡Confimación de contraseña correcta!");
            } else {
                ChangeFormControlColor("formPasswordConfirmSignIn", "error");
                ChangeGlyphiconColor("spGlyphiconPasswordConfirmSignIn", "remove");
                ChangeDivMessageShow("dvMessagePasswordConfirmSignIn", "¡La contraseña no coincide con la anterior!");
            }

        } else {
            ChangeFormControlColor("formPasswordConfirmSignIn", "warning");
            ChangeGlyphiconColor("spGlyphiconPasswordConfirmSignIn", "warning-sign");
            ChangeDivMessageHide("dvMessagePasswordConfirmSignIn");
        }
    });



    $("#btnSignIn").on("click", function () {
        if ($("#formSignIn")[0].checkValidity()) {
            SignIn($("#tbFirstNameSignIn").val(), $("#tbLastNameSignIn").val(),
                   $("#tbEmailSignIn").val(), $("#tbPasswordSignIn").val());
            return false;
        } else {

            if ($("#tbFirstNameSignIn").val() == "") {
                $("#tbFirstNameSignIn").focus();
                ChangeFormControlColor("formFirstNameSignIn", "error");
                ChangeGlyphiconColor("spGlyphiconFirstNameSignIn", "remove");
                ChangeDivMessageShow("dvMessageFirstNameSignIn", "¡Tu nombre es requerido!");
                return false;
            } else
                if ($("#tbLastNameSignIn").val() == "") {
                    $("#tbLastNameSignIn").focus();
                    ChangeFormControlColor("fromLastNameSignIn", "error");
                    ChangeGlyphiconColor("spGlyphiconLastNameSignIn", "remove");
                    ChangeDivMessageShow("dvMessageLastNameSignIn", "¡Tu apellido es requerido!");
                    return false;
                } else
                    if ($("#tbEmailSignIn").val() == "") {
                        $("#tbEmailSignIn").focus();
                        ChangeFormControlColor("formEmailSignIn", "error");
                        ChangeGlyphiconColor("spGlyphiconEmailSignIn", "remove");
                        ChangeDivMessageShow("dvMessageEmailSignIn", "¡Tu correo es requerido!");
                        return false;
                    } else
                        if ($("#tbEmailSignIn").val() != "") {
                            if (!isValidEmailAddress($("#tbEmailSignIn").val())) {
                                ChangeFormControlColor("formEmailSignIn", "error");
                                ChangeGlyphiconColor("spGlyphiconEmailSignIn", "remove");
                                ChangeDivMessageShow("dvMessageEmailSignIn", "¡Tu correo electrónico, no es correcto!");

                                $.ajax({
                                    url: window.rootUrl + 'Security/CheckEmail',
                                    type: 'POST',
                                    data: "{'sEmail':'" + $("#tbEmailSignIn").val() + "'}",
                                    contentType: 'application/json',
                                    success: function (data) {
                                        if (data) {
                                            ChangeFormControlColor("formEmailSignIn", "success");
                                            ChangeGlyphiconColor("spGlyphiconEmailSignIn", "ok");
                                            ChangeDivMessageShow("dvMessageEmailSignIn", "¡Tu correo electrónico, es correcto!");
                                        } else {
                                            ChangeFormControlColor("formEmailSignIn", "error");
                                            ChangeGlyphiconColor("spGlyphiconEmailSignIn", "remove");
                                            ChangeDivMessageShow("dvMessageEmailSignIn", "¡Ya existe una cuenta asociada a este correo, intenta con uno diferente!");
                                            $("#tbEmailSignIn").focus();
                                            return false;
                                        }
                                    },
                                    error: function (dataError) {
                                        alert(dataError);
                                    }
                                });
                            }
                        } else
                            if ($("#tbPasswordSignIn").val() == "") {
                                $("#tbPasswordSignIn").focus();
                                ChangeFormControlColor("formPasswordSignIn", "error");
                                ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "remove");
                                ChangeDivMessageShow("dvMessagePasswordSignIn", "¡Tu contraseña es requerido!");
                                return false;
                            } else

                                if ($("#tbPasswordSignIn").val() != "") {
                                    if ($("#tbPasswordSignIn").val().length >= 8 && $("#tbPasswordSignIn").val().length <= 50) {
                                        if (isValidPassword($("#tbPasswordSignIn").val())) {
                                            ChangeFormControlColor("formPasswordSignIn", "success");
                                            ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "ok");
                                            ChangeDivMessageShow("dvMessagePasswordSignIn", "¡Tu contraseña, es correcta!");
                                        } else {
                                            ChangeFormControlColor("formPasswordSignIn", "error");
                                            ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "remove");
                                            ChangeDivMessageShow("dvMessagePasswordSignIn", "¡Tu contraseña, debe tener minimo 8 caracteres, 1 letra mayuscula y un número!");
                                            $("#tbPasswordSignIn").focus();
                                            return false;
                                        }
                                    } else {
                                        ChangeFormControlColor("formPasswordSignIn", "error");
                                        ChangeGlyphiconColor("spGlyphiconPasswordSignIn", "remove");
                                        ChangeDivMessageShow("dvMessagePasswordSignIn", "¡Tu contraseña, debe tener minimo 8 caracteres, 1 letra mayuscula y un número!");
                                        $("#tbPasswordSignIn").focus();
                                        return false;
                                    }
                                } else
                                    if ($("#tbPasswordConfirmSignIn").val() == "") {
                                        $("#tbPasswordConfirmSignIn").focus();
                                        ChangeFormControlColor("formPasswordConfirmSignIn", "error");
                                        ChangeGlyphiconColor("spGlyphiconPasswordConfirmSignIn", "remove");
                                        ChangeDivMessageShow("dvMessagePasswordConfirmSignIn", "¡Tu contraseña es requerido!");
                                    } else
                                        if ($("#tbPasswordConfirmSignIn").val() != "") {
                                            if ($("#tbPasswordConfirmSignIn").val() == $("#tbPasswordSignIn").val()) {
                                                ChangeFormControlColor("formPasswordConfirmSignIn", "success");
                                                ChangeGlyphiconColor("spGlyphiconPasswordConfirmSignIn", "ok");
                                                ChangeDivMessageShow("dvMessagePasswordConfirmSignIn", "¡Confimación de contraseña correcta!");
                                            } else {
                                                ChangeFormControlColor("formPasswordConfirmSignIn", "error");
                                                ChangeGlyphiconColor("spGlyphiconPasswordConfirmSignIn", "remove");
                                                ChangeDivMessageShow("dvMessagePasswordConfirmSignIn", "¡La contraseña no coincide con la anterior!");
                                                $("#tbPasswordConfirmSignIn").focus();
                                                return false;
                                            }

                                        }

            return false;
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



    $("#myCarousel").carousel({
        interval: 5000
    });


});



function goPurchasingSummary() {
    var url = $("#RedirectTo").val();
    location.href = url;
}

function LogIn(piUserName, piPassword) {

    var user = { sUserName: piUserName, sPassword: piPassword };

    $.ajax({
        url: window.rootUrl + 'Security/LogIn',
        type: 'POST',
        data: "{'objUser':'" + JSON.stringify(user) + "'}",
        contentType: 'application/json',
        success: function (data) {
            if (data) {
                window.location = window.rootUrl + "Security/Principal";
                $("#loading").hide();
            } else {
                //spGlyphiconErrorSigIn
                ChangeFormControlColor("formErorMessageLogIn", "error");
                ChangeGlyphiconColor("spGlyphiconErrorSigIn", "remove");
                ChangeDivMessageShow("dvMessageErrorLogIn", "¡Tu usuario o contraseña son incorrectos!");
                $("#altErrorLogIn").show();
                $("#loading").hide();
            }
        },
        error: function (dataError) {
            $("#loading").hide();
            alert(dataError);
        }
    });
}

function SignIn(piFirstName, piLastName, piEmail, piPassword) {

    var user = {
        sFirstName: piFirstName,
        sLastName: piLastName,
        sEmail: piEmail,
        sPassword: piPassword
    };

    $.ajax({
        url: window.rootUrl + 'Security/SignIn',
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
                $("#altErrorSignIn").show();

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