
var montog = 0;
var proysupId = 0;
var proyid = 0;
var msjact;

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

    $('#'+area+'t'+' tbody tr').each(function (i) {
        if ($(this)[0].rowIndex != 0) {
            var idspe = $(this).children().find("#id").html();
            if (idspe == id) {
                var tdats = $('#'+area+'t').DataTable();
                tdats.row(i).remove().draw(false);

            }
        }
    });

    
    $("#table").DataTable().row.add(['<span id="area">' + area + '</span>', '<span id="id"  hidden="" style="display:none">' + id + '</span>', '<span id="nomb">' + esp + '</span>', '<button type="button" class="btn-sm" id="sel">' +
        '<span class="glyphicon glyphicon-user"></span></button>', '<button type="button" class="btn-sm" id="deleteRow">' +
        '<span class="glyphicon glyphicon-remove-sign"></span></button>']).draw();
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
   
  
    /*Trabajando tabla especalistas*/
  
    $('#table tbody tr').each(function (i) {
        if ($(this)[0].rowIndex != 0) {
            var idfase = $(this).children().find("#id").html();
            if (idfase == id) {
                
                var tdats = $('#table').DataTable();
                tdats.row(i).remove().draw(false);
              
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
    $("#" + area + 't').DataTable().row.add(['<a class="btn btn-sm green" id="Asignar"><span class="glyphicon glyphicon-arrow-down"></span>' +
                               '</a>', '<span id="area">' + area + '</span>', '<span id="id" hidden="">' + id + '</span>', '<span id="nomb">' + esp + '</span>']).draw();
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
    var head = '<tr><th>Area</th><th> </th><th>Nombre</th></tr>';
    $tabla.append(head);
    var area = $(this).parents('tr:first').find('#area').html();
    var id = $(this).parents('tr:first').find('#id').html();
    var esp = $(this).parents('tr:first').find('#nomb').html();
    var newrmsj = '<tr><td><span id="area">' + area + '</span></td>' +
        '<td><span id="id" hidden="">' + id + '</span></td>' +
        '<td><span id="nomb">' + esp + '</span></td>' +
        '<td> <div style="text-align: center;"><button type="button" class="btn btn-success btn-xs" id="delesp" >Eliminar</button>' +
        '</td>' +
        '</tr>';
    $tabla.append(newrmsj);
    
});

function MostrarMensaje()
{
    $('#lbmjs').empty();
    $('#modalmsj').modal('show');
    var $tabla = $("#tablamsj");
    $tabla.find("th").remove();
    $('#tablamsj').find("tr:gt(0)").remove();
    
   
    var newrmsj = '<tr><td>'+msjact+'!!!'+'</td></tr>';
    $tabla.append(newrmsj);
}


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
    $(this).parents('tr:first').find('#sel').css({ background: "#00bfff" });
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
            if (data === -1) {
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
                            $tabla.append('<tr><td><span id="idfase" hidden="">' + id + '</span></td>' +
                                '<td><span id="nfase">' + fase + '</span></td>' +
                                '<td><input type="number" value="0" class="form-control text-box single-line" required="true"  placeholder="valor" id="valord" value=0>' + "</td>" +
                                '<td><input type="number" value="0" class="form-control text-box single-line" required="true" placeholder="valor" id="valorm">' + "</td>" +
                                '</tr>');
                           
                        }
                       
                       
                     
                        var dataString = 'idsup=' + idsup;
                        $.ajax({
                            type: 'POST',
                            url: '/Projects/IsSupDetoCanc/',
                            data: dataString,
                            success: function (data) {
                                if (data == 1) {
                                    MostrarT('Este suplemento ha sido Detenido o Cancelado no se pueden ejecutar acciones','Informacion','warning')
                                    $('#alerta').empty();
                                    $('#alerta').append('Suplento Cancelado o Detenido!!!');
                                }
                            },
                            error: function (req, stat, err) {

                                alert(req.responseText);


                            }
                        });
                        
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
                var $tabla = $("#tablacron").DataTable();
               // $tabla.find("tr:gt(0)").remove();
                $tabla.rows().remove().draw(false);
                for (var f = 0; f < F.length; f++) {
                    var id = F[f].id;
                    var fase = F[f].fase;
                    var dias = F[f].dias;
                    var montoc = F[f].monto;
                    
                    $tabla.row.add(['<span id="idfase" hidden="">' + id + '</span>', '<span id="nfase">' + fase + '</span>', '<input type="number" class="form-control text-box single-line" required="true"  placeholder="valor" id="valord" value="' + dias + '">', '<input type="number" value="' + montoc + '" class="form-control text-box single-line" required="true" placeholder="valor" id="valorm">', '<a data-toggle="modal" href="#detallefase" class="btn btn-success btn-xs" id="detfase">Detalles</a>']).draw();

                }
               

                var dataString = 'idsup=' + idsup;
                $.ajax({
                    type: 'POST',
                    url: '/Projects/IsSupDetoCanc/',
                    data: dataString,
                    success: function (data) {
                        if (data == 1) {
                            MostrarT('Este suplemento ha sido Detenido o Cancelado no se pueden ejecutar acciones', 'Informacion', 'warning');
                            $('#alerta').empty();
                            $('#alerta').append('Suplento Cancelado o Detenido!!!');
                        }
                    },
                    error: function (req, stat, err) {

                        alert(req.responseText);


                    }
                });
               
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

    var id = proysupId;

    var dataString = 'proysupid=' + id;
    $.ajax({
        type: 'POST',
        url: '/Projects/IsSupDetoCanc/',
        data: dataString,
        success: function (data) {
            if (data == 1) {
                MostrarT('Este suplemento ha sido Detenido o Cancelado no se pueden ejecutar acciones', 'Informacion', 'info');

            } else {
                var jsonv = cronTojson();
                var contact = JSON.parse(jsonv);

                var c = contact.Cron;
                for (var f = 0; f < c.length; f++) {

                    var idf = c[f].idf;
                    var d = c[f].d;
                    var m = c[f].m;
                    var band = false;
                    dataString = 'id=' + id + '&idf=' + idf + '&d=' + d + '&m=' + m;
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

            }
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
   

   


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
                var statesup = F[f].statesup;

                var row = '<tr><td hidden=""><span id="iddet">' + iddet + '</span></td>' +
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
                   '<td><span id="statesup" hidden="">' + statesup + '</span></td>' +
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
    $('#esp001').hide();
    var idp = $('#idpt').val();
    var nombfase = $(this).parents('tr:first').find('#nfase').html();
    var iddet = $(this).parents('tr:first').find('#iddet').html();

    var check = $(this).parents('tr:first').find('#statesup').html();
    if (check == 1)
        check = 'disabled';
    else
        check = '';
    //Chequeando suplemento
   
    /////

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
            if (data !=-1) {
                var contact = JSON.parse(data);
                var F = contact.Esp;

                //Limpiando tabla
                $('#tablaesp tbody tr').each(function (i) {
                    if ($(this)[0].rowIndex != 0) {

                        var tdats = $('#tablaesp').DataTable();
                        tdats.row().remove().draw(false);


                    }
                });
                /////
                for (var f = 0; f < F.length; f++) {
                    var area = F[f].area;
                    var idspec = F[f].idspec;
                    var nombre = F[f].nombre;
                    var jefe = F[f].jefe;
                    if (jefe == "True") {
                        jefe = '<span class="glyphicon glyphicon-user"></span>';
                    } else {
                        jefe = '';
                    }

                    $('#tablaesp').DataTable().row.add(['<button type="button" class="btn btn-primary btn-xs" id="Asignarafase" ' + check + '><span class="glyphicon glyphicon-download-alt"></span>' +
                            '</button>', '<span id="area">' + area + '</span>',
                              '<span id="id" hidden="">' + idspec + '</span>', '<span id="nomb">' + nombre + '</span>', jefe]).draw();

                }
            }
           
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });

    //Asignados actualmente
    dataString = 'idddet=' + iddet;
    var cont = 0;
    //Limpiando tabla
    $('#tablaespfase tbody tr').each(function (i) {
        if ($(this)[0].rowIndex != 0) {

            var tdats = $('#tablaespfase').DataTable();
            tdats.row().remove().draw(false);


        }
    });
    /////
    $.ajax({
        type: 'POST',
        url: '/Projects/SpesProyAct/',
        data: dataString,
        success: function (data) {
           
            if (data != "") {
                var contact = JSON.parse(data);
                var F = contact.Asig;
               
              
                for (var f = 0; f < F.length; f++) {
                    var area = F[f].area;
                    var id = F[f].idspec;
                    var nombre = F[f].nombre;
                    var part = F[f].part;
                    cont += parseFloat(part);
                    
                   
                    $('#tablaespfase').DataTable().row.add(['<span id="area">' + area + '</span>','<span id="id" hidden="">' + id + '</span>',
                        '<span id="nomb">' + nombre + '</span>', '<span id="part">' + part + '</span>',  '<button type="button" class="btn btn-primary btn-xs" id="delfaseasig" data-placement="left" '+check+'>' +
                            '<span class="glyphicon glyphicon-remove-circle"></span></button>']).draw();
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
         '<td><input type="text" id="valor"></td>' +
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
            
            var id = data;         
            $('#tablaespfase').DataTable().row.add(['<span id="area">' + area + '</span>', '<span id="id" hidden="">' + id + '</span>',
                        '<span id="nomb">' + nombre + '</span>', '<span id="part">' + porcientop + '</span>', '<button type="button" class="btn btn-primary btn-xs" id="delfaseasig" data-placement="left">' +
                            '<span class="glyphicon glyphicon-remove-circle"></span></button>']).draw();
            $('#porciq').empty();
            var x = 100 - cont;
            $('#porciq').append(x);

            $('#tablaesp tbody tr').each(function (i) {
                if ($(this)[0].rowIndex != 0) {
                    var id = $(this).children().find("#id").html();
                    if (id == idprojspec) {
                        var table = $('#tablaesp').DataTable();
                        table.row(i).remove().draw(false);

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
$(document).on('click', '#delfaseasig', function () {
    $('#esp001').hide();
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
                    if (data !== "0") {
                        if (id === iddel) {
                            var table =$('#tablaespfase').DataTable();
                            table.row(i).remove().draw(false);
                        }
                    }
                   
                    if (data === '0') {
                        $('#esp001').show();
                       
                    } else {
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
    var idclien = $('#idclientt').val();
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
        var idcliente = $('#idclientt').val();
        var idproducto = $('#idprod').val();
      
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
                $('#detallefase').modal('hide');
                
                $('#detfase').click();//Revisar internet
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


//Editar factura Get

function check2() {
    document.getElementById("editar_f").disabled = "";
    $('#editar_f').tooltip('destroy');

    var mposible = $('#amount').val();
    var $vals = $('input[name=valosfact2]');
    var cont = 0;
    var numero = $('#numf2').val();
    if (numero == "") {
        document.getElementById("editar_f").disabled = "disabled";
        document.getElementById("editar_f").title = "La factura debe tener numero";
        $('#editar_f').tooltip('toggle');
        $('#editar_f').tooltip('show');
        return false;
    }

    if ($vals.length > 0) {
        for (var i = 0; i < $vals.length; i++) {
            var num = $vals.get(i).value;
            var temp = parseFloat(num);

            if (isNaN(temp) == true || temp < 0) {
                document.getElementById("editar_f").disabled = "disabled";
                document.getElementById("editar_f").title = "El monto a facturar tiene que ser un numero";
                $('#editar_f').tooltip('toggle');
                $('#editar_f').tooltip('show');
                return false;

            } else {
                cont += temp;
                if (cont == 0) {
                    $('#editar_f').tooltip('destroy');
                    document.getElementById("editar_f").disabled = "disabled";
                    document.getElementById("editar_f").title = "EL monto total a facturar no puede ser 0";
                    $('#editar_f').tooltip('toggle');
                    $('#editar_f').tooltip('show');
                    return false;
                }
                if (cont > mposible) {
                    $('#editar_f').tooltip('destroy');
                    document.getElementById("editar_f").disabled = "disabled";
                    document.getElementById("editar_f").title = "El monto a facturar (" + cont + ") tiene que ser menor que el monto posible (" + mposible + ")";
                    $('#editar_f').tooltip('toggle');
                    $('#editar_f').tooltip('show');
                    return false;
                }
            }
        }
    }

    return true;
}
$(document).on('change', '#valosfact2', function () {

    check2();
});

$(document).on('click', '#editfact001', function () {
    var idfact = $(this).parents('tr:first').find('#idff0').html();
    var iddet = $('#iddetalle').val();
    var dataString = 'id=' + idfact + '&iddetalle='+iddet;
    $.ajax({
        type: 'POST',
        url: '/Invoices/Edit/',
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

//Editar Factura Post

$(document).on('click', '#editar_f', function () {
    var iddet = $('#iddetalle').val();
    var idf = $('#idfact').val();
    var oldm = $('#amount').val();

    var $projDetailsSpecialistId = $('input[name=projDetailsSpecialistId]');
    var $montos = $('input[name=valosfact2]');
    var Smontos = '*';
    var Sids = '*';

    for (var i = 0; i < $montos.length; i++) {
        Smontos += $montos.get(i).value + '*'; 
        Sids += $projDetailsSpecialistId.get(i).value + '*';
    };

    var dataString = 'idfact=' + idf + '&iddet=' + iddet + '&projDetailsSpecialistId=' + Sids + '&montos=' + Smontos;
    $.ajax({
        type: 'POST',
        url: '/Invoices/Editar_Factura/',
        data: dataString,
        success: function (data) {
            $('#modalfact1').modal('hide');
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
});

//Eliminar Facturas Get
$(document).on('click', '#elimfact001', function () {

   
    var idfact = $(this).parents('tr:first').find('#idff0').html();   
    var dataString = 'id=' + idfact;
    $.ajax({
        type: 'POST',
        url: '/Invoices/Delete/',
        data: dataString,
        success: function (data) {
          $('#modalfact1').modal('hide');
            var $tabla = $("#tabladeletefact");
            $tabla.empty();
            $tabla.append(data);
            $('#modalfactdelete').modal('show');
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });

});

//Eliminar Factura Post
$(document).on('click', '#submitDelete', function () {
    var idfact = $('#facturaid').html();
    var dataString = 'id=' + idfact;
    $.ajax({
        type: 'POST',
        url: '/Invoices/DeleteConfirmed/',
        data: dataString,
        success: function (data) {
           
            $('#modalfactdelete').modal('hide');
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });

});



//ProyectoEnProceso
$(document).on('click', '#enp00', function () {
    var idp= $('#idpt').val();

    var dataString = 'id=' + idp;
    $.ajax({
        type: 'POST',
        url: '/Projects/EnProc/',
        data: dataString,
        success: function (data) {
            if (data === '0') {
                msjact = 'Ya este Proyecto esta En proceso';
                MostrarMensaje();
            }
            if (data === '1') {
                msjact = 'Estado del Proyecto cambiado a En proceso';
                $('#enp00').css({ background: "#47a447" }, { border: "#398439" });
                $('#det00').css({ background: "#566c88" }, { border: "#566c88" });
                $('#canc00').css({ background: "#566c88" }, { border: "#566c88" });
                $('#rasondet').empty();
                $('#rasoncanc').empty();
                MostrarMensaje();
            }
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
});

//ProyectoDetenido
$(document).on('click', '#det00', function () {
    var idp = $('#idpt').val();

   
    var rason = $('#rasondet').val();
    if (rason === '') {
        msjact = 'Proporcione un motivo para la detencion del proyecto';
        MostrarMensaje();
    } else {
        var dataString = 'id=' + idp + '&rason=' + rason;
        $.ajax({
            type: 'POST',
            url: '/Projects/DetP/',
            data: dataString,
            success: function (data) {
                if (data === '0') {
                    msjact = 'Ya este Proyecto esta Detenido';
                    MostrarMensaje();
                }
                if (data === '1') {
                    msjact = 'Estado del Proyecto cambiado a Detenido';
                    $('#enp00').css({ background: "#566c88" }, { border: "#566c88" });
                    $('#det00').css({ background: "#47a447" }, { border: "#398439" });
                    $('#canc00').css({ background: "#566c88" }, { border: "#566c88" });
                    $('#rasoncanc').empty();
                    MostrarMensaje();
                }
            },
            error: function (req, stat, err) {

                alert(req.responseText);


            }
        });
    }
   
});

$(document).on('click', '#canc00', function () {
    var idp = $('#idpt').val();


    var rason = $('#rasoncanc').val();
    if (rason === '') {
        msjact = 'Proporcione un motivo para la cancelación del proyecto';
        MostrarMensaje();
    } else {
        var dataString = 'id=' + idp + '&rason=' + rason;
        $.ajax({
            type: 'POST',
            url: '/Projects/CancProy/',
            data: dataString,
            success: function (data) {
                if (data === '0') {
                    msjact = 'Ya este Proyecto esta Cancelado';
                    MostrarMensaje();
                }
                if (data === '1') {
                    msjact = 'Estado del Proyecto cambiado a Cancelado';
                    $('#enp00').css({ background: "#566c88" }, { border: "#566c88" });
                    $('#canc00').css({ background: "#47a447" }, { border: "#398439" });
                    $('#det00').css({ background: "#566c88" }, { border: "#566c88" });
                    $('#rasondet').empty();
                    MostrarMensaje();
                }
            },
            error: function (req, stat, err) {

                alert(req.responseText);


            }
        });
    }

});




//Inicializando tablas fuera de Metronic
$(document).ready(function () {

    var dataString = '';
    
    $.ajax({
        type: 'POST',
        url: '/Projects/GetAreas/',
        data: dataString,
        success: function (data) {
            var contact = JSON.parse(data);
            var F = contact.Areas;
            for (var f = 0; f < F.length; f++) {
                var area = F[f].nomb + 't';
                InicializatT(area);

            }
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });

    InicializatT('table');
    InicializatT('tablesx');
    InicializatT('tablacron');
    InicializatT('tableClientt');
    InicializatT('tablecontrat1');
    InicializatT('tabP');
    InicializatT('tablaesp');
    InicializatT('tablaespfase');
    

   // var ntabla = '';
    
    

});


function InicializatT(ntabla) {

    var table = $('#' + ntabla);  
    
    // begin: third table
    table.dataTable({
        "lengthMenu": [
            [5, 15, 20, -1],
            [5, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 5,
        "language": {
            "lengthMenu": " _MENU_ records"
        },
        "columnDefs": [{  // set default column settings
            'orderable': true,
            'targets': [0]
        }, {
            "searchable": true,
            "targets": [0]
        }],
        "order": [
            [1, "asc"]
        ] // set first column as a default sort by asc
    });

    var tableWrapper = jQuery('#' + ntabla + '_wrapper');

    table.find('.group-checkable').change(function () {
        var set = jQuery(this).attr("data-set");
        var checked = jQuery(this).is(":checked");
        jQuery(set).each(function () {
            if (checked) {
                $(this).attr("checked", true);
            } else {
                $(this).attr("checked", false);
            }
        });
        jQuery.uniform.update(set);
    });

    tableWrapper.find('.dataTables_length select').select2(); // initialize select2 dropdown

}

