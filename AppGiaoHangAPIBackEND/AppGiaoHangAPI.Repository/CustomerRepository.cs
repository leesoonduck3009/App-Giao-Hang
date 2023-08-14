
using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AppGiaoHangAPI.Repository
{
    public class CustomerRepository: ICustomerRepository
    {
        string connectionString;
        public CustomerRepository(IConfiguration IConfiguration)
        {
            this.connectionString = IConfiguration.GetConnectionString("dbConnection");
        }

        public async Task<ErrorMessageInfo> createNewCustomer(List<Customer> customers)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();

            foreach (var customer in customers)
            {
                if (string.IsNullOrEmpty(customer.CustomerName)
                    || string.IsNullOrEmpty(customer.CustomerRank)
                    || customer.Birthday == null
                    )
                {
                    errorMessageInfo.message = "Chưa điền đủ thông tin";
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrCus002";
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
                                customer.DateCreate = DateTime.Now;
                                string query = "Insert into Customer(CustomerName, Birthday, CustomerRank, DateCreate) values (@CustomerName    ," +
                                    "@Birthday, @CustomerRank, @DateCreate) ";
                                errorMessageInfo.data = await sqlConnection.ExecuteAsync(query, customer);
                                errorMessageInfo.isSuccess = true;
                            }
                            catch (Exception e)
                            {
                                errorMessageInfo.isErrorEx = true;
                                errorMessageInfo.message = e.Message;
                                errorMessageInfo.error_code = "ErrCus001";
                            }
                            sqlConnection.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrCus001";
                    }
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> deleteCustomer(List<Customer> customers)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach (Customer customer1 in customers)
            {
                try
                {
                    using (SqlConnection sql = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sql.Open();
                            DynamicParameters dynamicParameters = new DynamicParameters();
                            string querySelect = "SELECT * FROM Customer WHERE CustomerID = @id";
                            dynamicParameters.Add("id", customer1.CustomerId);
                            Customer customer = await sql.QuerySingleOrDefaultAsync<Customer>(querySelect, dynamicParameters);
                            if (customer == null)
                            {
                                errorMessageInfo.isErrorEx = true;
                                errorMessageInfo.message = "Không có khách hàng này";
                                errorMessageInfo.error_code = "ErrCus003";
                                return errorMessageInfo;
                            }
                            else
                            {
                                string queryDelete = "DELETE Customer Where CustomerID = @id";
                                errorMessageInfo.message = "Xóa khách hàng thành công";
                                errorMessageInfo.data = await sql.ExecuteAsync(queryDelete, dynamicParameters);
                                errorMessageInfo.isSuccess = true;
                            }
                        }
                        catch (Exception e)
                        {
                            errorMessageInfo.message = e.Message;
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.error_code = "ErrCus001";
                            return errorMessageInfo;

                        }
                        sql.Close();
                    }
                }
                catch (Exception e)
                {
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrCus001";
                    return errorMessageInfo;

                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getAllCustomer()
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Customer";
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Customer>(query);
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrCus001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getCustomerByID(long id)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Customer WHERE CustomerID = @id";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("id", id);
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Customer>(query, dynamicParameters);
                    if (errorMessageInfo.data == null)
                        errorMessageInfo.message = "Không có khách hàng này";
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrCus001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> updateNewCustomer(List<Customer> customers)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach (Customer customer in customers)
            {
                try
                {
                    using (SqlConnection sql = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sql.Open();
                            DynamicParameters dynamicParameters = new DynamicParameters();
                            string querySelect = "SELECT * FROM Customer WHERE CustomerID = @id";
                            dynamicParameters.Add("id", customer.CustomerId);
                            Customer customerFind = await sql.QuerySingleOrDefaultAsync<Customer>(querySelect, dynamicParameters);
                            if (customerFind == null)
                            {
                                errorMessageInfo.isErrorEx = true;
                                errorMessageInfo.message = "Không có khách hàng này";
                                errorMessageInfo.error_code = "ErrCus003";
                                return errorMessageInfo;
                            }
                            else
                            {
                                customer.CustomerCode = customerFind.CustomerCode;
                                customer.CustomerId = customerFind.CustomerId;
                                string queryDelete = "UPDATE Customer   " +
                                    "SET Birthday = @Birthday, CustomerName = @CustomerName, CustomerRank = @CustomerRank " +
                                    "Where CustomerID = @CustomerID";
                                errorMessageInfo.data = await sql.ExecuteAsync(queryDelete, customer);
                                errorMessageInfo.isSuccess = true;
                            }
                        }
                        catch (Exception e)
                        {
                            errorMessageInfo.message = e.Message;
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.error_code = "ErrCus001";
                            return errorMessageInfo;

                        }
                        sql.Close();
                    }
                }
                catch (Exception e)
                {
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrCus001";
                    return errorMessageInfo;
                }
            }
            return errorMessageInfo;
        }
    }
}
