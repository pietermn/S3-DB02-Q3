using System;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using DotNetEnv;

namespace telnyxApi
{
    public class Sms
    {

        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }

        public Sms(string To, string Text)
        {
            from = Env.GetString("Telynx_Phonenumber");
            to = To;
            text = Text;
        }

    }
}
