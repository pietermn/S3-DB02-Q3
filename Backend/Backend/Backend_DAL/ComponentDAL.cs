using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        private static List<ProductionsDTO> GetProductions(List<string> tableNames, DateTime begin, DateTime end)
        {
            List<ProductionsDTO> Productions = new();
            Parallel.ForEach(tableNames, tablename =>
            {
                MySqlConnection _connection = Connection.GetConnection();
                string cmdText = $"SELECT * FROM `{tablename}` WHERE Timestamp BETWEEN '{begin:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}';";
                using MySqlCommand command = new(cmdText, _connection);

                _connection.Open();


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        DateTime timestamp = Convert.ToDateTime(reader["Timestamp"]);
                        double shottime = Convert.ToDouble(reader["ShotTime"]);
                        int productionLineId = Convert.ToInt32(reader["ProductionLineId"]);

                        Productions.Add(new ProductionsDTO() { Id = id, Timestamp = timestamp, ShotTime = shottime, ProductionLineId = productionLineId });
                    }
                }

                _connection.Close();
            });

            return Productions;
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
