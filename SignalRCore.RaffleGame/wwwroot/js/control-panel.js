const controlPanelConnection = new signalR.HubConnectionBuilder()
    .withUrl("/controlPanelHub")
    .build();

controlPanelConnection.start().then(() => {
    init()
}).catch((err) => {
    console.error(err.toString());
});


controlPanelConnection.on("UpdateConnectedPlayers", (value) => {
    document.getElementById("players").innerHTML = value;
});

const init = () => {
    document.getElementById("startGame").addEventListener("click", () => {
        controlPanelConnection.invoke("GetWinner").catch((err) => {
            console.error(err.toString());
        });
    });

    document.getElementById("restartGame").addEventListener("click", () => {
        controlPanelConnection.invoke("RestartGame").catch((err) => {
            console.error(err.toString());
        });
    });
}