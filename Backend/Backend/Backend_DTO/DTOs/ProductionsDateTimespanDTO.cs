using System;
namespace Backend_DTO.DTOs
{
    public class ProductionsDateTimespanDTO
    {
        public int ProductionLineId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}
