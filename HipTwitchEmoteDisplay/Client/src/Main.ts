import * as signalR from "@microsoft/signalr";
import LinkMessage from "./LinkMessage";

const animationDuration = 750;

let currentPageWbg = 1;

function getInnerWbh() {
    return window.innerWidth / window.innerHeight;
}

function processAppearance(yays: HTMLDivElement[]) {
    for (const yay of yays) {
        yay.style.transitionDuration = `${animationDuration}ms`;
        yay.style.opacity = "1";
    }
}

function processDisappearance(yays: HTMLDivElement[]) {

    for (const yay of yays) {
        yay.style.opacity = "0";
    }

    setTimeout(() => {
        for (const yay of yays) {
            yay.remove();
        }
        // не знаю, просто так.
        yays.length = 0;
    }, animationDuration);
}

window.onload = () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub")
        .build();

    currentPageWbg = getInnerWbh();
    window.onresize = () => {
        currentPageWbg = getInnerWbh();
        // тут нужно реконфигурацию наверное сделать но мне как то супер мега впадву.
    };

    const lastYays: HTMLDivElement[] = [];

    connection.on("Set", (msg: LinkMessage) => {

        const oldYayas = lastYays.splice(0);

        processDisappearance(oldYayas);

        let maxWbg = 1;
        for (const info of msg.infos) {
            if (info.wbh > maxWbg) {
                maxWbg = info.wbh;
            }
        }

        // Если наш экран имеет соотношение сторон 1:1, а смайл 3:1, то его актуальная высота будет всего 33%
        // Значит все остальные эмоуты, чьё соотношение сторон 1:1, должны будут иметь высоту в 33%, 
        // чтобы иметь одинаковую высоту 
        let maxWidthEmoteActualHeight = currentPageWbg / maxWbg;
        if (maxWidthEmoteActualHeight > 1) {
            maxWidthEmoteActualHeight = 1;
        }
        
        const newYayas: HTMLDivElement[] = [];

        for (const info of msg.infos) {

            const elem = document.createElement("div");

            // Актуальные высоты эмоутов могут быть разными, нужно компенсировать это,
            // чтобы отображаемые эмоуты имели одинаковую высоту.
            let actualEmoteHeight = currentPageWbg / info.wbh;

            // гиганты нам не нужны
            if (actualEmoteHeight > 1) {
                actualEmoteHeight = 1;
            }

            const targetHeight = 1 - (actualEmoteHeight - maxWidthEmoteActualHeight);

            console.log(targetHeight);

            elem.classList.add("yay");
            elem.style.opacity = "0";
            // elem.style.transitionDuration = `${animationDuration}ms`;
            elem.style.backgroundImage = `url(${info.uri})`;
            elem.style.height = `${targetHeight * 100}%`;
            elem.style.top = `${(0.5 - targetHeight / 2) * 100}%`;

            document.body.appendChild(elem);

            newYayas.push(elem);
            lastYays.push(elem);
        }
        
        // если сразу делать опасити 0 и транзишн дурейшн, он стандартный опасити 1 начинает крутить в 0, 
        // а затем делает 1 в методе ниже

        setTimeout(() => processAppearance(newYayas), 1);
        // processAppearance(newYayas);
    });

    connection.start().catch((err) => document.write(err));
};