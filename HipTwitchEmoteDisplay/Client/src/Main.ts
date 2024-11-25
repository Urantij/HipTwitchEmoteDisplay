import * as signalR from "@microsoft/signalr";
import LinkMessage from "./LinkMessage";

window.onload = () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub")
        .build();

    const yayImage = document.getElementById("yay") as HTMLDivElement;

    if (yayImage === undefined) {
        document.write("чето хреня какая то браток");
        throw "стоппу";
    }

    connection.on("Set", (msg: LinkMessage) => {
        yayImage.style.backgroundImage = `url(${msg.uri})`;
    });

    connection.start().catch((err) => document.write(err));
};