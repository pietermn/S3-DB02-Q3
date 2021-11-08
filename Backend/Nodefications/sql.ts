import mysql from "mysql";
import { Notification } from "./types";

const sqlConnection = mysql.createConnection({
  host: process.env.ConnectionServer ? process.env.ConnectionServer : "localhost",
  user: "root",
  password: "root",
  database: "db",
  port: process.env.ConnectionPort ? parseInt(process.env.ConnectionPort) : 3307,
});

export default class SQL {
  static getNotifications = async () => {
    const p = new Promise<Notification[]>((resolve, reject) => {
      sqlConnection.query(
        'SELECT Notifications.Id AS id, ComponentId as componentId, Components.Description AS "component", Message as description FROM `Notifications` INNER JOIN `Components` ON Notifications.ComponentId = Components.Id;',
        (err, res) => {
          if (err) reject(err);
          resolve(res);
        }
      );
    });

    return await p;
  };

  static addNotification = (componentId: number, message: string) => {
    sqlConnection.query(
      "INSERT INTO `Notifications` (`Id`, `ComponentId`, `Message`) VALUES (NULL, " +
        componentId +
        ', "' +
        message +
        '")'
    );
  };

  static removeNotification = (notificationId: number) => {
    sqlConnection.query("DELETE FROM `Notifications` WHERE Id = " + notificationId);
  };

  static updaterTimespan = async () => {
    const p = new Promise((resolve, reject) => {
      sqlConnection.query("SELECT MIN(Timestamp) AS Min, MAX(Timestamp) AS Max FROM `Productions`", (err, res) => {
        if (err) reject(err);
        resolve(res);
      });
    });

    return await p;
  };

  static addMaintenance = async (componentId: number, description: string) => {
    sqlConnection.query("INSERT INTO `Maintenance` (`ComponentId`, `Description`) VALUES (?, ?)", [
      componentId,
      description,
    ]);
  };
}
