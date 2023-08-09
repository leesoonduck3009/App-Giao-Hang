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
    public class ProductRepository : IProductRepository
    {
        private string connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("dbConnection");
        }
        public async Task<ErrorMessageInfo> createNewProduct(List<Product> products)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach(Product product in products)
            {

                if (string.IsNullOrEmpty(product.ProductName) || 
                    product.Price == null
                    )
                {
                    errorMessageInfo.message = "Chưa điền đủ thông tin";
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrPro002";
                    return errorMessageInfo;
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
                                string query = "INSERT INTO Product(ProductName,Price,Description)" +
                                    " VALUES (@ProductName, @Price, @Description)";
 
                                errorMessageInfo.data = await sqlConnection.ExecuteAsync(query, product);
                                errorMessageInfo.isSuccess = true;
                            }
                            catch (Exception e)
                            {
                                errorMessageInfo.isErrorEx = true;
                                errorMessageInfo.message = e.Message;
                                errorMessageInfo.error_code = "ErrPro001";
                                return errorMessageInfo;

                            }
                            sqlConnection.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrPro001";
                        return errorMessageInfo;
                    }
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> deleteProduct(List<Product> products)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach(Product product1 in products)
            try
            {
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    try
                    {
                        sql.Open();
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        string querySelect = "SELECT * FROM Product WHERE ProductID = @id";
                        dynamicParameters.Add("id", product1.ProductId);
                        Product product = await sql.QuerySingleOrDefaultAsync<Product>(querySelect, dynamicParameters);
                        if (product == null)
                        {
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = "Không có khách hàng này";
                            errorMessageInfo.error_code = "ErrPro003";
                            return errorMessageInfo;

                            }
                            else
                        {
                            string queryDelete = "DELETE Product Where ProductID = @id";
                            errorMessageInfo.message = "Xóa khách hàng thành công";
                            errorMessageInfo.data = await sql.ExecuteAsync(queryDelete, dynamicParameters);
                            errorMessageInfo.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessageInfo.message = e.Message;
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrPro001";
                        return errorMessageInfo;
                    }
                    sql.Close();
                }
            }
            catch (Exception e)
            {
                errorMessageInfo.message = e.Message;
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ErrPro001";
                return errorMessageInfo;
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getAllProduct()
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Product";
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Product>(query);
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrPro001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getProductByID(long id)
        {

            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM Product WHERE ProductID = @id";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("id", id);
                    errorMessageInfo.data = await sqlConnection.QueryAsync<Product>(query, dynamicParameters);
                    if (errorMessageInfo.data == null)
                        errorMessageInfo.message = "Không có khách hàng này";
                    errorMessageInfo.isSuccess = true;
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrPro001";
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> updateNewProduct(List<Product> products)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach (Product product in products)
            {
                try
                {
                    using (SqlConnection sql = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sql.Open();
                            DynamicParameters dynamicParameters = new DynamicParameters();
                            string querySelect = "SELECT * FROM Product WHERE ProductID = @id";
                            dynamicParameters.Add("id", product.ProductId);
                            Product productFind = await sql.QuerySingleOrDefaultAsync<Product>(querySelect, dynamicParameters);
                            if (productFind == null)
                            {
                                errorMessageInfo.isErrorEx = true;
                                errorMessageInfo.message = "Không có sảng phẩm này";
                                errorMessageInfo.error_code = "ErrPro003";
                                return errorMessageInfo;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(product.Description))
                                    product.Description = "";
                                string queryUpdate = "UPDATE Product   " +
                                    "SET ProductName = @ProductName, Price = @Price, Description = @Description " +
                                    "Where ProductID = @ProductID";
                                errorMessageInfo.data = await sql.ExecuteAsync(queryUpdate, product);
                                errorMessageInfo.isSuccess = true;
                            }
                        }
                        catch (Exception e)
                        {
                            errorMessageInfo.message = e.Message;
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.error_code = "ErrPro001";
                            return errorMessageInfo;
                        }
                        sql.Close();
                    }
                }
                catch (Exception e)
                {
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrPro001";
                    return errorMessageInfo;
                }
            }
            return errorMessageInfo;
        }
    }
}
