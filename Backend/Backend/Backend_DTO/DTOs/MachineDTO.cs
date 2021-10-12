using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend_DTO.DTOs
{
    [Table("Machines")]
    public class MachineDTO
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }
    }
}
