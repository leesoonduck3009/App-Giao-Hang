package com.pnb309.appgiaohang_android.Presenter;

import android.content.Context;

import com.pnb309.appgiaohang_android.Contract.IOrderCancelledContract;
import com.pnb309.appgiaohang_android.Contract.IOrderOngoingFragmentContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Model.CustomerOrderModel;
import com.pnb309.appgiaohang_android.Model.ICustomerOrderModel;

import java.util.ArrayList;

public class OrderCancelledPresenter implements IOrderCancelledContract.Presenter {
    private IOrderCancelledContract.View view;
    private ICustomerOrderModel model;
    public OrderCancelledPresenter(IOrderCancelledContract.View view, Context context)
    {
        this.view = view;
        model = new CustomerOrderModel(context);
    }
    @Override
    public void OnLoadingOrderCancelledCustomer(long employeeID) {
        model.LoadCustomerOrderByEmployeeIDAndStatus(employeeID, CustomerOrder.ORDER_CANCLED, customerOrderList -> {
            if(customerOrderList == null)
                customerOrderList = new ArrayList<>();
            view.LoadOrderCancelledCustomer(customerOrderList);
        });
    }
}
