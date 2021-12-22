using System;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;

namespace telnyxApi
{
    public class Sms
    {

        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }

        public Sms(string To, string Text)
        {
            from = "+15802031449";
            to = To;
            text = Text;
        }

    }
}
