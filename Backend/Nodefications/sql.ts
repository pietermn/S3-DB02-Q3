import mysql from 'mysql';
import { Notification } from './types';

const sqlConnection = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'root',
    database: 'db',
    port: 3307
});

export default class SQL {
    static getNotifications = async () => {
        let notifications: Notification[] = [];
        const p = new Promise<Notification[]>((resolve, reject) => {
            sqlConnection.query('SELECT * FROM `Notifications`', (err, res) => {
                if (err) reject(err);
                resolve(res);
            });
        });

        await p.then((result: Notification[]) => {
            notifications = result;
        })

        return notifications;
    }

    static addNotification = (componentId: number, message: string) => {
        sqlConnection.query('INSERT INTO `Notifications` (`Id`, `ComponentId`, `Message`) VALUES (NULL, ' + componentId + ', "' + message + '")');
    }

    static removeNotification = (notificationId: number) => {
        sqlConnection.query('DELETE FROM `Notifications` WHERE Id = ' + notificationId);
    }
}