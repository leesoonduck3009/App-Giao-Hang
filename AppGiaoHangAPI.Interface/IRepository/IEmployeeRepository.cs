using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Interface.IRepository
{
    public interface IEmployeeRepository
    {
        Task<ErrorMessageInfo> getAllEmployee();
        Task<ErrorMessageInfo> getEmployeeByID(long id);
        Task<ErrorMessageInfo> createNewEmployee(Employee employee);
        Task<ErrorMessageInfo> updateNewEmployee(long employeeID,Employee employee);
        Task<ErrorMessageInfo> deleteNewEmployee(long employeeID);

    }
}
