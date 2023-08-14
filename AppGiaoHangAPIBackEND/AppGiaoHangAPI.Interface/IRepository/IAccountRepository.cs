using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Interface.IRepository
{
    public interface IAccountRepository
    {
        Task<ErrorMessageInfo> getAccount(string username, string password);
        Task<ErrorMessageInfo> getAllAccount();
        Task<ErrorMessageInfo> getAccountByID(long id);
        Task<ErrorMessageInfo> createNewAccount(List<Account> accounts);
        Task<ErrorMessageInfo> updateAccount(List<Account> accounts);
        Task<ErrorMessageInfo> deleteAccount(List<Account> accounts);
        Task<ErrorMessageInfo> changePassword(Account account);

    }
}
