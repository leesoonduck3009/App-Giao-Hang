package com.pnb309.appgiaohang_android.Model;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.List;

public interface ICustomerOrderModel {
    void LoadCustomerOrderByEmployeeID(long id, OnLoadCustomerOrderByEmployeeIDListener listener);
    interface OnLoadCustomerOrderByEmployeeIDListener{
       void OnFinishLoadCustomerOrderByEmployeeID(List<CustomerOrder> customerOrderList);
    }
}
