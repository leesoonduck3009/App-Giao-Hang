package com.pnb309.appgiaohang_android.Contract;

import android.net.Uri;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.List;

public interface IOrderOngoingFragmentContract {
    interface View{
        void LoadOrderCustomer(List<CustomerOrder> customerOrderList);
    }
    interface Presenter{
        void OnLoadingOrderCustomer(long employeeID);

    }
}
