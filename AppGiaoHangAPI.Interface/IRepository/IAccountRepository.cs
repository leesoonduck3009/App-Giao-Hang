using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Interface.IRepository
{
    public interface IAccountRepository
    {
        Task<ErrorMessageInfo> getAccount(string username, string password);
        Task<ErrorMessageInfo> getAllAccount();
        Task<ErrorMessageInfo> getAccountByID(long id);
        Task<ErrorMessageInfo> createNewAccount(Account account);
        Task<ErrorMessageInfo> updateAccount(long accountID, Account account);
        Task<ErrorMessageInfo> deleteAccount(long accountID);
        Task<ErrorMessageInfo> changePassword(Account account);

    }
}
