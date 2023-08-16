package com.pnb309.appgiaohang_android.Contract;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.List;

public interface IOrderCancelledContract {
    interface View{
        void LoadOrderCancelledCustomer(List<CustomerOrder> customerOrderList);

    }
    interface Presenter{
        void OnLoadingOrderCancelledCustomer(long employeeID);

    }
}
