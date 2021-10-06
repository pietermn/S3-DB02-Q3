using System;
using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO;
using MySql.Data.MySqlClient;

namespace Backend_DAL
{
    public class ComponentDAL : IComponentDAL
    {

        readonly MySqlConnection connection;

        public ComponentDAL(MySqlConnection Connection)
        {
            connection = Connection;
        }

        public int getTotalActions(int port, int board, DateTime timestampMin, DateTime timestampMax)
        {
            List<MonitoringData202009DTO> monitoringDataList = new();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `monitoring_data_202009` WHERE (port=@port AND board=@board) AND timestamp BETWEEN @timestampMin AND @timestampMax;";
            command.Parameters.AddWithValue("@port", port);
            command.Parameters.AddWithValue("@board", board);
            command.Parameters.AddWithValue("@timestampMin", timestampMin);
            command.Parameters.AddWithValue("@timestampMax", timestampMax);

            connection.Open();

            try
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monitoringDataList.Add(new MonitoringData202009DTO(
                            reader.GetInt32("Id"),
                            reader.GetByte("Board"),
                            reader.GetByte("Port"),
                            reader.GetByte("Com"),
                            reader.GetInt32("Code"),
                            reader.GetInt32("Code2"),
                            reader.GetDateTime("Timestamp"),
                            reader.GetDateTime("Datum"),
                            reader.GetString("MacAddress"),
                            reader.GetDouble("ShotTime"),
                            reader.GetInt32("PreviousShotId")
                        ));
                    }
                }
                return monitoringDataList.Count;
            }
            finally
            {
                connection.Close();
            }
        }

        public int getTotalActionsWithTime(int port, int board, DateTime timestampMin, DateTime timestampMax)
        {
            List<MonitoringData202009DTO> monitoringDataList = new();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `monitoring_data_202009` WHERE (port=@port AND board=@board) AND timestamp BETWEEN @timestampMin AND @timestampMax;";
            command.Parameters.AddWithValue("@port", port);
            command.Parameters.AddWithValue("@board", board);
            command.Parameters.AddWithValue("@timestampMin", timestampMin);
            command.Parameters.AddWithValue("@timestampMax", timestampMax);

            connection.Open();

            try
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monitoringDataList.Add(new MonitoringData202009DTO(
                            reader.GetInt32("Id"),
                            reader.GetByte("Board"),
                            reader.GetByte("Port"),
                            reader.GetByte("Com"),
                            reader.GetInt32("Code"),
                            reader.GetInt32("Code2"),
                            reader.GetDateTime("Timestamp"),
                            reader.GetDateTime("Datum"),
                            reader.GetString("MacAddress"),
                            reader.GetDouble("ShotTime"),
                            reader.GetInt32("PreviousShotId")
                        ));
                    }
                }
                return monitoringDataList.Count;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
