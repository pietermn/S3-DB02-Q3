using System;
using System.Collections.Generic;
using Authentication_DTO;

namespace Authentication_Logic_Interface
{
    public interface ISmsRecieverContainer
    {
        public List<SmsReciever> GetRecievers();

        public void SetReciever(SmsReciever reciever);

        public void RemoveReciever(int id);
    }
}
