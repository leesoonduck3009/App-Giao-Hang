using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppGiaoHangAPI.Repository
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private string connectionString;
        private QuanLyGiaoHang quanlyCuaHang;
        public CustomerOrderRepository(IConfiguration configuration,QuanLyGiaoHang quanlyCuaHang)
        {
            connectionString = configuration.GetConnectionString("dbConnection");
            this.quanlyCuaHang = quanlyCuaHang;
        }
        

        public async Task<ErrorMessageInfo> getAllCustomerOrder()
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                  
                errorMessageInfo.data = quanlyCuaHang.CustomerOrders
                    .Include(p=>p.CustomerOrderDetails)
                    .ToList();
                errorMessageInfo.isSuccess = true;
            }
            catch (Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.message = e.Message;
                errorMessageInfo.error_code = "ErrOrd001";
            }    
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getCustomerOrderByCustomerID(long customerID)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {

                errorMessageInfo.data = await quanlyCuaHang.CustomerOrders
                    .Include(p => p.CustomerOrderDetails)
                    .Where(p=>p.CustomerOrderInformationId == customerID)
                    .ToListAsync();
                errorMessageInfo.isSuccess = true;
            }
            catch (Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.message = e.Message;
                errorMessageInfo.error_code = "ErrOrd001";
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getCustomerOrderByEmployeeID(long employeeID)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {

                errorMessageInfo.data = await quanlyCuaHang.CustomerOrders
                    .Include(p => p.CustomerOrderDetails)
                    .Where(p => p.EmployeeId == employeeID)
                    .ToListAsync();
                errorMessageInfo.isSuccess = true;
            }
            catch (Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.message = e.Message;
                errorMessageInfo.error_code = "ErrOrd001";
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> getCustomerOrderByOrderID(long customerOrderID)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {

                errorMessageInfo.data = await quanlyCuaHang.CustomerOrders
                    .Include(p => p.CustomerOrderDetails)
                    .Where(p => p.CustomerOrderId == customerOrderID)
                    .SingleOrDefaultAsync();
                errorMessageInfo.isSuccess = true;
            }
            catch (Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.message = e.Message;
                errorMessageInfo.error_code = "ErrOrd001";
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> postCustomerOrder(CustomerOrder customerOrder)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();
                    int insertedOrderID;
                    using (var transaction = sqlConnection.BeginTransaction())
                    {
                        try
                        {
                            string insertQuery = @"
                    INSERT INTO CustomerOrder (CustomerOrderInformationId, EmployeeId, OrderStatus, OrderDate)
                    VALUES (@CustomerOrderInformationId, @EmployeeId, @OrderStatus, GETDATE());
                    SELECT SCOPE_IDENTITY() AS OrderID;";
                            insertedOrderID = await sqlConnection.ExecuteScalarAsync<int>(insertQuery, customerOrder, transaction);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            errorMessageInfo.isErrorEx = true;
                            errorMessageInfo.message = ex.Message;
                            errorMessageInfo.error_code = "ErrOrd001";
                            return errorMessageInfo;
                        }
                    }
                    List<CustomerOrderDetail> customerOrderDetails = customerOrder.CustomerOrderDetails.ToList();
                    customerOrderDetails.ForEach(item => item.CustomerOrderId = insertedOrderID);
                    string insertQueryOrderDetail = @"
                    INSERT INTO CustomerOrderDetail (CustomerOrderID, ProductID, Quantity) 
                    VALUES (@CustomerOrderID, @ProductID, @Quantity)";
                    errorMessageInfo.data = await sqlConnection.ExecuteAsync(insertQueryOrderDetail, customerOrderDetails);
                    errorMessageInfo.message = "Success";
                }
            }
            catch(Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.message = e.Message;
                errorMessageInfo.error_code = "ErrOrd001";
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> updateCustomerOrder(long customerOrderID, CustomerOrder customerOrder)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                CustomerOrder customerOrderFind = await quanlyCuaHang.CustomerOrders.Include(p=>p.CustomerOrderDetails).FirstOrDefaultAsync(p=>p.CustomerOrderId==customerOrder.CustomerOrderId);
                if (customerOrderFind == null)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = "This order not exist";
                    errorMessageInfo.error_code = "ErrOrd003";
                }
                else
                {
                    using (var sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        // B2. Kiểm tra trong danh sách HoaDonChiTiet mới có đối tượng HoaDonChiTiet cũ không. Nếu không có => Delete HoaDonChiTietCu đi
                        List<CustomerOrderDetail> customerOrderDetailFind = customerOrderFind.CustomerOrderDetails.Where(p => p.CustomerOrderId == customerOrder.CustomerOrderId).ToList();
                        if (customerOrderDetailFind != null)
                        {
                            if (customerOrder.CustomerOrderDetails == null)
                            {
                                string querydelete = "Delete CustomerOrderDetails WHERE CustomerOrderID = @CustomerOrderId";
                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("CustomerOrderID", customerOrderID);
                                await sqlConnection.ExecuteAsync(querydelete, parameters);
                            }
                            else
                            {
                                customerOrderDetailFind.ForEach(async item =>
                                {
                                    CustomerOrderDetail customerOrderDetail = customerOrder.CustomerOrderDetails.SingleOrDefault(p => p.CustomerOrderDetailId == item.CustomerOrderDetailId);
                                    if (customerOrderDetail == null)
                                    {

                                        customerOrder.TotalPrice -= item.OrderDetailPrice;
                                        string query = "DELETE CustomerOrderDetail WHERE CustomerOrderDetailID = @CustomerOrderDetailId";
                                        await sqlConnection.ExecuteAsync(query, item);
                                    }
                                });
                            }
                        }
                        string queryUpdate = "UPDATE CustomerOrder " +
                        "SET TotalPrice = @TotalPrice " +
                        "WHERE CustomerOrderID = @CustomerOrderId";
                        errorMessageInfo.data = await sqlConnection.ExecuteAsync(queryUpdate, customerOrder);
                        
                        //B3. Bắt đầu kiểm tra danh sách HoaDonChiTiet mới
                        //-Nếu ID tồn tại => Update
                        //- Ngược lại => Insert
                        if (customerOrder.CustomerOrderDetails != null)
                        {
                            if (customerOrderFind.CustomerOrderDetails.Count == 0)
                            {
                                string queryI = "INSERT INTO CustomerOrderDetail(CustomerOrderID,ProductID,Quantity) VALUES(@CustomerOrderId,@ProductId,@Quantity)\r\n";

                                await sqlConnection.ExecuteAsync(queryI, customerOrder.CustomerOrderDetails);
                            }
                            else
                            {
                                customerOrder.CustomerOrderDetails.ToList().ForEach(async item =>
                                {

                                    CustomerOrderDetail customerOrderDetail = customerOrderFind.CustomerOrderDetails.Where(p => p.CustomerOrderDetailId == item.CustomerOrderDetailId).SingleOrDefault();
                                    if (customerOrderDetail == null)
                                    {
                                        string query = "INSERT INTO CustomerOrderDetail(CustomerOrderID,ProductID,Quantity) VALUES(@CustomerOrderId,@ProductId,@Quantity)\r\n";
                                        item.OrderDetailPrice = quanlyCuaHang.Products.Find(item.ProductId).Price * item.Quantity;
                                        await sqlConnection.ExecuteAsync(query, item);
                                    }
                                    else
                                    {
                                        string query = "UPDATE CustomerOrderDetail " +
                                       "SET ProductID = @ProductId, Quantity = @Quantity " +
                                       "WHERE CustomerOrderDetailID = @CustomerOrderDetailId";
                                        await sqlConnection.ExecuteAsync(query, item);
                                        item.OrderDetailPrice = quanlyCuaHang.Products.Find(item.ProductId).Price * item.Quantity;
                                        customerOrder.TotalPrice = customerOrder.TotalPrice - customerOrderDetail.OrderDetailPrice + item.OrderDetailPrice;
                                    }
                                });
                            }
                        }
                        string query = "UPDATE CustomerOrder " +
                                   "SET CustomerOrderInformationID = @CustomerOrderInformationId, EmployeeID = @EmployeeId, OrderStatus = @OrderStatus , TotalPrice = @TotalPrice " +
                                   "WHERE CustomerOrderID = @CustomerOrderId";
                        errorMessageInfo.data = await sqlConnection.ExecuteAsync(query, customerOrder);
                        sqlConnection.Close();
                    }
                }

            }
            catch (Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.message = e.Message;
                errorMessageInfo.error_code = "ErrOrd001";
            }
            return errorMessageInfo;
        }
        public async Task<ErrorMessageInfo> deleteCustomerOrder(long customerOrderID)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                 CustomerOrder customerRemove = await quanlyCuaHang.CustomerOrders.Include(p=>p.CustomerOrderDetails).Where(p=> p.CustomerOrderId == customerOrderID).SingleOrDefaultAsync();
                if(customerRemove==null)
                {
                    errorMessageInfo.message = "Không tồn tại order này";
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.error_code = "ErrOrd003";
                }    
                else
                {
                    if(customerRemove.CustomerOrderDetails!=null)
                        quanlyCuaHang.CustomerOrderDetails.RemoveRange(customerRemove.CustomerOrderDetails);
                    quanlyCuaHang.CustomerOrders.Remove(customerRemove);
                    errorMessageInfo.data = await quanlyCuaHang.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.message = e.Message;
                errorMessageInfo.error_code = "ErrOrd001";
            }
            return errorMessageInfo;

        }
    }
}
