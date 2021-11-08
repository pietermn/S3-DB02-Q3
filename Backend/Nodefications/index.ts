import express from "express";
import http from "http";
import { Server } from "socket.io";
import sql from "./sql";
import axios from "axios";
import ActionsChecker from "./actionsChecker";
import { Notification as NotificationType } from "./types";

const port = 5300;
const app = express();
const server = http.createServer(app);
const io = new Server(server, { cors: { origin: "*" } });

let timer: NodeJS.Timer;
let timerActive = false;
let timerComponent: NodeJS.Timer;
let timerActiveComponent = false;

process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

app.get("/", (_req, res) => {
    res.send(
        "<div><h1>Q3 Socket service</h1><ul><li><a href='/notifications'>Notification service<a/></li><li><a href='/updater'>Updater service</a></li></ul><div>"
    );
});

app.get("/notifications", (_req, res) => {
    res.sendFile(__dirname + "/notifications.html");
});

app.get("/updater", (_req, res) => {
    res.sendFile(__dirname + "/updater.html");
});

io.on("connection", async (socket) => {
    await ActionsChecker.allComponentsNeedNotification();
    socket.emit("Add Maintenance List", await GetMaintenanceNotifcations());
    socket.emit("Add Notification List", await sql.getNotifications());

    console.log("a user connected");
    socket.on("disconnect", () => {
        console.log("a user disconnected");
    });

    socket.on("Set Max Actions", async (data: { componentId: number; maxActions: number }) => {
        if (data.maxActions <= 2147483647) {
            await axios.put(
                "http://localhost:5000/component/maxactions?component_id=" +
                    data.componentId +
                    "&max_actions=" +
                    data.maxActions
            );
            if (await ActionsChecker.componentNeedsNotification(data.componentId)) {
                if (await sql.componentHasNoNotification(data.componentId)) {
                    sql.addNotification(data.componentId, "");
                }
            } else {
                sql.removeNotificationFromComponent(data.componentId);
            }
        }

        io.emit("Add Notification List", await ActionsChecker.allComponentsNeedNotification());
    });

    socket.on("Add Maintenance", async (data: { componentId: number; description: string }) => {
        await axios.post("http://localhost:5000/maintenance", data);
        io.emit("Add Maintenance List", await GetMaintenanceNotifcations());
    });

    socket.on("Finish Maintenance", async (data: { maintenanceId: number }) => {
        await axios.put(`http://localhost:5000/maintenance?maintenanceId=${data.maintenanceId}`);
        sql.resetComponentUses(data.maintenanceId);
        io.emit("Update Components");
        io.emit("Add Maintenance List", await GetMaintenanceNotifcations());
    });

    socket.on("Add Notification", async (notification) => {
        sql.addNotification(notification.componentId, notification.message);
        await ActionsChecker.allComponentsNeedNotification();
        io.emit("Add Notification List", await sql.getNotifications());
    });

    socket.on("Remove Notification", async (notification) => {
        sql.removeNotification(notification.id);
        await ActionsChecker.allComponentsNeedNotification();
        io.emit("Add Notification List", await sql.getNotifications());
    });

    socket.on("Start Timer", () => {
        if (!timerActive) {
            timer = setInterval(async () => {
                await ActionsChecker.allComponentsNeedNotification();
                io.emit("Add Maintenance List", await GetMaintenanceNotifcations());
                io.emit("Add Notification List", await sql.getNotifications());
            }, 5000);
            timerActive = true;
            io.emit("Timer Started");
        }
    });

    socket.on("Stop Timer", () => {
        clearInterval(timer);
        timerActive = false;
        io.emit("Timer Stopped");
    });

    socket.on("Start Timer Component", () => {
        if (!timerActiveComponent) {
            timerComponent = setInterval(async () => {
                io.emit("Update Components");
                sql.addUsesToComponent(173, Math.floor(Math.random() * 100));
            }, 30000);
            timerActiveComponent = true;
            io.emit("Timer Started Component");
        }
    });

    socket.on("Stop Timer Component", () => {
        clearInterval(timerComponent);
        timerActiveComponent = false;
        io.emit("Timer Stopped Component");
    });
});

server.listen(port, () => {
    console.log(`listening on :${port}`);
});

async function GetMaintenanceNotifcations() {
    const data = (
        await axios.get<
            {
                id: number;
                component: { description: string };
                componentId: number;
                description: string;
            }[]
        >("http://localhost:5000/maintenance/readall?done=false")
    ).data;
    let maintenanceNotifications: NotificationType[] = [];

    data.forEach((n) => {
        maintenanceNotifications.push({
            id: n.id,
            component: n.component.description,
            componentId: n.componentId,
            description: n.description,
        });
    });

    return maintenanceNotifications;
}
