using Enums;
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
        public ComponentType Type { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [ForeignKey("ProductionLines")]
        public List<ProductionLineHistoryDTO> History { get; set; }
    }
}
