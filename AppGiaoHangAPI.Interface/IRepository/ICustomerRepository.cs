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
        Task<ErrorMessageInfo> createNewCustomer(List<Customer> customer);
        Task<ErrorMessageInfo> updateNewCustomer(List<Customer> customers);
        Task<ErrorMessageInfo> deleteCustomer(List<Customer> customers);
    }
}
