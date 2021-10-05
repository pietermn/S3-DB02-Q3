using System;
using System.Collections.Generic;

#nullable disable

namespace Backend_DTO
{
    public partial class MonitoringData202009DTO
    {
        public int Id { get; set; }
        public byte Board { get; set; }
        public byte Port { get; set; }
        public byte Com { get; set; }
        public int Code { get; set; }
        public int Code2 { get; set; }
        public DateTime? Timestamp { get; set; }
        public DateTime? Datum { get; set; }
        public string MacAddress { get; set; }
        public double ShotTime { get; set; }
        public int PreviousShotId { get; set; }
    }
}
