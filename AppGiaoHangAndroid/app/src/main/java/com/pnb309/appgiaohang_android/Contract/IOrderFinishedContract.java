package com.pnb309.appgiaohang_android.Contract;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.List;

public interface IOrderFinishedContract {
    interface View{
        void LoadOrderFinishedCustomer(List<CustomerOrder> customerOrderList);

    }
    interface Presenter{
        void OnLoadingOrderFinishedCustomer(long employeeID);

    }
}
