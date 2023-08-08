using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private QuanLyGiaoHang db;
        private string connectionString;
        public AccountRepository(QuanLyGiaoHang db, IConfiguration configuration)
        {
            this.db = db;
            this.connectionString = configuration.GetConnectionString("dbConnection");
        }

        public async Task<ErrorMessageInfo> changePassword(Account account)
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
                        string querySelect = "SELECT * FROM Account WHERE UserName = @UserName";
                        dynamicParameters.Add("UserName", account.UserName);
                        dynamicParameters.Add("Password", account.Password);
                        Account customerFind = await sql.QuerySingleOrDefaultAsync<Account>(querySelect, dynamicParameters);
                        if (customerFind == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có tài khoản này";
                            errorMessageInfo.error_code = "ErrAcc003";
                        }
                        else
                        {

                            string queryUpdate = "UPDATE Account   " +
                                "SET Password = @Password " +
                                "Where UserName = @UserName";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryUpdate, account);
                            errorMessageInfo.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrAcc001";
                    }
                    sql.Close();
                }
            }
            catch (Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrAcc001";
            }

            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> createNewAccount(Account account)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            if (string.IsNullOrEmpty(account.UserName)
                || string.IsNullOrEmpty(account.Password)
                || account.Roles == null
                )
            {
                errorMessageInfo.message = "Chưa điền đủ thông tin";
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrAcc002";
            }
            else
            {
                
                try
                {
                    if (db.Accounts.Where(p => p.UserName == account.UserName).SingleOrDefault() != null)
                    {
                        errorMessageInfo.message = "Tên đăng nhập này đã tồn tại";
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrAcc003";
                    }
                    else
                    {
                        using (var sqlConnection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                sqlConnection.Open();
                                string query = "INSERT INTO Account (UserName,Password,AccountCreateTime, EmployeeID, Roles)" +
                                    " VALUES (@UserName,@Password,GETDATE(), '1', @Roles)";
                                
                                errorMessageInfo.data = await sqlConnection.ExecuteAsync(query, account);
                                errorMessageInfo.isSuccess = true;
                            }
                            catch (Exception e)
                            {
                                errorMessageInfo.isErrorEx = true;
                                errorMessageInfo.message = e.Message;
                                errorMessageInfo.error_code = "ErrAcc001";
                            }
                            sqlConnection.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrAcc001";
                }

            }
            return errorMessageInfo;

        }

        public async Task<ErrorMessageInfo> deleteAccount(long accountID)
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
                        string querySelect = "SELECT * FROM Account WHERE AccountID = @id";
                        dynamicParameters.Add("id", accountID);
                        Account customer = await sql.QuerySingleOrDefaultAsync<Account>(querySelect, dynamicParameters);
                        if (customer == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có tài khoản này";
                            errorMessageInfo.error_code = "ErrAcc003";
                        }
                        else
                        {
                            string queryDelete = "DELETE Account Where AccountID = @id";
                            errorMessageInfo.message = "Xóa tài khoản thành công";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryDelete, dynamicParameters);
                            errorMessageInfo.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrAcc001";
                    }
                    sql.Close();
                }
            }
            catch (Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrAcc001";
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getAccount(string username, string password)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                errorMessageInfo.data = await db.Accounts.Include(p=>p.Employee).SingleOrDefaultAsync(p => p.UserName == username && p.Password == password);
                        errorMessageInfo.isSuccess = true;
            }
            catch(Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ERACC001";
                errorMessageInfo.message = e.Message;
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getAccountByID(long id)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Account WHERE AccountID = @id";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("id", id);
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Account>(query, dynamicParameters);
                    if (errorMessageInfo.data == null)
                        errorMessageInfo.message = "Không có khách hàng này";
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrAcc001";
                }
            }
            return errorMessageInfo;

        }

        public async Task<ErrorMessageInfo> getAllAccount()
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Account";
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Account>(query);
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrAcc001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> updateAccount(long accountID, Account account)
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
                        string querySelect = "SELECT * FROM Account WHERE AccountID = @id";
                        dynamicParameters.Add("id", accountID);
                        Account customerFind = await sql.QuerySingleOrDefaultAsync<Account>(querySelect, dynamicParameters);
                        if (customerFind == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có tài khoản này";
                            errorMessageInfo.error_code = "ErrAcc003";
                        }
                        else
                        {
                          
                            string queryUpdate = "UPDATE Account   " +
                                "SET Roles = @Roles, Password = @Password " +
                                "Where AccountID = @AccountId";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryUpdate, account);
                            errorMessageInfo.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrAcc001";
                    }
                    sql.Close();
                }
            }
            catch (Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrAcc001";
            }

            return errorMessageInfo;
        }
    }
}
