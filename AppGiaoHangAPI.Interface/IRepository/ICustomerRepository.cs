using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Interface.IRepository
{
    public interface ICustomerRepository
    {
        Task<ErrorMessageInfo> getAllCustomer();
        Task<ErrorMessageInfo> getCustomerByID(long id);
        Task<ErrorMessageInfo> createNewCustomer(Customer customer);
        Task<ErrorMessageInfo> updateNewCustomer(long customerID, Customer employee);
        Task<ErrorMessageInfo> deleteCustomer(long customerID);
    }
}
