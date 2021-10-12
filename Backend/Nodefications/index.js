const express = require('express');
const http = require('http');
const { Server } = require('socket.io');
const mysql = require('mysql');

const sqlConnection = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'root',
    database: 'db',
    port: 3307
});

const port = 5300;
const app = express();
const server = http.createServer(app);
const io = new Server(server);

const sql = require('./sql');

app.get('/', (_req, res) => {
    res.sendFile(__dirname + '/index.html')
});

io.on('connection', async (socket) => {
    socket.emit('Add Notification List', await sql.getNotifications())

    console.log('a user connected');
    socket.on('disconnect', () => {
        console.log('a user disconnected')
    })

    socket.on('Add Notification', async (notification) => {
        sql.addNotification(notification.componentId, notification.message)
        io.emit('Add Notification List', await sql.getNotifications())
    })

    socket.on('Remove Notification', async (notification) => {
        sql.removeNotification(notification.id)
        io.emit('Add Notification List', await sql.getNotifications())
    })
});

server.listen(port, () => {
    console.log(`listening on :${port}`)
});
