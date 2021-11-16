using Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_DTO.DTOs
{
    [Table("Components")]
    public class ComponentDTO
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        [Required]
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ComponentType Type { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [ForeignKey("ComponentId")]
        public List<ProductionLineHistoryDTO> History { get; set; }        
        [ForeignKey("ComponentId")]
        public List<MaintenanceDTO> MaintenanceHistory { get; set; }
        public int TotalActions { get; set; }
        public int MaxActions { get; set; }
        public int CurrentActions { get; set; }
        [NotMapped]
        public int PercentageMaintenance => CalculatePercentage();

        private int CalculatePercentage()
        {
            if(MaxActions != 0)
            {
                return Convert.ToInt32((decimal)CurrentActions / (decimal)MaxActions * 100);
            }
            return 0;
        }
    }

}