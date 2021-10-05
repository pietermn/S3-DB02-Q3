using System;
using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO;
using MySql.Data.MySqlClient;

namespace Backend_DAL
{
    public class ProductonSideDAL : IProductonSideDAL
    {

        readonly MySqlConnection connection;

        public ProductonSideDAL()
        {
        }
    }
}
