using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_DTO
{
    public class DateConverter : IsoDateTimeConverter
    {
        public DateConverter() =>
            this.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    }
}
