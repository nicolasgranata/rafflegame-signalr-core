const drawConnection = new signalR.HubConnectionBuilder()
    .withUrl("/drawHub")
    .build();

const participate = () => {
    document.getElementById("participatePannel").classList.add("d-none");
    document.getElementById("connectingPanel").classList.remove("d-none");

    drawConnection.start().then(() => {
        setTimeout(() => {
            document.getElementById("welcomePanel").classList.add("d-none");
            document.getElementById("lobby").classList.remove("d-none");
        }, 3000);

        drawConnection.invoke("JoinParticipant").catch((err) => {
            console.error(err.toString());
        });
    }).catch((err) => {
        console.error(err.toString());
    });
};

drawConnection.on("Winner", () => {
    document.getElementById().classList.remove("d-none");
});

drawConnection.on("Loser", () => {
    document.getElementById("loserPanel").classList.remove("d-none");
    document.getElementById("lobby").classList.add("d-none");
});

drawConnection.on("RestartGame", () => {
    document.getElementById("restartGame").classList.remove("d-none");
    document.getElementById("lobby").classList.add("d-none");

    if (drawConnection.state === signalR.HubConnectionState.Connected) {
        drawConnection.invoke("RestartGame").catch((err) => {
            console.error(err.toString());
        });
    }
});
