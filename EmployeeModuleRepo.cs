using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper.Oracle;
using Dapper;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace DataAccess.Repository
{
    public class EmployeeModuleRepo : ControllerBase
    {
        private readonly DataContext _context;
        private readonly EmployeeModel _employeemodel;

        public EmployeeModuleRepo(DataContext context, EmployeeModel employeemodel)
        {
            _context = context;
            _employeemodel = employeemodel;
        }

        public string GetNameRepo(string name)
        {
            string res = "my name is" + name;
            return res;
        }



        //[HttpGet("GetRepoQuery1",Name = "GetRepoQuery1")]
        //public async Task<IActionResult> GetRepoQuery1()
        //{
        //try
        //{
        // using var connection = _context.CreateConnection();
        //string query = "SELECT * FROM EMP_433891";
        //var res = await connection.QueryAsync<EmployeeModel>(query);
        //return Ok(res);
        //}
        //catch (Exception ex) {
        //return StatusCode(500,"Internal server error: " + ex.Message);
        //}

        // }


    
        // execution of procedure without flag
        public async Task<IActionResult> GetDetailsRepo1()
        {
            OracleRefCursor result = null;
            var procedureName = "proc_viewempWithOut_May31";
            var parameters = new OracleDynamicParameters();

            parameters.Add("emp_refcur", result, OracleMappingType.RefCursor, ParameterDirection.Output);
            parameters.BindByName = true;

            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<EmployeeModel>(
                procedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (response == null || !response.Any())
            {
                return NotFound(); // Return 404 if no data is found
            }

            return Ok(response); // Return 200 OK with the student details
        }


        public async Task<IEnumerable<EmployeeModel>>GetDetailsRepo2(string flag,string para1,string para2)
        {

            OracleRefCursor result = null;
            var procedurename = "proc_viewempWithflag_May31";
            var parameters = new OracleDynamicParameters();

            parameters.Add("flag",flag,OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("paravalue1", para1, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("paravalue2", para2, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("emp_refcur", result, OracleMappingType.RefCursor, ParameterDirection.Output);

            parameters.BindByName = true;

            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<EmployeeModel>(
                procedurename,
                parameters,
                commandType: CommandType.StoredProcedure);
            return response;    

        }






    }
}
