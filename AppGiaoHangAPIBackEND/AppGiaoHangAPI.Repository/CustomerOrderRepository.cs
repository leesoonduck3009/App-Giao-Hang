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
                    .Include(p=>p.Employee)
                    .Include(p=>p.CustomerOrderInformation)
                    .Where(p => p.EmployeeId == employeeID)
                    .AsNoTracking()
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

        public async Task<ErrorMessageInfo> postCustomerOrder(List<CustomerOrder> customerOrders)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach (CustomerOrder customerOrder in customerOrders)
            {
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
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrOrd001";
                    return errorMessageInfo;
                }
            }
            return errorMessageInfo;
        }

        public async Task<ErrorMessageInfo> updateCustomerOrder(List<CustomerOrder> customerOrders)
        {
            
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach (CustomerOrder customerOrder in customerOrders)
            {
                try
                {
                    CustomerOrder customerOrderFind = await quanlyCuaHang.CustomerOrders.AsNoTracking().Include(p => p.CustomerOrderDetails).FirstOrDefaultAsync(p => p.CustomerOrderId == customerOrder.CustomerOrderId);
                    await quanlyCuaHang.Entry(customerOrderFind).ReloadAsync();
                    if (customerOrderFind == null)
                    {
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.message = "This order not exist";
                        errorMessageInfo.error_code = "ErrOrd003";
                        return errorMessageInfo;
                    }
                    else
                    {
                        using (var sqlConnection = new SqlConnection(connectionString))
                        {
                            sqlConnection.Open();
                            // B2. Kiểm tra trong danh sách HoaDonChiTiet mới có đối tượng HoaDonChiTiet cũ không. Nếu không có => Delete HoaDonChiTietCu đi
                            List<CustomerOrderDetail> oldCustomerOrderDetails = customerOrderFind.CustomerOrderDetails.ToList();
                            List<CustomerOrderDetail> newCustomerOrderDetails = customerOrder.CustomerOrderDetails.ToList();
                            foreach (var item in oldCustomerOrderDetails)
                            {
                                if (!newCustomerOrderDetails.Exists(p => p.CustomerOrderId == item.CustomerOrderId && p.ProductId == item.ProductId))
                                {
                                    {
                                        string querydelete = "DELETE CustomerOrderDetail WHERE CustomerOrderId = @CustomerOrderId AND ProductID = @ProductId";
                                        customerOrderFind.TotalPrice -= item.OrderDetailPrice;
                                        await sqlConnection.ExecuteAsync(querydelete, item);
                                    }
                                }
                            }
                            //    B3.Bắt đầu kiểm tra danh sách HoaDonChiTiet mới
                            //     - Nếu ID tồn tại => Update
                            //     - Ngược lại => Insert
                            foreach (var item in newCustomerOrderDetails)
                            {
                                if (oldCustomerOrderDetails.Exists(p => p.CustomerOrderId == item.CustomerOrderId && p.ProductId == item.ProductId))
                                {
                                    {
                                        string queryUpdate = "UPDATE CustomerOrderDetail " +
                                           "SET Quantity = @Quantity " +
                                           "WHERE CustomerOrderID = @CustomerOrderId AND ProductID = @ProductId";
                                        customerOrderFind.TotalPrice -= oldCustomerOrderDetails.Single(p => p.CustomerOrderId == item.CustomerOrderId && p.ProductId == item.ProductId).OrderDetailPrice;
                                        customerOrderFind.TotalPrice += (quanlyCuaHang.Products.Find(item.ProductId).Price * item.Quantity);
                                        await sqlConnection.ExecuteAsync(queryUpdate, item);
                                    }
                                }
                                else
                                {
                                    string queryInsert = "INSERT INTO CustomerOrderDetail(CustomerOrderID,ProductID,Quantity) VALUES(@CustomerOrderId,@ProductId,@Quantity)\r\n";
                                    customerOrderFind.TotalPrice += (quanlyCuaHang.Products.Find(item.ProductId).Price * item.Quantity);
                                    await sqlConnection.ExecuteAsync(queryInsert, item);
                                }
                            }
                            customerOrderFind.OrderStatus = customerOrder.OrderStatus;
                            customerOrderFind.CustomerOrderInformation = customerOrder.CustomerOrderInformation;
                            customerOrderFind.EmployeeId = customerOrder.EmployeeId;
                            string query = "UPDATE CustomerOrder " +
                                   "SET CustomerOrderInformationID = @CustomerOrderInformationId, EmployeeID = @EmployeeId, OrderStatus = @OrderStatus , TotalPrice = @TotalPrice " +
                                   "WHERE CustomerOrderID = @CustomerOrderId";
                            errorMessageInfo.data = await sqlConnection.ExecuteAsync(query, customerOrderFind);
                            sqlConnection.Close();
                        }
                    }

                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrOrd001";
                    return errorMessageInfo;
                }
            }
            return errorMessageInfo;
        }
        public async Task<ErrorMessageInfo> deleteCustomerOrder (List<CustomerOrder> customerOrders)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            foreach (CustomerOrder customerOrder in customerOrders)
            {
                try
                {
                    CustomerOrder customerRemove = await quanlyCuaHang.CustomerOrders.Include(p => p.CustomerOrderDetails).Where(p => p.CustomerOrderId == customerOrder.CustomerOrderId).SingleOrDefaultAsync();
                    if (customerRemove == null)
                    {
                        errorMessageInfo.message = "Không tồn tại order này";
                        errorMessageInfo.isErrorEx = true;
                        errorMessageInfo.error_code = "ErrOrd003";
                        return errorMessageInfo;

                    }
                    else
                    {
                        if (customerRemove.CustomerOrderDetails != null)
                            quanlyCuaHang.CustomerOrderDetails.RemoveRange(customerRemove.CustomerOrderDetails);
                        quanlyCuaHang.CustomerOrders.Remove(customerRemove);
                        errorMessageInfo.data = await quanlyCuaHang.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    errorMessageInfo.isErrorEx = true;
                    errorMessageInfo.message = e.Message;
                    errorMessageInfo.error_code = "ErrOrd001";
                    return errorMessageInfo;
                }
            }
            return errorMessageInfo;

        }
    }
}
