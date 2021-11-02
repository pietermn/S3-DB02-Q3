using System;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;

namespace telnyxApi
{
    public class Sms
    {

        public string from = "+18022219734";
        public string to { get; set; }
        public string text { get; set; }

        public Sms(string To, string Text)
        {
            to = To;
            text = Text;
        }

    }
}
