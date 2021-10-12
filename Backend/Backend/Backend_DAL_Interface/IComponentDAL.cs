using System;
namespace Backend_DAL_Interface
{
    public interface IComponentDAL
    {
        public int getTotalActions(int port, int board, DateTime timestampMin, DateTime timestampMax);

        public int getTotalActionsWithTime(int port, int board, DateTime timestampMin, DateTime timestampMax);
    }
}
