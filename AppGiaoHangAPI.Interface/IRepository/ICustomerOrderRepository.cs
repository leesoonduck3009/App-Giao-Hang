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
        Task<ErrorMessageInfo> postCustomerOrder(CustomerOrder customerOrder);
        Task<ErrorMessageInfo> deleteCustomerOrder(long customerOrderID);
        Task<ErrorMessageInfo> updateCustomerOrder(long customerOrderID, CustomerOrder customerOrder);
        Task<ErrorMessageInfo> getAllCustomerOrder();
    }
}
