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
        Task<ErrorMessageInfo> createNewEmployee(List<Employee> employees);
        Task<ErrorMessageInfo> updateNewEmployee(List<Employee> employees);
        Task<ErrorMessageInfo> deleteNewEmployee(List<Employee> employees);
        Task<ErrorMessageInfo> updateEmployeeLocation(long id, Employee employee);

    }
}
