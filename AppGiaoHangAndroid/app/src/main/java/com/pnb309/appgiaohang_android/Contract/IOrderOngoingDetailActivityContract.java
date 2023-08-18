package com.pnb309.appgiaohang_android.Contract;

import android.net.Uri;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;

import java.util.List;

public interface IOrderOngoingDetailActivityContract {
    interface View{
        void FinishUpdateCustomerOrder(boolean isSuccess);
    }
    interface Presenter{
        void OnUpdateCustomerOrderStatus(CustomerOrder customerOrder, Uri imageUri);
    }
}
