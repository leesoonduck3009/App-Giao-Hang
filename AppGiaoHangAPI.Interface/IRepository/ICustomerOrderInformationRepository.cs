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
        Task<ErrorMessageInfo> createNewCustomerOrderInformation(CustomerOrderInformation customerOrderInformation);
        Task<ErrorMessageInfo> updateNewCustomerOrderInformation(long CustomerOrderInformationID, CustomerOrderInformation customerOrderInformation);
        Task<ErrorMessageInfo> deleteCustomerOrderInformation(long CustomerOrderInformationID);
    }
}
