using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Interface.IRepository
{
    public interface ICustomerOrderRepository
    {
        Task<ErrorMessageInfo> getCustomerOrderByCustomerID(long customerID);  
        Task<ErrorMessageInfo> getCustomerOrderByEmployeeID(long employeeID);
        Task<ErrorMessageInfo> getCustomerOrderByOrderID(long customerOrderID);
        Task<ErrorMessageInfo> postCustomerOrder(List<CustomerOrder> customerOrders);
        Task<ErrorMessageInfo> deleteCustomerOrder(List<CustomerOrder> customerOrders);
        Task<ErrorMessageInfo> updateCustomerOrder(List<CustomerOrder> customerOrders);
        Task<ErrorMessageInfo> getAllCustomerOrder();
        Task<ErrorMessageInfo> getAllCustomerOrderByOrderStatusAndEmployeeID(CustomerOrder customerOrder);
        Task<ErrorMessageInfo> putSingleCustomerOrder(long orderID, CustomerOrder customerOrder);
    }
}
