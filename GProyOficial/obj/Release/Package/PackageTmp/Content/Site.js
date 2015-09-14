
var montog = 0;
var proysupId = 0;
var proyid = 0;


//$('#Crear').click(function () {
//    var _griddata = gridTojson();
//    var x = $('#jsonid');
//    x[0].defaultValue = '';
//    x[0].defaultValue = _griddata;


//});


//function gridTojson() {
//    var json = '{';
//    var otArr = [];
//    var cont = 0;
//    var tbl2 = $('#table tbody tr').each(function (i) {
//        if ($(this)[0].rowIndex != 0) {
//            x = $(this).children();
//            var itArr = [];

//            var id = $(this).children().find("#id").html();
//            var sel = $(this).children().find("#sel option:selected").html();


//            itArr.push('"' + id + '"');
//            itArr.push('"' + sel + '"');
//            otArr.push('"' + i + '": [' + itArr.join(',') + ']');


//        }
//    });
//    json += otArr.join(",") + '}';
//    return json;
//}

function cronTojson() {
    var json = '{"Cron":[';
    var otArr = [];
    var cont = 0;
    var tbl2 = $('#tablacron tbody tr').each(function (i) {
        if ($(this)[0].rowIndex != 0) {
            x = $(this).children();
            var itArr = [];

            var idfase = $(this).children().find("#idfase").html();
            var dias = $(this).children().find('#valord').val();
            var monto = $(this).children().find('#valorm').val();


            itArr.push('"idf":'+'"' + idfase + '"');
            itArr.push('"d":' + '"' + dias + '"');
            itArr.push('"m":' + '"' + monto + '"');
            otArr.push('{' + itArr.join(',') + '}');


        }
    });
    json += otArr.join(",") + ']}';
    return json;
}

