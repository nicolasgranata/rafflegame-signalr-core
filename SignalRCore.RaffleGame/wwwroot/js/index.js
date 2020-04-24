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

drawConnection.on("winner", () => {


});

drawConnection.on("loser", () => {
    document.getElementById("loserPanel").classList.remove("d-none");
});
