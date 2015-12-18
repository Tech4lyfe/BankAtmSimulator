$(function () {
    "use strict";
    $("#QuickCash").click(function () {
        var $CheckingAccountId = $("#checkingAccountId").val();

        $.ajax({
            url: "/Transaction/QuickCash",
            data:{checkingAccountId : $CheckingAccountId},
            success: function() {
                alert("Transaction Was Successful"); // or any other indication if you want to show
            }
        });
    });

    //$("#Transfer").click(function () {
    //    alert("TransferFundsdd");

    //    var $CheckingAccountId = $("#checkingAccountId").val();

    //    $.ajax({
    //        url: "Transaction/TransferFunds",
    //        data: { checkingAccountId: $CheckingAccountId },
    //        success: function() {
    //            alert("Transfer Was Successful");
    //        }

    //    });
    //});

})();