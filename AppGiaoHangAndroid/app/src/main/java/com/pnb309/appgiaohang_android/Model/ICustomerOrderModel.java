package com.pnb309.appgiaohang_android.Model;

import android.net.Uri;

import com.pnb309.appgiaohang_android.Entity.Customer;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.ArrayList;
import java.util.List;

public interface ICustomerOrderModel {
    void LoadCustomerOrderByEmployeeIDAndStatus(long id,String status, OnLoadCustomerOrderByEmployeeIDListener listener);
    interface OnLoadCustomerOrderByEmployeeIDListener{
       void OnFinishLoadCustomerOrderByEmployeeID(List<CustomerOrder> customerOrderList);
    }
    void updateCustomerOrderStatus( CustomerOrder customerOrder,Uri updateImage, OnUpdateCustomerOrderListener listener);
    interface OnUpdateCustomerOrderListener{
        void OnFinishhUpdateCustomerOrder(boolean isSuccess,Exception e);
    }
}
