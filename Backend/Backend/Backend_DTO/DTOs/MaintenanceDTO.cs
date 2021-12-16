using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend_DTO.DTOs
{
    [Table("Maintenance")]
    public class MaintenanceDTO
    {
        [Key]
        public int Id { get; set; }
        public ComponentDTO Component { get; set; }
        public int ComponentId { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public DateTime TimeDone { get; set; } 
        public bool Done { get; set; }
    }
}
