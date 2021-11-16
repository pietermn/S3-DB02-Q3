using System;
using System.Collections.Generic;
using Authentication_DAL_Interface;
using Authentication_DTO;
using Authentication_Logic_Interface;

namespace Authentication_Logic.Containers
{
    public class SmsRecieverContainer : ISmsRecieverContainer
    {
        readonly ISmsRecieverDAL _smsRecieverDAL;
        public SmsRecieverContainer(ISmsRecieverDAL smsRecieverDAL)
        {
            _smsRecieverDAL = smsRecieverDAL;
        }

        public List<SmsReciever> GetRecievers()
        {
            return _smsRecieverDAL.GetRecievers();
        }

        public void RemoveReciever(int id)
        {
            _smsRecieverDAL.RemoveReciever(id);
        }

        public void SetReciever(SmsReciever reciever)
        {
            _smsRecieverDAL.SetReciever(reciever);
        }
    }
}
