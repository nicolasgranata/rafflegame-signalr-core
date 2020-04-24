﻿const controlPanelConnection = new signalR.HubConnectionBuilder()
    .withUrl("/drawHub")
    .build();

controlPanelConnection.start().then(() => {
    document.getElementById("startGame").addEventListener("click", () => {
        controlPanelConnection.invoke("GetWinner").catch((err) => {
            console.error(err.toString());
        });
    });
}).catch((err) => {
    console.error(err.toString());
});