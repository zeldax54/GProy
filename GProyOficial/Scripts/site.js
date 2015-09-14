$(document).ready(function () {
   
    


    window.myValueTr = 0;
    $("#client_isSubject").click(function() {
        if ($("#client_isSubject").is(":checked")) {
            $("#checkBoxFather").show();
        } else {
            $("#checkBoxFather").hide();
        }
    });

    if ($("#client_isSubject").is(":checked")) {
        $("#checkBoxFather").show();
    };

    function createJson() {
        var json = '{';
        var otArr = [];
        var tbl2 = $('#table tbody tr').each(function (i) {
            if ($(this)[0].rowIndex != 0) {
                x = $(this).children();
                var itArr = [];


                x.each(function (j) {
                    if ($(this).text() != "" && j != 1 && j != 3) {
                        itArr.push('"' + $(this).text() + '"');
                    }
                });

                otArr.push('"' + i + '": [' + itArr.join(',') + ']');
            }
        });
        json += otArr.join(",") + '}';
        return json;
    };

    $("#create").click(function () {
        var newRow = '<tr><td>' + $("#accountBankaccountBank1").val() + '</td>' +
            '<td>' + $("#accountBankbankId option:selected").html() + '</td>' +
            '<td hidden="hidden">' + $("#accountBankbankId").val() + '</td>' +
            '<td>' + $("#accountBankcurrencyTypeId option:selected").html() + '</td>' +
            '<td hidden="hidden">' + $("#accountBankcurrencyTypeId").val() + '</td>' +
            '<td>' + $("#accountBanktitular").val() + '</td>' +
            '<td><input type="image" src="../Content/images/trash.png" id="deleteRow"></td></tr>';
        $("#table").find('tbody').append(newRow);
        var _jsondata = createJson();
        $('#jsonData').val(_jsondata);
        
    });

    $(document).on('click', '#deleteRow', function() {
        var parentTr = $(this).parents().get(1);
        $(parentTr).remove();
        var _jsondata = createJson();
        $('#jsonData').val(_jsondata);
    });

    function createClientJson() {
        var json = "{";
        var value = 0;
        var clientArr = [];
        var valueArr = [];
        var idproyarr = [];
        var resultArr = [];
      
        $("#tableC tbody").find('.clientId').each(function (i) {
            clientArr.push('"' + $(this).html() + '"');
          
        });
        $("#tableC tbody").find('#idproyect').each(function (i) {
            idproyarr.push('"' + $(this).val() + '"');

        });

        $("#tableC tbody").find('.amountValue').each(function (i) {
            valueArr.push('"' + $(this).val() + '"');
            value += parseFloat($(this).val());
            $('#amountV').val(value);
        });

        if (clientArr.length == valueArr.length) {
            for (var i = 0; i < clientArr.length; i++) {
                resultArr.push('[' + clientArr[i] + ',' + valueArr[i] + ','+idproyarr[i]+']');
            }
        }
        json += resultArr.join(',') + '}';
        $('#jsonClient').val(json);
        
    };

    function countClient() {
        var count = 0;
        $("#tableC tbody").find('.clientId').each(function (i) {
            count += 1;
        });
        return count;
    }

    $(document).on('click', '#sendClient', function () {
        var idact = $(this).parents('tr').find('.clientId').html();
        $(this).parents('tr').find("#numdec").css({ background: "none" });
        if ($(this).parents('tr').find('.numdec').val() === '0.00' || $(this).parents('tr').find('.numdec').val() === '') {

            $(this).parents('tr').find("#numdec").css({ background: "#45B6AF" });
            MostrarT('El monto no pude ser 0','Error', 'error');

        } else {
            if ($(this).parents('tr').hasClass('success')) {
                
                MostrarT($(this).parents('tr').find('#clientName').html() + ' Ya esta agregado','Mensaje','info');
               
            } else {

                var newRow = '<tr><td hidden="hidden" class="clientId">' + $(this).parents('tr').find('.clientId').html() + '</td>' +
                    '<td class="clientName">' + $(this).parents('tr').find('#clientName').html() + '</td>' +
                    '<td><input class="amountValue" type="text" required="true" readonly value="' + $(this).parents('tr').find('.numdec').val() + '"></td>' +
                    '<td><input type="text" id="nombproy" value="Nuevo Proyecto" readonly>' +
                    '<a href="#" class="btn btn-xs green" id="botonp"><i class="fa fa-plus"></i></a></td><td><input type="image" src="../../Content/images/trash.png" class="sendRow" style="top: 25%;"></td><td><input type="text" id="idproyect" value="*" readonly hidden=""></td>' +
                    '</tr>';
                $("#tableC").find('tbody').append(newRow);

                $('#sample_2 tbody tr').each(function() {
                    if ($(this)[0].rowIndex != 0) {
                        var idspe = $(this).children().find(".clientId").html();
                        if (idspe === idact) {
                            
                            $(this).children().parents('tr').addClass("success");

                        }
                    }
                });
                createClientJson();
                var count = countClient();
                $('#pass').val(count);
            }
        }
       

    });

    //Agregar Proyecto al Suplemento
    $(document).on('click', '#botonp', function () {
        var idcliente = $(this).parents('tr').find('.clientId').html();
        var nombcliente = $(this).parents('tr').find('.clientName').html();
        var dataString = 'idc=' + idcliente;
        var prod = $('#productId').val();
        

        $.ajax({
            type: 'POST',
            url: '/Supplements/GetProyectos/',
            data: dataString,
            success: function (data) {
                if (data !== '-1') {
                    var contact = JSON.parse(data);
                    var P = contact.Proyectos;
                    //Limpiando tabla
                    $('#tabP tbody tr').each(function (i) {
                        if ($(this)[0].rowIndex != 0) {
                           

                                var tdats = $('#tabP').DataTable();
                                tdats.row().remove().draw(false);

                            
                        }
                    });
                    /////
                   
              
                    for (var f = 0; f < P.length; f++) {
                        var id = P[f].id;
                        var nomb = P[f].nombre;
                        var producto = P[f].producto;
                        var productonomb = P[f].productonomb;
                        $('#tabP').DataTable().row.add(['<span id="proyname">' + nomb + '</span>', '<span id="prodname" >' + productonomb + '</span>','<a href="#" class="btn btn-xs green" id="sendproytosup"><i class="fa fa-plus"></i></a>',
                            '<span id="proyid" hidden="">' + id + '</span>', '<span id="prodid" hidden="">' + producto + '</span>']).draw();

                    }
                    $('#titulo').empty();
                    $('#titulo').append('Proyectos en Proceso para el Cliente ' + nombcliente);
                    $('#idprodact').empty();
                    $('#idprodact').append(prod);
                    $('#idclientact').empty();
                    $('#idclientact').append(idcliente);
                    
                    $('#myModalP').modal('show');
                   
                }
                if (data === '-1') {
                  
                    MostrarT('No hay Proyectos en Proceso en este cliente active alguno si existe o se creara uno nuevo automaticamente'
                        ,'Mensaje','info');
                }
            },
            error: function (req, stat, err) {

                alert(req.responseText);


            }
        });
       
       
    });
    $(document).on('click', '#sendproytosup', function () {
        var idprod = $(this).parents('tr').find('#prodid').html();
        var idprodact = $('#idprodact').html();
        var idclienteact = $('#idclientact').html();

        var nombp = $(this).parents('tr').find('#proyname').html();
        var idproy = $(this).parents('tr').find('#proyid').html();
        if (idprod !== idprodact) {
            MostrarT('El producto de este Proyecto es distinto al producto del Suplemento','Mensaje','info');
        } else {
            $("#tableC tbody tr").each(function (i) {
                var idc = $(this).children().parents('tr').find('.clientId').html();
                if (idc === idclienteact) {
                    $(this).children().parents('tr').find('#nombproy').val(nombp);
                    $(this).children().parents('tr').find('#idproyect').val(idproy);
                    //Actualizar Json
                    createClientJson();
                    //Mostrar Msj
                    MostrarT('Proyecto Asignado','Mensaje','info');
                }
            });

        }
    });


    $(document).on('click', '.sendRow', function () {
        var value = $(this).parents('tr').find('.amountValue').val();
        
        var valueAmount = $('#amountV').val();
        var result = parseFloat(valueAmount - value);
        var id = $(this).parents('tr').find('.clientId').html();
        $('#amountV').val(result);
        $('#sample_2 tbody tr').each(function () {
            if ($(this)[0].rowIndex != 0) {
                var idspe = $(this).children().find(".clientId").html();
                if (idspe === id) {

                    $(this).children().parents('tr').removeClass("success");

                }
            }
        });
        var parentTr = $(this).parents('tr');
        $(parentTr).remove();
        createClientJson();    
        var count = countClient();
        $('#pass').val(count);
    });



    $(document).on('blur', '.amountValue', function () {
        var json = "{";
        var value = 0;
        var clientArr = [];
        var valueArr = [];
        var resultArr = [];
        $("#tableC tbody").find('.clientId').each(function (i) {
            clientArr.push('"' + $(this).text() + '"');
        });
        
        $("#tableC tbody").find('.amountValue').each(function(i) {
            valueArr.push('"' + $(this).val() + '"');
            value += parseFloat($(this).val());
        });
        
        if (clientArr.length == valueArr.length) {
            for (var i = 0; i < clientArr.length; i++) {
                resultArr.push('[' + clientArr[i] + ',' + valueArr[i] + ']');
            }
        }
        json += resultArr.join(',') + '}';
        
        $('#jsonClient').val(json);
        
        $('#amountV').val(value);
    });

    $(document).on('click', '#asigne', function() {
        var clientArr = [];
        $("#tableC tbody").find('.clientId').each(function (i) {
            clientArr.push(parseInt($(this).text()));
        });
        $("#tableClient tbody").find('.clientId').each(function (j) {
            
            for (var x = 0; x < clientArr.length; x++) {
                
                if (clientArr[x] == parseInt($(this).text())) {
                    var parentTr = $(this).parents('tr');
                    $(parentTr).remove();
                }
            }
        });
    });


    $(document).on('change', '#stateC', function () {
        $('#dateState').val('');
        $('#descriptionState').val('');
        
    });


   


});



var i = -1, toastCount = 0, $toastlast;

function MostrarT(msj,titlet,tipo) {

  
        var shortCutFunction = tipo;
        var msg = msj;
        var title = titlet;
              
        var toastIndex = toastCount++;

        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-center",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

               

        $("#toastrOptions").text("Command: toastr[" + shortCutFunction + "](\"" + msg + (title ? "\", \"" + title : '') + "\")\n\ntoastr.options = " + JSON.stringify(toastr.options, null, 2));

        var $toast = toastr[shortCutFunction](msg, title); // Wire up an event handler to a button in the toast, if it exists
        $toastlast = $toast;
        if ($toast.find('#okBtn').length) {
            $toast.delegate('#okBtn', 'click', function () {
                alert('you clicked me. i was toast #' + toastIndex + '. goodbye!');
                $toast.remove();
            });
        }
        if ($toast.find('#surpriseBtn').length) {
            $toast.delegate('#surpriseBtn', 'click', function () {
                alert('Surprise! you clicked me. i was toast #' + toastIndex + '. You could perform an action here.');
            });
        }
}


