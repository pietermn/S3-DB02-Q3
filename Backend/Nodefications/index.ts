import express from "express";
import http from "http";
import { Server } from "socket.io";
import sql from "./sql";

const port = 5300;
const app = express();
const server = http.createServer(app);
const io = new Server(server, { cors: { origin: "*" } });

let timer: NodeJS.Timer;
let timerActive = false;
let jump = 0;
let startDate: Date;

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
	socket.emit("Add Notification List", await sql.getNotifications());
	socket.emit("Updater", await sql.updaterTimespan());

	console.log("a user connected");
	socket.on("disconnect", () => {
		console.log("a user disconnected");
	});

	socket.on("Add Notification", async (notification) => {
		sql.addNotification(notification.componentId, notification.message);
		io.emit("Add Notification List", await sql.getNotifications());
	});

	socket.on("Remove Notification", async (notification) => {
		sql.removeNotification(notification.id);
		io.emit("Add Notification List", await sql.getNotifications());
	});

	socket.on("Set Timer", (data: { interval: number; startDate: Date }) => {
		if (
			!timerActive &&
			(data.startDate !== null || data.startDate !== undefined)
		) {
			startDate = new Date(data.startDate);
			timer = setInterval(() => {
				jump = Number(jump) + Number(data.interval);
				let newDate = new Date(new Date(startDate).getTime() + jump * 1000);
				io.emit("New Current Date", newDate);
			}, 5000);
			timerActive = true;
			io.emit("Timer Started");
		}
	});

	socket.on("Stop Timer", () => {
		clearInterval(timer);
		timerActive = false;
		jump = Number(0);
		io.emit("Timer Stopped");
	});
});

server.listen(port, () => {
	console.log(`listening on :${port}`);
});
