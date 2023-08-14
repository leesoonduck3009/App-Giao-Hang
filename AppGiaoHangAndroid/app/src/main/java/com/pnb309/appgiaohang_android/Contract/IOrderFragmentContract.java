package com.pnb309.appgiaohang_android.Contract;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.List;

public interface IOrderFragmentContract {
    interface View{
        void LoadOrderCustomer(List<CustomerOrder> customerOrderList);
    }
    interface Presenter{
        void OnLoadingOrderCustomer(long employeeID);
    }
}
