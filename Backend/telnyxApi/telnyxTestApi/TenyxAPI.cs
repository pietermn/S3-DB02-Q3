using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Telnyx;

namespace telnyxApi
{
    public class TelnyxAPI
    {
        private readonly MessagingSenderIdService service = new();

        public TelnyxAPI()
        {
        }

        public async Task<bool> SendMessage(Sms sms)
        {
            var sendMessageOptions = new NewMessagingSenderId
            {
                From = sms.from,
                To = sms.to,
                Text = sms.text
            };
            Console.WriteLine(JsonConvert.SerializeObject(sendMessageOptions));

            try
            {
                var messagingSender = await this.service.CreateAsync(sendMessageOptions);
                Console.WriteLine(JsonConvert.SerializeObject(messagingSender));
                return true;
            }
            catch (TelnyxException ex)
            {
                Console.WriteLine("exception");
                Console.WriteLine(JsonConvert.SerializeObject(ex));
                return false;
            }
        }


    }
}
