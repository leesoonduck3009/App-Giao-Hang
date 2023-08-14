package com.pnb309.appgiaohang_android.Presenter;

import com.pnb309.appgiaohang_android.Contract.IOrderFragmentContract;
import com.pnb309.appgiaohang_android.Model.CustomerOrderModel;
import com.pnb309.appgiaohang_android.Model.ICustomerOrderModel;

import java.util.ArrayList;

public class OrderFragmentPresenter implements IOrderFragmentContract.Presenter {
    private IOrderFragmentContract.View view;
    private ICustomerOrderModel model;
    public OrderFragmentPresenter(IOrderFragmentContract.View view)
    {
        this.view = view;
        model = new CustomerOrderModel();
    }
    @Override
    public void OnLoadingOrderCustomer(long employeeID) {
        model.LoadCustomerOrderByEmployeeID(employeeID,customerOrderList -> {
            if(customerOrderList == null)
                customerOrderList = new ArrayList<>();
            view.LoadOrderCustomer(customerOrderList);
        });
    }
}
