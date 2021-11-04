using System;
using System.Collections.Generic;
using Authentication_DTO;

namespace Authentication_DAL_Interface
{
    public interface ISmsRecieverDAL
    {
        public List<SmsReciever> GetRecievers();

        public void SetReciever(SmsReciever reciever);

        public void RemoveReciever(int id);
    }
}
