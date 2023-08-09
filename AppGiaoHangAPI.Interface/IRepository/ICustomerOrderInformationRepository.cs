using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Interface.IRepository
{
    public interface ICustomerOrderInformationRepository
    {
        Task<ErrorMessageInfo> getAllCustomerOrderInformation();
        Task<ErrorMessageInfo> getCustomerOrderInformationByCustomerID(long id);
        Task<ErrorMessageInfo> getCustomerOrderInformationByID(long id);
        Task<ErrorMessageInfo> createNewCustomerOrderInformation(List<CustomerOrderInformation> customerOrderInformations);
        Task<ErrorMessageInfo> updateNewCustomerOrderInformation(List<CustomerOrderInformation> customerOrderInformations);
        Task<ErrorMessageInfo> deleteCustomerOrderInformation(List<CustomerOrderInformation> CustomerOrderInformations);
    }
}
