using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Backend_DAL
{
    public class ComponentDAL : IComponentDAL
    {
        public Q3Context _Context = new();

        public ComponentDTO GetComponent(int component_id)
        {
            return _Context.Components
                .Where(c => c.Id == component_id)
                .Include(c => c.History)
                    .ThenInclude(h => h.ProductionLine)
                    .AsNoTracking()
                .FirstOrDefault();
        }

        public List<ComponentDTO> GetComponents()
        {
            return _Context.Components
                .Include(c => c.History)
                    .ThenInclude(h => h.ProductionLine)
                    .AsNoTracking()
                .ToList();
        }

        public static int GetTotalRecordsAmount(string tableName, DateTime begin, DateTime end)
        {
            MySqlConnection _connection = Connection.GetConnection();
            string cmdText = $"SELECT Count(*) FROM `{tableName}` WHERE Timestamp >= @Begin AND Timestamp <= @End;";
            using MySqlCommand command = new(cmdText, _connection);
            command.Parameters.AddWithValue("@Begin", begin);
            command.Parameters.AddWithValue("@End", end);

            _connection.Open();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            int amount = int.Parse(command.ExecuteScalar().ToString());
            watch.Stop();

            _connection.Close();
            return amount;
        }

        private static List<ProductionsDTO> GetProductions(List<string> tableNames, DateTime begin, DateTime end)
        {
            List<ProductionsDTO> Productions = new();

            foreach (string tablename in tableNames)
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                int totalRecords = GetTotalRecordsAmount(tablename, begin, end);
                watch.Stop();



                for (int i = 0; i < totalRecords; i += 100_000)
                {
                    MySqlConnection _connection = Connection.GetConnection();
                    string cmdText = $"SELECT * FROM `{tablename}` WHERE Timestamp >= @Begin AND Timestamp <= @End LIMIT 100000 OFFSET @Offset;";
                    using MySqlCommand command = new(cmdText, _connection);
                    command.Parameters.AddWithValue("@Begin", begin);
                    command.Parameters.AddWithValue("@End", end);
                    command.Parameters.AddWithValue("@Offset", i);

                    _connection.Open();
                    //int amount = int.Parse(command.ExecuteScalar().ToString());
                    DataSet ds = new();
                    using MySqlDataAdapter adapter = new(command);
                    adapter.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int id = Convert.ToInt32(row["Id"]);
                        DateTime timestamp = Convert.ToDateTime(row["Timestamp"]);
                        double shottime = Convert.ToDouble(row["ShotTime"]);
                        int productionLineId = Convert.ToInt32(row["ProductionLineId"]);

                        Productions.Add(new ProductionsDTO() { Id = id, Timestamp = timestamp, ShotTime = shottime, ProductionLineId = productionLineId });
                    }

                    _connection.Close();
                }
            };

            return Productions;
        }

        public int GetPreviousActionsPerDate(List<ProductionsDateTimespanDTO> timespans)
        {
            int amount = 0;
            DateTime tableDate = timespans.First().Begin;
            int monthDifference = ((timespans.Last().End.Year - timespans.First().Begin.Year) * 12) + timespans.Last().End.Month - timespans.Last().Begin.Month;

            for (int i = 0; i < monthDifference + 1; i++)
            {
                if (tableDate > new DateTime(2020, 9, 1) && tableDate < new DateTime(2021, 10, 1))
                {
                    MySqlConnection _connection = Connection.GetConnection();
                    string cmdText = $"SELECT COUNT(*) FROM `Productions-{tableDate:yyyy-MM}` WHERE ";

                    int counter = 0;
                    foreach (ProductionsDateTimespanDTO timespan in timespans)
                    {
                        cmdText += $"(Timestamp >= '{timespan.Begin:yyyy-MM-dd HH:mm:ss}' AND Timestamp <= '{timespan.End:yyyy-MM-dd HH:mm:ss}' AND ProductionLineId = {timespan.ProductionLineId})";
                        if (counter != timespans.Count - 1)
                        {
                            cmdText += " OR ";
                        }
                        counter++;
                    }

                    using MySqlCommand command = new(cmdText, _connection);

                    _connection.Open();
                    amount += int.Parse(command.ExecuteScalar().ToString());
                    _connection.Close();

                    tableDate = tableDate.AddMonths(1);
                }
            }

            return amount;
        }

        public List<ProductionsDTO> GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            ComponentDTO component = _Context.Components.Where(c => c.Id == component_id)
                .Include(c => c.History)
                .FirstOrDefault();

            List<ProductionsDTO> allProductions = new();
            List<ProductionsDTO> productions = new();

            int monthDifference = ((beginDate.Year - endDate.Year) * 12) + (endDate.Month - beginDate.Month);
            if (endDate.Year > beginDate.Year) monthDifference *= -1;

            if (endDate < beginDate) return new List<ProductionsDTO>();

            List<string> tableNames = new();
            for (int i = 0; i <= monthDifference; i++)
            {
                DateTime currentDatetime = beginDate.AddMonths(i);
                if (currentDatetime >= new DateTime(2020, 9, 1) && currentDatetime < new DateTime(2021, 10, 1))
                {
                    tableNames.Add("Productions-" + currentDatetime.ToString("yyyy-MM"));

                }
            }
            allProductions = GetProductions(tableNames, beginDate, endDate);

            foreach (ProductionsDTO productionsDTO in allProductions)
            {
                foreach (ProductionLineHistoryDTO historyDTO in component.History)
                {
                    if (productionsDTO.ProductionLineId == historyDTO.ProductionLineId
                        && productionsDTO.Timestamp > historyDTO.StartDate
                        && productionsDTO.Timestamp < historyDTO.EndDate)
                    {
                        productions.Add(productionsDTO);
                    }
                }
            }

            return productions.OrderBy(p => p.Timestamp).ToList();
        }

        public void SetMaxAction(int component_id, int max_actions)
        {
            _Context.Components.Where(c => c.Id == component_id).FirstOrDefault().MaxActions = max_actions;
            _Context.SaveChanges();
        }
    }
}
