using Backend_DTO.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend_DTO.DTOs
{
    [Table("ProductionLines")]
    public class ProductionLineDTO
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(5)]
        public string Name { get; set; }
        [MaxLength(5)]
        public string Description { get; set; }
        public bool Active { get; set; }
        public int Port { get; set; }
        public int Board { get; set; }
        public List<ProductionsDTO> Productions {get; set;} 
        public List<MachineDTO> Machines { get; set; }
        public List<ComponentDTO> Components { get; set; }
    }
}
