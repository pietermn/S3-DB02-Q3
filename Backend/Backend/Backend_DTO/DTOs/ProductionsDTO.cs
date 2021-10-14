using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend_DTO.DTOs
{
    [Table("Productions")]
    public class ProductionsDTO
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double ShotTime { get; set; }
        public int ProductionLineId { get; set; }
    }
}
