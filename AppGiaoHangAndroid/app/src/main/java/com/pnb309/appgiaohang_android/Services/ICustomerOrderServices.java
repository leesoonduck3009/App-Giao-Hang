package com.pnb309.appgiaohang_android.Services;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ICustomerOrderServices {
    @POST("employee-id-OrderStatus")
    Call<ResponseInfo> getCustomerOrderByEmployeeIDAndStatus(@Body CustomerOrder customerOrder);
}
