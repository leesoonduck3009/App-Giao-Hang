using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private QuanLyGiaoHang db;
        public AccountRepository(QuanLyGiaoHang db)
        {
            this.db = db;
        }

        public async Task<ErrorMessageInfo> getAccount(string username, string password)
        {
            ErrorMessageInfo errorMessageInfo = new ErrorMessageInfo();
            try
            {
                errorMessageInfo.data = await db.Accounts.Include(p=>p.Employee).SingleOrDefaultAsync(p => p.UserName == username && p.Password == password);
                        errorMessageInfo.isSuccess = true;
            }
            catch(Exception e)
            {
                errorMessageInfo.isErrorEx = true;
                errorMessageInfo.error_code = "ERACC001";
                errorMessageInfo.message = e.Message;
            }
            return errorMessageInfo;
        }
    }
}
