using System;

namespace Backend_DTO
{
    public class MonitoringData202009DTO
    {
        public int Id { get; set; }
        public byte Board { get; set; }
        public byte Port { get; set; }
        public byte Com { get; set; }
        public int Code { get; set; }
        public int Code2 { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime Datum { get; set; }
        public string MacAddress { get; set; }
        public double ShotTime { get; set; }
        public int PreviousShotId { get; set; }

        public MonitoringData202009DTO() { }

        public MonitoringData202009DTO(int id, byte board, byte port, byte com, int code, int code2, DateTime timestamp, DateTime datum, string macAddress, double shotTime, int previousShotId)
        {
            Id = id;
            Board = board;
            Port = port;
            Com = com;
            Code = code;
            Code2 = code2;
            Timestamp = timestamp;
            Datum = datum;
            MacAddress = macAddress;
            ShotTime = shotTime;
            PreviousShotId = previousShotId;
        }
    }
}
