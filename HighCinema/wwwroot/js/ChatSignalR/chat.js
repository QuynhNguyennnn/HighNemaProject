"use strict";

LoadConversations();
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", async function (senderId, message) {
    LoadConversations(senderId, message);
});

LoadConversations();

async function LoadConversations(senderId, message) {

    const currentUserId = await TakeSessionUserID();
    console.log(currentUserId);

    if (message !== undefined) {

        if (senderId === currentUserId) {
            const node = document.createElement("div");
            node.className = 'messages__item messages__item--operator';
            node.style = 'background-color: orange';
            const textnode = document.createElement("p");
            textnode.textContent = `${message}`;
            node.appendChild(textnode);
            document.getElementById("messagesList").appendChild(node);
        } else {
            const node = document.createElement("div");
            node.className = 'messages__item messages__item--visitor';
            const textnode = document.createElement("p");
            textnode.textContent = `${message}`;
            node.appendChild(textnode);
            document.getElementById("messagesList").appendChild(node);
        }
    }
}

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", async function (event) {
    var message = document.getElementById("messageInput").value;
    var senderId = await TakeSessionUserID();
    console.log(senderId);

    connection.invoke("SendMessage", senderId, message).catch(function (err) {
        console.error(err.toString());
    });
   

    document.getElementById("messageInput").value = "";
    event.preventDefault();
});



$(document).ready(() => {
    TakeSessionUserID();
});


//function getUserIdFromLocalStorage() {
//    var sessionUserId = localStorage.getItem("SessionUserID");
//    return sessionUserId;
//}

async function TakeSessionUserID() {
    try {
        const SessionUserId = await $.ajax({
            url: '/Account/GetSessionUserId',
            method: 'GET'
        });
        if (SessionUserId) {
            console.log(SessionUserId);
            //localStorage.setItem("SessionUserID", SessionUserId);
            return SessionUserId.toString();
        }
        return;
    }
    catch (error) {
        return false;
    }
};