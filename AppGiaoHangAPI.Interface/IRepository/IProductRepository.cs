using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Interface.IRepository
{
    public interface IProductRepository
    {
        Task<ErrorMessageInfo> getAllProduct();
        Task<ErrorMessageInfo> getProductByID(long id);
        Task<ErrorMessageInfo> createNewProduct(Product Product);
        Task<ErrorMessageInfo> updateNewProduct(long ProductID, Product product);
        Task<ErrorMessageInfo> deleteProduct(long ProductID);
    }
}
