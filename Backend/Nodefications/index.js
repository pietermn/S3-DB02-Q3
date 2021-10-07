const express = require('express');
const http = require('http');
const { Server } = require('socket.io')

const port = 5300
const app = express();
const server = http.createServer(app);
const io = new Server(server);

let notifications = []

app.get('/', (_req, res) => {
    res.sendFile(__dirname + '/index.html')
});

io.on('connection', (socket) => {
    console.log('a user connected');
    socket.on('disconnect', () => {
        console.log('a user disconnected')
    })

    socket.emit('Add Notification List', notifications)

    socket.on('Add Notification', (notification) => {
        notifications.push(notification)
        io.emit('Add Notification List', notifications)
    })

    socket.on('Remove Notification', (notification) => {
        let nid = notifications.findIndex(n => n.id === notification.id)
        if (nid !== -1) {
            notifications.splice(nid)
            io.emit('Add Notification List', notifications)

        }
    })
})

server.listen(port, () => {
    console.log(`listening on :${port}`)
});