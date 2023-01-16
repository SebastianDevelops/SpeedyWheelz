(function () {
    // Defining a connection to the server hub.
    var myHub = $.connection.driverHub;
    // Setting logging to true so that we can see whats happening in the browser console log. [OPTIONAL]
    $.connection.hub.logging = true;
    // Start the hub
    $.connection.hub.start();

    // Client method to broadcast the message
    myHub.client.hello = function (message) {
        if (message === "Assigned") {
            $("#orderButton").text("Assigned");
            $("#orderButton").attr("disabled", true);
            $("#orderButton").removeClass("unassigned");
            $("#orderStatus").html('<p class="text-danger">Somebody is already assigned to this order<p/>');
            $("#viewOrders").html('<button class="btn btn-block mt-2 btn-success" id="orderDetails">Order details</button>');
        } else {
            $("#orderButton").text("Assign Order To Me");
            $("#orderButton").attr("disabled", false);
            $("#orderButton").addClass("unassigned");
            $("#orderStatus").html('<p class="text-success">Nobody is assigned to this order<p/>');
        }
    };

    //Button click jquery handler
    $("#orderButton").click(function () {
        // Call SignalR hub method
        var order = $(this).data('order-id');
        myHub.server.checkOrderStatus(order);
    });
}()); 

let isLoading = true;

setTimeout(() => {
    $(document).ready(function () {
        $("#orderButton").click();
    })

    // Task finished
    isLoading = false;
}, 2000);

setInterval(() => {
    if (isLoading) {
        // Show loader
        document.getElementById("ftco-loader").style.display = "block";
    } else {
        // Hide loader
        document.getElementById("ftco-loader").style.display = "none";
    }
}, 100);