using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend_DTO.DTOs
{
    [Table("ProductionLinesHistory")]
    public class ProductionLineHistoryDTO
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductionLine")]
        public int ProductionLineId { get; set; }
        public ProductionLineDTO ProductionLine { get; set; }
        public ComponentDTO Component { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime StartDate { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime EndDate { get; set; }
    }
}
