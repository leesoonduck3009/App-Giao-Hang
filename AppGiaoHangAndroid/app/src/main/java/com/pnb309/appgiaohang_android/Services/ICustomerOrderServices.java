package com.pnb309.appgiaohang_android.Services;

import com.pnb309.appgiaohang_android.Entity.ResponseInfo;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface ICustomerOrderServices {
    @GET("customer-order/employee-id/{id}")
    Call<ResponseInfo> getCustomerOrderByEmployeeID(@Path("id") long employeeID);
}
