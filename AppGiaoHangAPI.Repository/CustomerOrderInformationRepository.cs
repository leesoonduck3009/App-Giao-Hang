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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AppGiaoHangAPI.Repository
{
    public class CustomerOrderInformationRepository : ICustomerOrderInformationRepository
    {
        private string connectionString;
        private QuanLyGiaoHang db;
        public CustomerOrderInformationRepository(IConfiguration configuration, QuanLyGiaoHang quanLyGiaoHang)
        {
            connectionString = configuration.GetConnectionString("dbConnection");
            db = quanLyGiaoHang;
        }
        public async Task<ErrorMessageInfo> createNewCustomerOrderInformation(CustomerOrderInformation customerOrderInformation)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            if (string.IsNullOrEmpty(customerOrderInformation.PhoneNumber)
                || string.IsNullOrEmpty(customerOrderInformation.Address)
                || customerOrderInformation.CustomerId == null
                )
            {
                errorMessageInfo.message = "Chưa điền đủ thông tin";
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrCusInfo002";
            }
            else
            {
                
                try
                {
                    if (db.Customers.FindAsync(customerOrderInformation.CustomerId) == null)
                    {
                        errorMessageInfo.message = "Không tồn tại khách hàng này";
                        errorMessageInfo.error_code = "ErrCusInfo003";
                        errorMessageInfo.isErrorEx = true;

                    }
                    else
                    {
                        using (var sqlConnection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                sqlConnection.Open();
                                string query = "Insert into CustomerOrderInformation (CustomerID,Address,PhoneNumber) " +
                                    "VALUES(@CustomerId, @Address, @PhoneNumber) ";
                                errorMessageInfo.data = await sqlConnection.ExecuteAsync(query, customerOrderInformation);
                                errorMessageInfo.isSuccess = true;
                                errorMessageInfo.message = "Insert Success";
                            }
                            catch (Exception e)
                            {
                                errorMessageInfo.isErrorEx = true;
                                errorMessageInfo.message = e.Message;
                                errorMessageInfo.error_code = "ErrCusInfo001";
                            }
                            sqlConnection.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrCusInfo001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> deleteCustomerOrderInformation(long CustomerOrderInformationID)
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
                        string querySelect = "SELECT * FROM CustomerOrderInformation WHERE CustomerOrderInformationID = @id";
                        dynamicParameters.Add("id", CustomerOrderInformationID);
                        Customer customer = await sql.QuerySingleOrDefaultAsync<Customer>(querySelect, dynamicParameters);
                        if (customer == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có thông tin khách hàng này";
                            errorMessageInfo.error_code = "ErrCusInfo003";
                        }
                        else
                        {
                            string queryDelete = "DELETE CustomerOrderInformation Where CustomerOrderInformationID = @id";
                            errorMessageInfo.message = "Xóa khách hàng thành công";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryDelete, dynamicParameters);
                            errorMessageInfo.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrCusInfo001";
                    }
                    sql.Close();
                }
            }
            catch (Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrCusInfo001";
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getAllCustomerOrderInformation()
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    
                    sqlConnection.Open();
                    string query = "SELECT * FROM CustomerOrderInformation";
                    errorMessageInfo.data = await sqlConnection.QueryAsync<CustomerOrderInformation>(query);
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrCusInfo001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getCustomerOrderInformationByCustomerID(long id)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("id", id);
                    sqlConnection.Open();
                    string query = "SELECT * FROM CustomerOrderInformation WHERE CustomerID = @id";
                    errorMessageInfo.data = await sqlConnection.QueryAsync<CustomerOrderInformation>(query,dynamicParameters);
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrCusInfo001";
                }
            }
            return errorMessageInfo;
        }
        public async Task<ErrorMessageInfo> getCustomerOrderInformationByID(long id)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("id", id);
                    sqlConnection.Open();
                    string query = "SELECT * FROM CustomerOrderInformation WHERE CustomerOrderInformationID = @id";
                    errorMessageInfo.data = await sqlConnection.QueryAsync<CustomerOrderInformation>(query, dynamicParameters);
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrCusInfo001";
                }
            }
            return errorMessageInfo;
        }
        public async Task<ErrorMessageInfo> updateNewCustomerOrderInformation(long CustomerOrderInformationID, CustomerOrderInformation customerOrderInformation)
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
                        string querySelect = "SELECT * FROM CustomerOrderInformation WHERE CustomerOrderInformationID = @id";
                        dynamicParameters.Add("id", CustomerOrderInformationID);
                        customerOrderInformation.CustomerOrderInformationId = CustomerOrderInformationID;
                        Customer customerFind = await sql.QuerySingleOrDefaultAsync<Customer>(querySelect, dynamicParameters);
                        if (customerFind == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có thông tin khách hàng này";
                            errorMessageInfo.error_code = "ErrCusInfo003";
                        }
                        else
                        {                           
                            string queryUpdate = "UPDATE CustomerOrderInformation   " +
                                "SET Address = @Address, PhoneNumber = @PhoneNumber " +
                                "Where CustomerOrderInformationID = @CustomerOrderInformationId";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryUpdate, customerOrderInformation);
                            errorMessageInfo.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrCusInfo001";
                    }
                    sql.Close();
                }
            }
            catch (Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrCusInfo001";
            }
            return errorMessageInfo;
        }
    }
}
