using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("dbConnection");
        }
        public async Task<ErrorMessageInfo> createNewEmployee(Employee employee)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            if (string.IsNullOrEmpty(employee.EmployeeName)
                || string.IsNullOrEmpty(employee.PhoneNumber)
                || string.IsNullOrEmpty(employee.IdentityNumber)
                || employee.Birthday == null
                )
            {
                errorMessageInfo.message = "Chưa điền đủ thông tin";
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrEmpl002";
            }
            else
            {
                try
                {
                    using (var sqlConnection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sqlConnection.Open();
                            employee.DateJoin = DateTime.Now;
                            string query = "Insert into Employee(EmployeeName, PhoneNumber, IdentityNumber, Birthday, DateJoin) values (@EmployeeName," +
                                "@PhoneNumber, @IdentityNumber, @Birthday, @DateJoin) ";
                            errorMessageInfo.data = await sqlConnection.ExecuteAsync(query, employee);
                            errorMessageInfo.isSuccess = true;
                        }
                        catch (Exception e)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = e.Message;
                            errorMessageInfo.error_code = "ErrEmpl001";
                        }
                        sqlConnection.Close();
                    }
                }
                catch (Exception e)
                {
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrEmpl001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> deleteNewEmployee(long employeeID)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    try
                    {
                        sql.Open();
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        string querySelect = "SELECT * FROM Employee WHERE EmployeeID = @id";
                        dynamicParameters.Add("id", employeeID);
                        Employee employee = await sql.QuerySingleOrDefaultAsync<Employee>(querySelect, dynamicParameters);
                        if (employee == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có nhân viên này";
                            errorMessageInfo.error_code = "ErrEmpl003";
                        }
                        else
                        {
                            string queryDelete = "DELETE Employee Where EmployeeID = @id";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryDelete, dynamicParameters);
                            errorMessageInfo.isSuccess = true;
                        }                        
                    }
                    catch(Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrEmpl001";
                    }
                    sql.Close();
                }
            }
            catch(Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrEmpl001";
            }      
                return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getAllEmployee()
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Employee";
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Employee>(query);
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrEmpl001";
                }
            }
            return errorMessageInfo;
        }
        public async Task<ErrorMessageInfo> getEmployeeByID(long id)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Employee WHERE EmployeeID = @id";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("id", id);
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Employee>(query, dynamicParameters);
                    if (errorMessageInfo.data == null)
                        errorMessageInfo.message = "Không có nhân viên này";
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrEmpl001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> updateNewEmployee(long employeeID, Employee employee)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    try
                    {
                        sql.Open();
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        string querySelect = "SELECT * FROM Employee WHERE EmployeeID = @id";
                        dynamicParameters.Add("id", employeeID);
                        Employee employeeFind = await sql.QuerySingleOrDefaultAsync<Employee>(querySelect, dynamicParameters);
                        if (employeeFind == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có nhân viên này";
                            errorMessageInfo.error_code = "ErrEmpl003";
                        }
                        else
                        {
                            employee.EmployeeCode = employeeFind.EmployeeCode;
                            employee.EmployeeId = employeeFind.EmployeeId;
                            string queryDelete = "UPDATE Employee   " +
                                "SET Birthday = @Birthday, EmployeeName = @EmployeeName, IdentityNumber = @IdentityNumber " +
                                "Where EmployeeID = @EmployeeId";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryDelete, employee);
                            errorMessageInfo.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrEmpl001";
                    }
                    sql.Close();
                }
            }
            catch (Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrEmpl001";
            }
            return errorMessageInfo;
        }
    }
}
