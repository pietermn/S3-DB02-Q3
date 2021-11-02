using System;
using System.Collections.Generic;
using System.Linq;
using Authentication_DAL_Interface;
using Authentication_DTO;
using Microsoft.EntityFrameworkCore;

namespace Authentication_DAL
{
    public class SmsRecieverDAL : ISmsRecieverDAL
    {
        readonly DBContext _Context;

        public SmsRecieverDAL(DBContext context)
        {
            _Context = context;
        }

        public List<SmsReciever> GetRecievers()
        {
            return _Context.SmsReceivers
                    .AsNoTracking()
                .ToList();
        }

        public void RemoveReciever(int id)
        {
            List<SmsReciever> recievers = _Context.SmsReceivers
                .Where(r => r.Id == id)
                    .AsNoTracking().ToList();

            _Context.SmsReceivers.RemoveRange(recievers);
            _Context.SaveChanges();
        }

        public void SetReciever(SmsReciever reciever)
        {
            _Context.SmsReceivers.Add(reciever);
            _Context.SaveChanges();
        }
    }
}
