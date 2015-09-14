$(document).ready(function() {
    GetEstatos();

});

function GetEstatos(anno) {
    var dataString = 'year=' + anno;
    $.ajax({
        type: 'POST',
        url: '/Projects/ProysState/',
        data: dataString,
        success: function (data) {

            if (data != "") {
                var contact = JSON.parse(data);
                var F = contact.ArrayEstados;
                var Terminado = F[0].Terminado;
                var EnProceso = F[0].EnProceso;
                var Detenido = F[0].Detenido;
                var Cancelado = F[0].Cancelado;
                var seriess = [];
                seriess.push('Terminados');
                seriess.push('En Proceso');
                seriess.push('Detenidos');
                seriess.push('Cancelados');

                ////////////////////////
                var datas = [];
                datas.push([1,Terminado]);
                datas.push([2, EnProceso]);
                datas.push([3, Detenido]);
                datas.push([4, Cancelado]);
                var data = [];
                var series = Math.floor(Math.random() * 10) + 1;
                series = series < 5 ? 5 : series;

                for (var i = 0; i < 3; i++) {
                    data[i] = {
                        label: seriess[i],
                        data: datas[i]
                    }
                }

                var options = {
                    series: {
                        bars: { show: true }
                    },
                    bars: {
                        barWidth: 0.8,
                        lineWidth: 0, // in pixels
                        shadowSize: 0,
                        align: 'left'
                    },

                    grid: {
                        tickColor: "#eee",
                        borderColor: "#eee",
                        borderWidth: 1
                    }
                };


                $.plot($("#pie_chart_8"), data, {
                    series: {
                        pie: {
                            show: true,
                            radius: 300,
                            label: {
                                show: true,
                                formatter: function (label, series) {
                                    return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">' + label + '<br/>' + Math.round(series.percent) + '%</div>';
                                },
                                threshold: 0.1
                            }
                        }
                    },
                    legend: {
                        show: false
                    }
                });
               
             

            }
        },
        error: function (req, stat, err) {

            alert(req.responseText);


        }
    });
}