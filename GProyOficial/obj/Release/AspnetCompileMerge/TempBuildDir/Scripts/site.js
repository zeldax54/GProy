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
        var resultArr = [];
        $("#tableC tbody").find('.clientId').each(function (i) {
            clientArr.push('"' + $(this).text() + '"');
        });

        $("#tableC tbody").find('.amountValue').each(function (i) {
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
    };

    function countClient() {
        var count = 0;
        $("#tableC tbody").find('.clientId').each(function (i) {
            count += 1;
        });
        return count;
    }

    $(document).on('click', '.sendClient', function() {
        var newRow = '<tr><td hidden="hidden" class="clientId">' + $(this).parents('tr').find('.clientId').html() + '</td>' +
            '<td class="clientName">' + $(this).parents('tr').find('.clientName').html() + '</td>' +
            '<td hidden="hidden" class="telephone">' + $(this).parents('tr').find('.telephone').html() + '</td>' +
            '<td hidden="hidden" class="email">' + $(this).parents('tr').find('.email').html() + '</td>' +
            '<td id="amountValue"><input type="number" class="form-control text-box single-line amountValue" required="true" value="0"></td>' +
            '<td><input type="image" src="../../Content/images/trash.png" class="sendRow" style="top: 25%;"></td></tr>';
        $("#tableC").find('tbody').append(newRow);
        var parentTr = $(this).parents('tr');
        $(parentTr).remove();
        createClientJson();
        var count = countClient();
        $('#pass').val(count);
    });

    $(document).on('click', '.sendRow', function() {
        var value = $(this).parents('tr').find('.amountValue').val();
        
        var valueAmount = $('#amountV').val();
        var result = parseFloat(valueAmount - value);

        
        $('#amountV').val(result);

        var newRow = '<tr><td hidden="hidden" class="clientId">' + $(this).parents('tr').find('.clientId').html() + '</td>' +
            '<td class="clientName">' + $(this).parents('tr').find('.clientName').html() + '</td>' +
            '<td class="telephone">' + $(this).parents('tr').find('.telephone').html() + '</td>' +
            '<td class="email">' + $(this).parents('tr').find('.email').html() + '</td>' +
            '<td><input type="image" src="../../Content/images/trash.png" class="sendClient"></td></tr>';
        $("#tableClient").find('tbody').append(newRow);
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

    var tf = setFilterGrid("tableClient");
    
});

