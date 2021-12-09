using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_DTO.DTOs
{
    public class ProductionsDateDTO
    {
        public string TimespanIndicator { get; set; }
        public string CurrentTimespan { get; set; }
        public int Productions { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public bool IsPredicted { get; set; }
    }
}
