using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess.Context
{
    public class DataContext : DbContext
    {

        private readonly string _connectionString;
        private IConfiguration _configuration;



        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleConnection");
        }

        public IDbConnection CreateConnection()
            => new OracleConnection(_connectionString);

    }
}
