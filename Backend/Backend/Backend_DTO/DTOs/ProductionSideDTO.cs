using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend_DTO.DTOs
{
    [Table("ProductionSides")]
    public class ProductionSideDTO
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Name { get; set; }
        [ForeignKey("ProductionSideId")]
        public List<ProductionLineDTO> ProductionLines { get; set; }
    }
}
