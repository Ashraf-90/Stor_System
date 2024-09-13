var connectionNotification = new signalR.HubConnectionBuilder().withUrl("/hubs/notification").build();
document.getElementById("sendButton").disabled = true;

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("notificationInput").value;
    connectionNotification.send("SendMessage", message).then(function () {
        document.getElementById("notificationInput").value = "";
    });
    event.preventDefault();
});

connectionNotification.start().then(function () {
    connectionNotification.send("LoadMessages");
    document.getElementById("sendButton").disabled = false;
});

connectionNotification.on("LoadNotification", function (message, counter, RREQ_counter) {
    var notificationCounter = document.getElementById("notificationCounter");
    var REQ_notificationCounter = document.getElementById("REQ_notificationCounter");


    notificationCounter.innerHTML = "<span>(" + counter +  ")</span>";
    REQ_notificationCounter.innerHTML = "<span>(" +  RREQ_counter + ")</span>";
});

/**************************************************** End ********************************************************/



















    //connectionNotification.on("LoadNotification", function (message, counter) {
    //    document.getElementById("messageList").innerHTML = "";
    //    var notificationCounter = document.getElementById("notificationCounter");
    //    notificationCounter.innerHTML = "<span>(" + counter + ")</span>";

    //    var maxVisibleNotifications = 4; // Number of notifications to show initially
    //    var messageList = document.getElementById("messageList");
    //    var showMoreBtn = document.getElementById("showMoreBtn");

    //    // Generate notifications
    //    for (let i = 0; i < message.length; i++) {
    //        var a = document.createElement("a");
    //        a.className = "dropdown-item preview-item";
    //        a.href = "URL_HERE"; // Replace with the actual URL or a dynamic URL if necessary

    //        var divThumbnail = document.createElement("div");
    //        divThumbnail.className = "preview-thumbnail";

    //        var img = document.createElement("img");
    //        img.src = "/a_stor_system/assets/images/Logos/Taswer_S_Logo.png"; // Adjust the image source as needed
    //        img.alt = "image";
    //        img.className = "profile-pic";
    //        divThumbnail.appendChild(img);

    //        var divContent = document.createElement("div");
    //        divContent.className = "preview-item-content d-flex align-items-start flex-column justify-content-center";

    //        var h6 = document.createElement("h6");
    //        h6.className = "preview-subject ellipsis mb-1 font-weight-normal";
    //        h6.textContent = "Project :" + message[i]; // Use the message from the array
    //        divContent.appendChild(h6);

    //        var p = document.createElement("p");
    //        p.className = "text-gray mb-0";

    //        var now = new Date();
    //        var formattedTime = now.toLocaleString(); // Format the date and time as desired
    //        p.textContent = formattedTime;
    //        divContent.appendChild(p);

    //        a.appendChild(divThumbnail);
    //        a.appendChild(divContent);

    //        // Append notification to the message list
    //        var li = document.createElement("li");
    //        li.appendChild(a);
    //        messageList.appendChild(li);

    //        // Hide notifications beyond the initial limit
    //        if (i >= maxVisibleNotifications) {
    //            li.style.display = "none";
    //        }
    //    }

    //    // Show the "Show More" button if there are more than 4 notifications
    //    if (message.length > maxVisibleNotifications) {
    //        showMoreBtn.style.display = "block";
    //    }

    //    // Add event listener for the "Show More" button
    //    showMoreBtn.addEventListener("click", function () {
    //        var items = messageList.getElementsByTagName("li");
    //        for (let i = maxVisibleNotifications; i < items.length; i++) {
    //            items[i].style.display = "block";
    //        }
    //        showMoreBtn.style.display = "none"; // Hide the button after all notifications are shown
    //    });
    //});







    




