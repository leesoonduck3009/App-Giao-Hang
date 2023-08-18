package com.pnb309.appgiaohang_android.Presenter;

import android.content.Context;

import com.pnb309.appgiaohang_android.Contract.IOrderCancelledContract;
import com.pnb309.appgiaohang_android.Contract.IOrderFinishedContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Model.CustomerOrderModel;
import com.pnb309.appgiaohang_android.Model.ICustomerOrderModel;

import java.util.ArrayList;

public class OrderFinishedPresenter implements IOrderFinishedContract.Presenter {
    private IOrderFinishedContract.View view;
    private ICustomerOrderModel model;
    public OrderFinishedPresenter(IOrderFinishedContract.View view, Context context)
    {
        this.view = view;
        model = new CustomerOrderModel(context);
    }
    @Override
    public void OnLoadingOrderFinishedCustomer(long employeeID) {
        model.LoadCustomerOrderByEmployeeIDAndStatus(employeeID, CustomerOrder.ORDER_CANCLED, customerOrderList -> {
            if(customerOrderList == null)
                customerOrderList = new ArrayList<>();
            view.LoadOrderFinishedCustomer(customerOrderList);
        });
    }
}
