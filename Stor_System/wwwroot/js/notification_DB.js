var connectionNotification = new signalR.HubConnectionBuilder().withUrl("/hubs/notification").build();


connectionNotification.start().then(function () {
    connectionNotification.send("LoadMessages2")
});

connectionNotification.on("LoadNotification", function (message, counter, RREQ_counter) {
    var notificationCounter = document.getElementById("notificationCounter2");
    var REQ_notificationCounter2 = document.getElementById("REQ_notificationCounter2");
    notificationCounter.innerHTML = "<span>(" + counter + ")</span>";
    REQ_notificationCounter2.innerHTML = "<span>(" + RREQ_counter + ")</span>";
});
/**************************************************** End ********************************************************/








    




