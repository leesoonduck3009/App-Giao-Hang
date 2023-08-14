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
        Task<ErrorMessageInfo> createNewProduct(List<Product> products);
        Task<ErrorMessageInfo> updateNewProduct(List<Product> products);
        Task<ErrorMessageInfo> deleteProduct(List<Product> products);
    }
}
