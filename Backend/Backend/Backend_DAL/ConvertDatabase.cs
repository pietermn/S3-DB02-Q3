using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using DotNetEnv;
using Enums;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL
{
    public class Connection
    {
        public static MySqlConnection GetConnection()
        {
            Env.TraversePath().Load();
            string connectionString = !string.IsNullOrEmpty(Env.GetString("ConnectionString")) ? Env.GetString("ConnectionString") : "Server=localhost;Uid=root;Database=db;Pwd=root;Port=3307;Allow Zero Datetime=True;SslMode=None";
            MySqlConnection GeneralConnection = new(connectionString);
            return GeneralConnection;
        }
    }

    public class ConvertDatabase : IConvertDbDAL
    {
        readonly MySqlConnection _connection = Connection.GetConnection();
        readonly Q3Context _Context;

        public ConvertDatabase(Q3Context context)
        {
            _Context = context;
        }

        private List<ProductionSideDTO> GetProductionSides()
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "SELECT id AS 'Id', naam AS 'Name' FROM `treeview` WHERE naam LIKE '%Zijde'";

            _connection.Open();

            command.ExecuteNonQuery();
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(ds);
            _connection.Close();

            List<ProductionSideDTO> ProductionSides = new List<ProductionSideDTO>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string name = Convert.ToString(row["Name"]);
                ProductionSides.Add(new ProductionSideDTO() { Id = id, Name = name });
            }

            return ProductionSides;
        }

        private List<ProductionLineDTO> GetProductionLines(int productionSide_id)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "SELECT treeview.id, treeview.naam, treeview.omschrijving, treeview.actief, machine_monitoring_poorten.port, machine_monitoring_poorten.board FROM `machine_monitoring_poorten` INNER JOIN `treeview` ON treeview.naam LIKE machine_monitoring_poorten.name WHERE parent = @ProductionSide_Id;";
            command.Parameters.AddWithValue("@ProductionSide_Id", productionSide_id);

            _connection.Open();

            command.ExecuteNonQuery();
            DataSet ds = new();
            MySqlDataAdapter da = new(command);
            da.Fill(ds);
            _connection.Close();

            List<ProductionLineDTO> ProductionLines = new List<ProductionLineDTO>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string name = Convert.ToString(row["naam"]);
                string description = Convert.ToString(row["omschrijving"]);
                bool active = Convert.ToBoolean(row["actief"]);
                int port = Convert.ToInt32(row["port"]);
                int board = Convert.ToInt32(row["board"]);

                ProductionLines.Add(new ProductionLineDTO() { Id = id, Name = name, Description = description, Active = active, Port = port, Board = board });
            }

            return ProductionLines;
        }

        private List<ComponentDTO> GetComponents()
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "SELECT DISTINCT a.id AS 'Id',  c.omschrijving AS 'Name', b.omschrijving AS 'Type', a.omschrijving AS 'Description' FROM `treeview` a  INNER JOIN `production_data`  ON production_data.treeview_id = a.id INNER JOIN `machine_monitoring_poorten` ON machine_monitoring_poorten.board =  production_data.board AND machine_monitoring_poorten.port = production_data.port LEFT JOIN `treeview` b ON b.id=a.parent LEFT JOIN `treeview` c ON c.id=b.parent WHERE c.omschrijving = 'Matrijzen';";

            _connection.Open();

            command.ExecuteNonQuery();
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(ds);
            _connection.Close();

            List<ComponentDTO> Components = new List<ComponentDTO>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string name = Convert.ToString(row["Name"]);
                string oldType = Convert.ToString(row["Type"]);

                ComponentType type = ComponentType.Coldhalf;

                switch (oldType)
                {
                    case "Hothalf":
                        type = ComponentType.Hothalf;
                        break;
                    case "Coldhalf":
                        type = ComponentType.Coldhalf;
                        break;
                    case "Compleet matrijzen":
                        type = ComponentType.Complete;
                        break;
                }

                string description = Convert.ToString(row["Description"]);

                Components.Add(new ComponentDTO() { Id = id, Name = name, Type = type, Description = description });
            }

            return Components;
        }

        private List<ProductionLineHistoryDTO> GetHistory(int componentId)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "SELECT treeview.id AS 'ProductionLineId', start_date AS 'StartDate', end_date AS 'EndDate', start_time as 'StartTime', end_time as 'EndTime' FROM `production_data`  INNER JOIN `machine_monitoring_poorten` ON machine_monitoring_poorten.port = production_data.port AND machine_monitoring_poorten.board = production_data.board INNER JOIN `treeview` ON treeview.naam LIKE machine_monitoring_poorten.name WHERE treeview_id=@ComponentId;";
            command.Parameters.AddWithValue("@ComponentId", componentId);

            _connection.Open();

            command.ExecuteNonQuery();
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(ds);
            _connection.Close();

            List<ProductionLineHistoryDTO> History = new List<ProductionLineHistoryDTO>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["ProductionLineId"]);

                DateTime startDate = Convert.ToDateTime(row["StartDate"]);
                string startTimeStr = Convert.ToString(row["StartTime"]);
                string[] starttimes = startTimeStr.Split(":");

                startDate = new(startDate.Year, startDate.Month, startDate.Month, Convert.ToInt32(starttimes[0]), Convert.ToInt32(starttimes[1]), Convert.ToInt32(starttimes[2]));

                DateTime endDate = new DateTime(1, 1, 1);
                try
                {
                    endDate = Convert.ToDateTime(row["EndDate"]);
                    string endTimeStr = Convert.ToString(row["EndTime"]);
                    string[] endtimes = endTimeStr.Split(":");

                    endDate = new(endDate.Year, endDate.Month, endDate.Day, Convert.ToInt32(endtimes[0]), Convert.ToInt32(endtimes[1]), Convert.ToInt32(endtimes[2]));
                }
                catch (MySqlConversionException)
                {

                }

                History.Add(new ProductionLineHistoryDTO() { StartDate = startDate, EndDate = endDate, ProductionLineId = id });
            }

            return History;
        }

        public List<MachineDTO> GetMachines(int productionLineId)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "SELECT treeview.id AS 'Id', treeview.naam AS 'Name', treeview.omschrijving AS 'Description' FROM `treeview`  WHERE parent = @ProductionLineId;";
            command.Parameters.AddWithValue("@ProductionLineId", productionLineId);

            _connection.Open();

            command.ExecuteNonQuery();
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(ds);
            _connection.Close();

            List<MachineDTO> Machines = new List<MachineDTO>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string name = Convert.ToString(row["Name"]);
                string description = Convert.ToString(row["Description"]);

                Machines.Add(new MachineDTO() { Id = id, Name = name, Description = description });
            }

            return Machines;
        }

        public int GetProductionLineId(int componentId)
        {
            using var command = _connection.CreateCommand();

            command.CommandText = "SELECT p.treeview_id, t.id FROM production_data p INNER JOIN machine_monitoring_poorten m ON m.port=p.port AND m.board=p.board INNER JOIN treeview t ON t.naam=m.name WHERE (p.treeview_id=@ComponentId OR p.treeview2_id=@ComponentId) AND CAST(end_date AS CHAR(10)) = '0000-00-00';";
            command.Parameters.AddWithValue("@ComponentId", componentId);

            _connection.Open();

            command.ExecuteNonQuery();
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(ds);
            _connection.Close();

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                return Convert.ToInt32(row["id"]);
            }

            return 0;
        }

        private List<ProductionsDTO> GetProductionsFromProductionLine(int productionLineId, string table)
        {
            //using var command = _connection.CreateCommand();

            string cmdText = $"SELECT md.id AS 'Id', md.timestamp AS 'Timestamp', md.shot_time AS 'ShotTime' FROM {table} md INNER JOIN `machine_monitoring_poorten` mm ON mm.port=md.port AND mm.board=md.board INNER JOIN `treeview` t ON t.naam=mm.name WHERE t.id=@ProductionLineId;";
            using MySqlCommand command = new(cmdText, _connection);
            command.Parameters.AddWithValue("@ProductionLineId", productionLineId);

            _connection.Open();

            List<ProductionsDTO> Productions = new();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    DateTime timestamp = Convert.ToDateTime(reader["Timestamp"]);
                    double shottime = Convert.ToDouble(reader["ShotTime"]);

                    Productions.Add(new ProductionsDTO() { Id = id, Timestamp = timestamp, ShotTime = shottime, ProductionLineId = productionLineId }) ;
                }
            }

            //DataSet ds = new DataSet();
            //MySqlDataAdapter da = new MySqlDataAdapter(command);
            //da.Fill(ds);
            _connection.Close();


            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    int id = Convert.ToInt32(row["Id"]);
            //    DateTime timestamp = Convert.ToDateTime(row["Timestamp"]);
            //    double shottime = Convert.ToDouble(row["ShotTime"]);

            //    Productions.Add(new ProductionsDTO() { Timestamp = timestamp, ShotTime = shottime });
            //}
            return Productions;
        }

        public void ConvertAll()
        {
            List<ComponentDTO> Components = _Context.Components.Include(c => c.History).ToList();

            foreach(ComponentDTO component in Components)
            {
                component.History = GetHistory(component.Id);
            }

            _Context.SaveChanges();

            //List<ProductionSideDTO> ProductionSides = GetProductionSides();

            //foreach (ProductionSideDTO productionSideDTO in ProductionSides)
            //{
            //    productionSideDTO.ProductionLines = GetProductionLines(productionSideDTO.Id);

            //    foreach (ProductionLineDTO productionLineDTO in productionSideDTO.ProductionLines)
            //    {
            //        productionLineDTO.Machines = GetMachines(productionLineDTO.Id);
            //        productionLineDTO.Productions = GetProductionsFromProductionLine(productionLineDTO.Id);
            //    }
            //}

            //List<ComponentDTO> Components = GetComponents();

            //foreach (ComponentDTO componentDTO in Components)
            //{
            //    componentDTO.History = GetHistory(componentDTO.Id);
            //}

            //_Context.ProductionSides.AddRange(ProductionSides);
            //_Context.Components.AddRange(Components);
            //_Context.SaveChanges();

            //List<ProductionLineDTO> productionLineDTOs = _Context.ProductionLines.ToList();

            //foreach (ProductionLineDTO newProductionLineDTO in productionLineDTOs)
            //{
            //    List<ProductionLineHistoryDTO> productionLineHistoryDTOs = _Context.ProductionLinesHistory
            //        .Where(plh => plh.EndDate == Convert.ToDateTime("0001-01-01 00:00:00") && plh.ProductionLineId == newProductionLineDTO.Id)
            //        .ToList();

            //    List<ComponentDTO> componentDTOs = new List<ComponentDTO>();
            //    foreach (ProductionLineHistoryDTO productionLineHistoryDTO in productionLineHistoryDTOs)
            //    {
            //        componentDTOs.Add(productionLineHistoryDTO.Component);
            //    }

            //    newProductionLineDTO.Components = componentDTOs;
            //}

            //_Context.SaveChanges();

            //List<ComponentDTO> Components = _Context.Components
            //    .Include(c => c.History)
            //    .ToList();

            //foreach (ComponentDTO component in Components)
            //{
            //    int totalActions = 0;
            //    foreach (ProductionLineHistoryDTO history in component.History)
            //    {
            //        totalActions += _Context.Productions.Where(p => p.ProductionLineId == history.ProductionLineId && history.StartDate <= p.Timestamp && history.EndDate >= p.Timestamp).Count();
            //    }
            //    component.TotalActions = totalActions;
            //}

            //_Context.SaveChanges();
            //_Context.Database.SetCommandTimeout(100000);
            //Q3Context context = new(new DateTime(2020, 10, 1));

            //List<ProductionLineDTO> ProductionLines = context.ProductionLines.Include(pl => pl.Productions).ToList();

            //foreach (ProductionLineDTO productionLineDTO in ProductionLines)
            //{
            //    context.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202010"));
            //    context.SaveChanges();
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202012"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202101"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202102"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202103"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202104"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202105"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202106"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202107"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202108"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202109"));
            //    //productionLineDTO.Productions.AddRange(GetProductionsFromProductionLine(productionLineDTO.Id, "monitoring_data_202110"));
            //}

            //bool done = true;

            //using var command = _connection.CreateCommand();

            //DateTime startDate = new(2021, 10, 1);
            //for (int i = 0; i < 14; i++)
            //{
            //    string cmdTextTable = $"CREATE TABLE `Productions-{startDate:yyyy-MM}` (`Id` int PRIMARY KEY, `Timestamp` datetime NOT NULL, `ShotTime` double NOT NULL, `ProductionLineId` int DEFAULT NULL);";
            //    using MySqlCommand commandTables = new(cmdTextTable, _connection);

            //    _connection.Open();
            //    commandTables.ExecuteNonQuery();
            //    _connection.Close();

            //    startDate = startDate.AddMonths(1);
            //}

            //Q3Context context = new(new DateTime(2020, 8, 1));

            //List<ProductionsDTO> Productions = context.Productions.Where(p => p.Timestamp >= startDate && p.Timestamp < startDate.AddMonths(1)).ToList();

            //context = new Q3Context(startDate);

            //context.Productions.AddRange(Productions);
            //context.SaveChanges();

            //Q3Context context = new(new DateTime(2020, 9, 1));
            //List<ComponentDTO> Components = context.Components.Include(c => c.History).ToList();

            //foreach (ComponentDTO component in Components)
            //{
            //    int totalActions = 0;
            //    DateTime startDate = new(2020, 9, 1);

            //    foreach (ProductionLineHistoryDTO plH in component.History)
            //    {
            //        startDate = new(2020, 9, 1);
            //        context = new(startDate);

            //        for (int i = 0; i < 10; i++)
            //        {
            //            totalActions += context.Productions.Where(p => p.Timestamp >= plH.StartDate && p.Timestamp <= plH.EndDate && p.ProductionLineId == plH.ProductionLineId).AsNoTracking().Count();
            //            startDate = startDate.AddMonths(1);
            //            context = new(startDate);

            //        }
            //    }
            //    context = new();
            //    ComponentDTO newComponent = context.Components.Where(c => c.Id == component.Id).FirstOrDefault();
            //    newComponent.CurrentActions = totalActions;
            //    newComponent.TotalActions = totalActions;
            //    context.SaveChanges();
            //}
        }
    }
}