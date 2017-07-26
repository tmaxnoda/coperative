


$(function () {

    $('#combobox').on('change', function (e) {
        var id = $("#combobox").val();
        e.preventDefault();


         $.ajax({
             url: '/Loan/GetAccountNumber',
            data: { id: id },
            method: 'GET',
            success: function (generateAccountNumber) {
                //$("#div1").html(generatePreviousPayment);
               
                $("input[name='AccountNum']").val(generateAccountNumber);
            },
            error: function () {
                alert('error');
            }

         });


         $.ajax({
             url: '/Loan/GetRegistrationNumber',
            data: { id: id },
            method: 'GET',
            success: function (generateRegistrationNumber) {
                //$("#div1").html(generatePreviousPayment);
               
                $("input[name='RegistratinNum']").val(generateRegistrationNumber);
            },
            error: function () {
                alert('error');
            }

        });


        $.ajax({
            url: '/ChangeOfPayments/GetPreviousPayment',
            data: { id: id },
            method: 'GET',
            success: function (generatePreviousPayment) {
                //$("#div1").html(generatePreviousPayment);
                $("input[name='div1']").val(generatePreviousPayment);
                $("input[name='div1Text']").val(generatePreviousPayment);
            },
            error: function () {
                alert('error');
            }

        });

        $.ajax({
            
             url: '/ChangeOfPayments/getNameResult',
            data: { id: id },
            method: 'GET',
            success: function (generatePreviousName) {
               
                //$("#div1").html(generatePreviousPayment);
                //alert("I am About to change name");
                $("input[name='Name']").val(generatePreviousName);
            },
            error: function () {
                alert('error');
            }

        });

        $.ajax({

            url: '/Loan/getActiveLoanResult',
            data: { id: id },
            method: 'GET',
            success: function (generateActiveLoan) {
                var isActive = generateActiveLoan

                //alert("Hey Am i on loan? :" + isActive);
                
                if (isActive === true) {
                    
                    ShowCustomDialogFailedNew();
                    $("#calculate").prop("disabled", true);
                    $("#button").prop("disabled", true);
                    return false;
                }

                else {
                    // DoSomethingElse()

                    ShowCustomDialogSuccessNew();
                    $("#button").prop("disabled", false);
                    $("input[name='div1Text2']").val(GetTotalHistoricalRecordPayments);
                    return true;
                }
                ////$("#div1").html(generatePreviousPayment);
                //alert("I am About to change name");
                //$("input[name='divName']").val(generatePreviousName);
            },
            error: function () {
                alert('error');
            }

        });

       

    });
});


function ShowCustomDialogFailedNew() {

    ShowDialogBox('Warning', 'You have a pending loan yet to be completed.', 'Ok', '', 'GoToAssetList', null);
}

function ShowCustomDialogSuccessNew() {

    ShowDialogBox('Warning', 'No pending loan', 'Ok', '', 'GoToAssetList', null);
}






