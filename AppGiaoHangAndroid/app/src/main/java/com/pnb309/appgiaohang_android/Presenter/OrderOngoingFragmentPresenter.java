package com.pnb309.appgiaohang_android.Presenter;

import android.content.Context;
import android.net.Uri;

import com.pnb309.appgiaohang_android.Contract.IOrderOngoingFragmentContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Model.CustomerOrderModel;
import com.pnb309.appgiaohang_android.Model.ICustomerOrderModel;

import java.util.ArrayList;

public class OrderOngoingFragmentPresenter implements IOrderOngoingFragmentContract.Presenter {
    private IOrderOngoingFragmentContract.View view;
    private ICustomerOrderModel model;
    public OrderOngoingFragmentPresenter(IOrderOngoingFragmentContract.View view, Context context)
    {
        this.view = view;
        model = new CustomerOrderModel(context);
    }
    @Override
    public void OnLoadingOrderCustomer(long employeeID) {
        model.LoadCustomerOrderByEmployeeIDAndStatus(employeeID, CustomerOrder.ORDER_ONGOING, customerOrderList -> {
            if(customerOrderList == null)
                customerOrderList = new ArrayList<>();
            view.LoadOrderCustomer(customerOrderList);
        });
    }


}