$(document).on('click', '#Asignar', function () {
    var area = $(this).parents('tr').find('#area').html();
    var id = $(this).parents('tr').find('#id').html();
    var esp = $(this).parents('tr').find('#nomb').html();
    var newRow = '<tr><td><span id="area">' + area + '</span></td>' +
         '<td><span id="id">' + id + '</span></td>' +
        '<td><span id="nomb">' + esp + '</span></td>' +
        '<td><button type="button" class="btn btn-default btn-sm" id="sel">' +
        '<span class="glyphicon glyphicon-user"></span></button>' +
        '<td><button type="button" class="btn btn-default btn-sm" id="deleteRow">' +
        '<span class="glyphicon glyphicon-remove-sign"></span></button></td>' +
        '</tr>';
    $(this).parents('tr').remove();
    $("#table").find('tbody').append(newRow);
    /*Agregando especialista en BD*/
    var projectid = $("#projectId").val();
    var dataString = 'idproy=' + projectid + '&idesp=' + id+'&jefe=false';
    $.ajax({
        type: 'POST',
        url: '/Projects/AddEspProy/',
        data: dataString,
        success: function (data) {
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
    /*Agregando especialista en BD*/
    var cont = $("#cont").html();
    cont++;
    $("#cont").empty();
    $("#cont").append(cont);
});



/*Eliminando especialista de bd*/
$(document).on('click', '#delesp', function () {

    var area = $(this).parents('tr:first').find('#area').html();
    var id = $(this).parents('tr:first').find('#id').html();
    var esp = $(this).parents('tr:first').find('#nomb').html();
    var newRow1 = '<tr> <td> <button type="button" class="btn btn-default btn-lg" id="Asignar">' +
      '<span class="glyphicon glyphicon-download-alt"></span></button></td>' +
      '<td><span id="area">' + area + '</span></td>' +
      '<td><span id="id">' + id + '</span></td>' +
      '<td><span id="nomb">' + esp + '</span></td>' +
      '</tr>';
  
    /*Trabajando tabla especalistas*/
  
    $('#table tbody tr').each(function (i) {
        if ($(this)[0].rowIndex != 0) {
            var idfase = $(this).children().find("#id").html();
            if (idfase == id) {
                var table = document.getElementById('table');
                table.deleteRow(i+1);
            }
        }
    });
    /*Eliminando realmente el especialista de la BD*/
    var projectid = $("#projectId").val();
    var dataString = 'idproy=' + projectid + '&idesp=' + id;
    $.ajax({
        type: 'POST',
        url: '/Projects/DeleteEspProy/',
        data: dataString,
        success: function (data) {
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
    /*Eliminando realmente el especialista de la BD*/


    $('#modalmsj').modal('hide');
    $("#" + area).find('tbody').append(newRow1);
    var cont = $("#cont").html();
    cont--;
    $("#cont").empty();
    $("#cont").append(cont);
    /*Trabajando tabla especalistas*/
});

/*Mensaje eliminar especialista*/
$(document).on('click', '#deleteRow', function () {
    $('#lbmjs').empty();
    $('#lbmjs').append('Se eliminara el especialista del Proyecto');
    $('#modalmsj').modal('show');
    var $tabla = $("#tablamsj");
    $tabla.find("th").remove();
    $('#tablamsj').find("tr:gt(0)").remove();
    var head = '<tr><th>Area</th><th>ID</th><th>Nombre</th></tr>';
    $tabla.append(head);
    var area = $(this).parents('tr:first').find('#area').html();
    var id = $(this).parents('tr:first').find('#id').html();
    var esp = $(this).parents('tr:first').find('#nomb').html();
    var newrmsj = '<tr><td><span id="area">' + area + '</span></td>' +
        '<td><span id="id">' + id + '</span></td>' +
        '<td><span id="nomb">' + esp + '</span></td>' +
        '<td> <div style="text-align: center;"><button type="button" class="btn btn-success btn-xs" id="delesp">Eliminar</button>'+
        '</td>' +
        '</tr>';
    $tabla.append(newrmsj);
    
});


$(document).on('click', '#asig', function () {
    $(".collapse").collapse();

});

/*Asignar jefe de proyecto*/
$(document).on('click', '#sel', function () {
    var id = $(this).parents('tr:first').find('#id').html();
    var projectid = $("#projectId").val();
    var dataString = 'projectid=' + projectid + '&idesp=' + id;
    $.ajax({
        type: 'POST',
        url: '/Projects/SetJefeP/',
        data: dataString,
        success: function (data) {
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
  
    /*darle Css al boton*/
});
/*--------------------------------------------------------------*/

/*Llenar Cronograma con fases correspondientes*/
$(document).on('click', '#crono', function () {
    $('#alertaerror').hide();
    var idserv = $(this).parents('tr:first').find('#sid').html();
    var projectid = $("#projectId").val();
    var idsup = $(this).parents('tr:first').find('#supid').html();
    var sname = $(this).parents('tr:first').find('#supname').html();
    var servname = $(this).parents('tr:first').find('#servname').html();
    var projsupid = $(this).parents('tr:first').find('#projsupid').html();
    proysupId = projsupid;
    var monto = $(this).parents('tr:first').find('#monto').html();
    montog = monto;
    var band = false;
    $.ajax({
        type: 'POST',
        url: '/Projects/Cronograma/' + projsupid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == -1) {
                $.ajax({
                    type: 'POST',
                    url: '/Projects/GetPhasesname/' + idserv,

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {                       
                        var contact = JSON.parse(data);
                        var F = contact.Fases;
                        var $tabla = $("#tablacron");
                        $tabla.find("tr:gt(0)").remove();
                        for (var f=0;f<F.length;f++) {
                            var id = F[f].id;
                            var fase = F[f].fase;
                            $tabla.append('<tr><td><span id="idfase">' + id + '</span></td>' +
                                '<td><span id="nfase">' + fase + '</span></td>' +
                                '<td><input type="number" value="0" class="form-control text-box single-line" required="true"  placeholder="valor" id="valord" value=0>' + "</td>" +
                                '<td><input type="number" value="0" class="form-control text-box single-line" required="true" placeholder="valor" id="valorm">' + "</td>" +
                                "</tr>");
                           
                        }
                     
                     

                        
                        $('#titcron').empty();
                        $('#titcron').append('Cronograma para el Suplemento: ').append('' + sname + "<br> Servicio : " + servname+'</br> Monto a Dividir: '+monto);
                        $('#psid').empty();
                        $('#psid').append(projsupid);
                        $('#modalsup').modal('show');
                        var $q = $('quedan');
                        $q.empty();
                        $q.append(0);
                        $('#savecron').hide();
                        $('#alerta').show();
                        $('#alertad').hide();
                    },
                    error: function (req, stat, err) {

                        alert(req.responseText);


                    }
                });
            } else {
                var contact = JSON.parse(data);
                var F = contact.Cron;
                var $tabla = $("#tablacron");
                $tabla.find("tr:gt(0)").remove();
                for (var f = 0; f < F.length; f++) {
                    var id = F[f].id;
                    var fase = F[f].fase;
                    var dias = F[f].dias;
                    var montoc = F[f].monto;
                    var row = '<tr><td><span id="idfase">' + id + '</span></td>' +
                        '<td><span id="nfase">' + fase + '</span></td>' +
                        '<td><input type="number" class="form-control text-box single-line" required="true"  placeholder="valor" id="valord" value="' + dias + '">' + "</td>" +
                        '<td><input type="number" value="' + montoc + '" class="form-control text-box single-line" required="true" placeholder="valor" id="valorm">' + "</td>" +
                       
                        '<td><a data-toggle="modal" href="#detallefase" class="btn btn-success btn-xs" id="detfase">Detalles</a></td></tr>"';
                    $tabla.append(row);

                }
                $('#titcron').empty();
                $('#titcron').append('Cronograma para el Suplemento: ').append('' + sname + "<br> Servicio : " + servname + '</br> Monto a Dividir: ' + monto);
                $('#psid').empty();
                $('#psid').append(projsupid);
                $('#modalsup').modal('show');
                var $q = $('quedan');
                $q.empty();
                $q.append(0);
                $('#alerta').hide();
                $('#alertad').show();
              

            }
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });


});


/*Salvar Cronograma */
$(document).on('click', '#savecron', function () {
    var jsonv = cronTojson();
    var id = proysupId;
    var contact = JSON.parse(jsonv);
    var c = contact.Cron;
    for (var f = 0; f < c.length; f++) {
       
            var idf = c[f].idf;
            var d = c[f].d;
            var m = c[f].m;
             var band = false;
            var dataString = 'id=' + id + '&idf=' + idf + '&d=' + d + '&m=' + m;
            $.ajax({
                type: 'POST',
                url: '/Projects/SCron/',
                data: dataString,
                success: function (data) {
                    if (data == '-1') {
                        $('#alertaerror').show();
                    } else {
                        $('#alertaerror').hide();
                    }

                },
                error: function (req, stat, err) {

                    alert(req.responseText);


                }
            });

        
    }


   


});

/*Calcular dinamicamente lo que va quedando por asignar en el cronograma*/
$(document).on('change', '#valorm', function () {
    var cont = 0;
    $('#savecron').show();
    $('#tablacron tbody tr').each(function () {
        if ($(this)[0].rowIndex != 0) {
            var monto = $(this).children().find('#valorm').val();
            cont += parseFloat(monto);
            
        }
    });
    $('#quedan').empty();
    $('#quedan').append((parseFloat(montog) - cont));
    if((parseFloat(montog) - cont)!=0) {
        $('#savecron').hide();
    }
});


/*Detalles por Fases en detalle_Cronograma(fase)*/
$(document).on('click', '#detfase', function () {

    var idfase = $(this).parents('tr:first').find('#idfase').html();
    var psid = $('#psid').html();
    var dataString = 'proysupid=' + psid + '&idfase=' + idfase;
    $.ajax({
        type: 'POST',
        url: '/Projects/GetDetallesFase/',
        data: dataString,
        success: function (data) {
            var detalles = JSON.parse(data);
            var F = detalles.Det;
            var $tabla = $("#tabdetfase");
            $tabla.find("tr:gt(0)").remove();
            for (var f = 0; f < F.length; f++) {
                var iddet = F[f].iddet;
                var nombfase = F[f].nombfase;
                var tc = F[f].tc;
                var tf = F[f].tf;
                var tpf = F[f].tpf;

                var row = '<tr><td><span id="iddet">' + iddet + '</span></td>' +
                    '<td hidden=""><span id="idfase">' + idfase + '</span></td>' +
                   '<td><span id="nfase">' + nombfase + '</span></td>' +
                   '<td><span id="nfase">' + tc + '</span></td>' +
                   '<td>' + tf + '</td>' +
                    '<td>' + tpf + '</td>' +
                    '<td> <a data-toggle="modal" href="#modalspec" class="btn btn-success btn-xs" id="asigesp">' +
                           '<span class="glyphicon glyphicon-user" ></span>' +
                       '</a>' +
                   '</td>' +
                   '<td> <a  class="btn btn-success btn-xs" id="facturarfase">' +
                           '<span class="glyphicon glyphicon-usd" ></span>' +
                       '</a>' +
                   '</td>' +
                    '</tr>';
                $tabla.append(row);
            }
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }

    });

});




/*start Asignar especialistas a las fases*/
$(document).on('click', '#asigesp', function () {
    var idp = $('#idpt').val();
    var nombfase = $(this).parents('tr:first').find('#nfase').html();
    var iddet = $(this).parents('tr:first').find('#iddet').html();
    $('#detalleact').empty();
    $('#detalleact').append(iddet);
    $('#nombfase').empty();
    $('#nombfase').append(nombfase);
    var dataString = 'idp=' + idp + '&idddet=' + iddet;
    //Posibles

    $.ajax({
        type: 'POST',
        url: '/Projects/SpesProy/',
        data: dataString,
        success: function (data) {
            var contact = JSON.parse(data);
            var F = contact.Esp;
            var $tabla = $("#tablaesp");
            $tabla.find("tr:gt(0)").remove();
            for (var f = 0; f < F.length; f++) {
                var area = F[f].area;
                var idspec = F[f].idspec;
                var nombre = F[f].nombre;
                var jefe = F[f].jefe;
                if (jefe == "True") {
                    jefe = '<div style="text-align:center"><span class="glyphicon glyphicon-user"></span></div>';
                } else {
                    jefe = '';
                }
                var row = '<tr><td> <button type="button" class="btn btn-default btn-sm" id="Asignarafase">' +
                            '<span class="glyphicon glyphicon-download-alt"></span>' +
                        '</button>' +
                    '</td><td><span id="area">' + area + '</span></td>' +
                    '<td hidden=""><span id="id">' + idspec + '</span></td>' +
                    '<td><span id="nomb">' + nombre + '</span></td>' +
                    '<td>' + jefe + '</td></tr>';
                $tabla.append(row);
            }
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });

    //Asignados actualmente
    var $tabla = $("#tablaespfase");
    $tabla.find("tr:gt(0)").remove();
    dataString = 'idddet=' + iddet;
    var cont =0;
    $.ajax({
        type: 'POST',
        url: '/Projects/SpesProyAct/',
        data: dataString,
        success: function(data) {
            if (data != "") {
                var contact = JSON.parse(data);
                var F = contact.Asig;
                var $tabla = $("#tablaespfase");
                $tabla.find("tr:gt(0)").remove();
                for (var f = 0; f < F.length; f++) {
                    var area = F[f].area;
                    var id = F[f].idspec;
                    var nombre = F[f].nombre;
                    var part = F[f].part;
                    cont += parseFloat(part);
                    var row = '<tr><td><span id="area">' + area + '</span></td>'
                    + '<td><span id="id">' + id + '</span></td>'
                     + '<td><span id="nomb">' + nombre + '</span></td>'
                       + '<td><span id="part">' + part + '</span></td>'
                    + '<td> <button type="button" class="btn btn-default btn-sm" id="delfaseasig">' +
                            '<span class="glyphicon glyphicon-remove-circle"></span>' +
                        '</button>' +
                    '</td></tr>';
                    $tabla.append(row);
                    $('#porciq').empty();
                    var x = 100 - cont;
                    $('#porciq').append(x);
                }
            }


           
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
    $('#porciq').empty();
    var x = 100 - cont;
    $('#porciq').append(x);
});

/*Mensaje solicitando porciento de participacion*/
$(document).on('click', '#Asignarafase', function () {
   
    $('#lbmjs').empty();
    $('#lbmjs').append('Indique porciento de Participación del especialista en la Fase.');
    var valor = $('#porciq').html();
    $('#prestm').empty();
    $('#prestm').append('Queda->' + ' ' + valor);
    $('#modalmsj').modal('show');
    var $tabla = $("#tablamsj");
    $tabla.find("th").remove();
    $('#tablamsj').find("tr:gt(0)").remove();
    var head = '<tr><th>Area</th><th>ID</th><th>Nombre</th><th>Participación.(%)</th></tr>';
    $tabla.append(head);
    var area = $(this).parents('tr:first').find('#area').html();
    var id = $(this).parents('tr:first').find('#id').html();
    var esp = $(this).parents('tr:first').find('#nomb').html();
    var newrmsj = '<tr><td><span id="area">' + area + '</span></td>' +
        '<td><span id="id">' + id + '</span></td>' +
        '<td><span id="nomb">' + esp + '</span></td>' +
         '<td><input type="number" id="valor" placeholder="0<%<=100"></td>' +
        '<td> <div style="text-align: center;"><button type="button" class="btn btn-success btn-sm" id="Tasigf">Asignar</button>' +
        '</td>' +
        '</tr>';
    $tabla.append(newrmsj);
    
});

//Asignar especialista a fase
$(document).on('click', '#Tasigf', function () {
    $('#prestm').empty();
   

    var iddet = $('#detalleact').html();
    var idprojspec = $(this).parents('tr:first').find('#id').html();
    var porcientop = $(this).parents('tr:first').find('#valor').val();
    //actualizando porciento restante
    var rest = parseFloat($('#porciq').html());
    $('#porciq').empty();
    $('#porciq').append(rest - parseFloat(porcientop));
    // fin actualizando porciento restante
    var area = $(this).parents('tr:first').find('#area').html();
    var nombre = $(this).parents('tr:first').find('#nomb').html();
   

    var dataString = 'idprojspec=' + idprojspec + '&projdetid=' + iddet + '&valor=' + porcientop;
    $.ajax({
        type: 'POST',
        url: '/Projects/Especialistafase/',
        data: dataString,
        success: function (data) {
            var $tabla = $("#tablaespfase");
            var id = data;
            var row = '<tr><td><span id="area">' + area + '</span></td>'
                    + '<td><span id="id">' + id + '</span></td>'
                     + '<td><span id="nomb">' + nombre + '</span></td>'
                       + '<td><span id="part">' + porcientop + '</span></td>'
                    + '<td> <button type="button" class="btn btn-default btn-sm" id="delfaseasig">' +
                            '<span class="glyphicon glyphicon-remove-circle"></span>' +
                        '</button>' +
                    '</td><tr>';
            $tabla.append(row);

            $('#tablaesp tbody tr').each(function (i) {
                if ($(this)[0].rowIndex != 0) {
                    var id = $(this).children().find("#id").html();
                    if (id == idprojspec) {
                        var table = document.getElementById('tablaesp');
                        table.deleteRow(i);

                    }
                }
            });
            $('#modalmsj').modal('hide');
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
    
});

//Eliminar especialista de fase
$(document).on('click', '#delfaseasig', function() {
    var iddel = $(this).parents('tr:first').find('#id').html();
    var dataString = 'id=' + iddel;
    $.ajax({
        type: 'POST',
        url: '/Projects/EspecialistafaseDelete/',
        data: dataString,
        success: function (data) {
            $('#tablaespfase tbody tr').each(function (i) {
                if ($(this)[0].rowIndex != 0) {
                    var id = $(this).children().find("#id").html();
                    if (id == iddel) {
                        var table = document.getElementById('tablaespfase');
                        table.deleteRow(i);
                        $('#modalspec').modal('hide');
                    }
                }
            });
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
});

//Validando en porciento de participacion
$(document).on('change', '#valor', function () {
    $('#Tasigf').show();
    if (parseFloat($('#valor').val())) {
        var valor = parseFloat($('#porciq').html());
        var valoact = parseFloat($('#valor').val());
        if (valoact > valor) {
            $('#Tasigf').hide();
        }

    } else {
        $('#Tasigf').hide();
    }
   
});


//Mensaje solicitando porciento de facturacion de la fase
$(document).on('click', '#facturarfase', function () {
    var iddet = $(this).parents('tr:first').find('#iddet').html();
    var dataString = 'iddet='+iddet;
    $.ajax({
        type: 'POST',
        url: '/Invoices/Index/',
        data: dataString,
        success: function (data) {
            var $tabla = $("#tablacreatefact");
            $tabla.empty();
            $tabla.append(data);
            $('#modalfact1').modal('show');
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });


});

$(document).on('click', '#nuevafact', function () {
    var iddet = $('#iddetalle').val();
    var idclien = $('#idclient').val();
    var idprod = $('#idprod').val();
    var dataString = 'iddet=' + iddet + '&idclien=' + idclien + '&idprod=' + idprod;
    $.ajax({
        type: 'POST',
        url: '/Invoices/CreateAlter/',
        data: dataString,
        success: function (data) {
            var $tabla = $("#tablacreatefact");
            $tabla.empty();
            $tabla.append(data);
            $('#modalfact1').modal('show');
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
});


$(document).on('change', '#valosfact', function () {

    check();
});

$(document).on('change', '#numf', function () {

    check();
});
function check() {
    document.getElementById("facturar").disabled = "";
    $('#facturar').tooltip('destroy');

    var mposible = $('#amount').val();
    var $vals = $('input[name=valosfact]');
    var cont = 0;
    var numero = $('#numf').val();
    if (numero == "") {
        document.getElementById("facturar").disabled = "disabled";
        document.getElementById("facturar").title = "La factura debe tener numero";
        $('#facturar').tooltip('toggle');
        $('#facturar').tooltip('show');
        return false;
    }

    if ($vals.length > 0) {
        for (var i = 0; i < $vals.length; i++) {
            var num = $vals.get(i).value;
            var temp = parseFloat(num);

            if (isNaN(temp) == true || temp < 0) {
                document.getElementById("facturar").disabled = "disabled";
                document.getElementById("facturar").title = "El monto a facturar tiene que ser un numero";
                $('#facturar').tooltip('toggle');
                $('#facturar').tooltip('show');
                return false;

            } else {
                cont += temp;
                if (cont == 0) {
                    $('#facturar').tooltip('destroy');
                    document.getElementById("facturar").disabled = "disabled";
                    document.getElementById("facturar").title = "EL monto total a facturar no puede ser 0";
                    $('#facturar').tooltip('toggle');
                    $('#facturar').tooltip('show');
                    return false;
                }
                if (cont > mposible) {
                    $('#facturar').tooltip('destroy');
                    document.getElementById("facturar").disabled = "disabled";
                    document.getElementById("facturar").title = "El monto a facturar ("+cont+") tiene que ser menor que el monto posible ("+mposible+")";
                    $('#facturar').tooltip('toggle');
                    $('#facturar').tooltip('show');
                    return false;
                }
            }
        }
    }

    return true;
}
$(document).on('click', '#facturar', function () {
    if (check()==true) {
        var iddet = $('#iddetalle').val();
        var idcliente = $('#idclient').val();
        var idproducto = $('#productId').val();
        var fecha = $('#fecha').val();
        var idestado = $('#stateid').val();
        var numero = $('#numf').val();
        var $projDetailsSpecialistId = $('input[name=projDetailsSpecialistId]');
        var $montos = $('input[name=valosfact]');

        var Smontos = '*';
        var Sids = '*';
        for (var i = 0; i < $montos.length; i++) {
            Smontos += $montos.get(i).value + '*';
            Sids += $projDetailsSpecialistId.get(i).value + '*';
        };
        //Ajax
        var dataString = 'iddetalle='+iddet + '&idcliente=' + idcliente + '&idproducto=' + idproducto + '&idestado=' + idestado +
            '&fecha=' + fecha + '&numero=' + numero + '&projDetailsSpecialistId=' + Sids +
            '&montos='+Smontos;
        $.ajax({
            type: 'POST',
            url: '/Invoices/Crear_Factura/',
            data: dataString,
            success: function (data) {
                $('#modalfact1').modal('hide');
            },
            error: function (req, stat, err) {

                alert(req.responseText);


            }
        });
    }

});

//Eventos Modal
$('#modalfact1').on('shown.bs.modal', function() {
    $('#detallefase').modal('hide');

});
$('#modalfact1').on('hide.bs.modal', function () {
    $('#detfase').click();

});
