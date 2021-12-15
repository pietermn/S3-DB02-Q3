import mysql from "mysql";
import { Component, Notification } from "./types";

const sqlConnection = mysql.createConnection({
    host: process.env.ConnectionServer ? process.env.ConnectionServer : "localhost",
    user: "root",
    password: "root",
    database: "db",
    port: process.env.ConnectionPort ? parseInt(process.env.ConnectionPort) : 3307,
});

export default class SQL {
    static getAllComponents = async () => {
        const p = new Promise<Component[]>((resolve, reject) => {
            sqlConnection.query(
                "SELECT Id AS id, MaxActions AS maxActions, CurrentActions AS currentActions, Description AS description FROM Components;",
                (err, res) => {
                    if (err) reject(err);
                    resolve(res);
                }
            );
        });

        return await p;
    };

    static componentHasNoNotification = async (componentId: number) => {
        const p = new Promise<boolean>((resolve, reject) => {
            sqlConnection.query(
                "SELECT Count(*) AS notificationsCount FROM Notifications WHERE ComponentId = ?;",
                [componentId],
                (err, res) => {
                    if (err) reject(err);
                    resolve(res[0].notificationsCount === 0);
                }
            );
        });

        return await p;
    };

    static getNotifications = async () => {
        const p = new Promise<Notification[]>((resolve, reject) => {
            sqlConnection.query(
                'SELECT Notifications.Id AS id, ComponentId as componentId, Components.Description AS "component", Message as description FROM `Notifications` INNER JOIN `Components` ON Notifications.ComponentId = Components.Id;',
                (err, res) => {
                    if (err) {
                        reject(err);
                        console.log(err);
                    }
                    resolve(res);
                }
            );
        });

        return await p;
    };

    static addNotification = (componentId: number, message: string) => {
        sqlConnection.query(
            "INSERT INTO `Notifications` (`ComponentId`, `Message`) VALUES (" + componentId + ', "' + message + '")'
        );
    };

    static removeNotification = (notificationId: number) => {
        sqlConnection.query("DELETE FROM `Notifications` WHERE Id = " + notificationId);
    };

    static removeNotificationFromComponent = (componentId: number) => {
        sqlConnection.query("DELETE FROM `Notifications` WHERE ComponentId = " + componentId);
    };

    static updaterTimespan = async () => {
        const p = new Promise((resolve, reject) => {
            sqlConnection.query(
                "SELECT MIN(Timestamp) AS Min, MAX(Timestamp) AS Max FROM `Productions`",
                (err, res) => {
                    if (err) reject(err);
                    resolve(res);
                }
            );
        });

        return await p;
    };

    static addMaintenance = async (componentId: number, description: string) => {
        sqlConnection.query("INSERT INTO `Maintenance` (`ComponentId`, `Description`) VALUES (?, ?)", [
            componentId,
            description,
        ]);
    };

    static addUsesToComponent = (componentId: number, amount: number) => {
        sqlConnection.query(
            "UPDATE `Components` SET `TotalActions` = `TotalActions` + ?, `CurrentActions` = `CurrentActions` + ? WHERE `Id` = ?",
            [amount, amount, componentId]
        );
    };

    static resetComponentUses = (maintenanceId: number) => {
        sqlConnection.query(
            "UPDATE `Components` INNER JOIN Maintenance ON Components.Id = Maintenance.ComponentId SET `CurrentActions` = 0 WHERE Maintenance.Id = ?",
            [maintenanceId]
        );
    };

    static addMMData = () => {
        console.log("ADDMMDATASQL");

        sqlConnection.query(
            "INSERT INTO `Productions` (`Id`, `Timestamp`, `ShotTime`, `ProductionLineId`) VALUES (NULL, ?, ?, '364')",
            [new Date(2020, 8, 30, new Date().getHours(), new Date().getMinutes() - 30, new Date().getSeconds()), 3000]
        );
    };
}
