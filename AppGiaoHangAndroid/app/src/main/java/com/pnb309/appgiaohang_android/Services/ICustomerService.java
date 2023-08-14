package com.pnb309.appgiaohang_android.Services;

import com.pnb309.appgiaohang_android.Entity.Customer;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface ICustomerService {
    @GET("customer")
    Call<ResponseInfo> getAllCustomer();
}
