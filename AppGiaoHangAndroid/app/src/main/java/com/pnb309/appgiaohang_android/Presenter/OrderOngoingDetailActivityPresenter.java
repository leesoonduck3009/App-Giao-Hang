package com.pnb309.appgiaohang_android.Presenter;

import android.content.Context;
import android.net.Uri;

import com.pnb309.appgiaohang_android.Contract.IOrderOngoingDetailActivityContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Model.CustomerOrderModel;
import com.pnb309.appgiaohang_android.Model.ICustomerOrderModel;
import com.pnb309.appgiaohang_android.View.OrderOngoingDetailActivity;

public class OrderOngoingDetailActivityPresenter implements IOrderOngoingDetailActivityContract.Presenter {
    private ICustomerOrderModel customerOrderModel;
    private IOrderOngoingDetailActivityContract.View view;
    public OrderOngoingDetailActivityPresenter(Context context, IOrderOngoingDetailActivityContract.View view)
    {
        customerOrderModel = new CustomerOrderModel(context);
        this.view = view;
    }
    @Override
    public void OnUpdateCustomerOrderStatus(CustomerOrder customerOrder, Uri imageUri) {
        customerOrderModel.updateCustomerOrderStatus(customerOrder, imageUri, new ICustomerOrderModel.OnUpdateCustomerOrderListener() {
            @Override
            public void OnFinishhUpdateCustomerOrder(boolean isSuccess, Exception e) {
                if(e!=null)
                    e.printStackTrace();
                else
                    view.FinishUpdateCustomerOrder(isSuccess);
            }
        });
    }
}
