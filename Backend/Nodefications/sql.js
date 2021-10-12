const mysql = require('mysql');

const sqlConnection = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'root',
    database: 'db',
    port: 3307
});

class SQL {
    static getNotifications = async () => {
        let notifications = [];
        const p = new Promise((resolve, reject) => {
            sqlConnection.query('SELECT * FROM `Notifications`', (err, res) => {
                if (err) reject(err);
                resolve(res);
            });
        });

        await p.then((result) => {
            notifications = result;
        })

        return notifications;
    }

    static addNotification = (componentId, message) => {
        sqlConnection.query('INSERT INTO `Notifications` (`Id`, `ComponentId`, `Message`) VALUES (NULL, ' + componentId + ', "' + message + '")');
    }

    static removeNotification = (notificationId) => {
        sqlConnection.query('DELETE FROM `Notifications` WHERE Id = ' + notificationId);
    }
}

module.exports = SQL;