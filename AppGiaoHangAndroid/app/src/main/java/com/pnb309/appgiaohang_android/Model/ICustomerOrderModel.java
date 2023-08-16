package com.pnb309.appgiaohang_android.Model;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.List;

public interface ICustomerOrderModel {
    void LoadCustomerOrderByEmployeeIDAndStatus(long id,String status, OnLoadCustomerOrderByEmployeeIDListener listener);
    interface OnLoadCustomerOrderByEmployeeIDListener{
       void OnFinishLoadCustomerOrderByEmployeeID(List<CustomerOrder> customerOrderList);
    }
}
