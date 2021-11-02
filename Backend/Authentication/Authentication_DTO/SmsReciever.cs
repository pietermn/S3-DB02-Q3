using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_DTO
{
    public class SmsReciever
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(12)]
        public string Telephone { get; set; }

        [Required]
        [MaxLength(155)]
        public string Name { get; set; }
    }
}