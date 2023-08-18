package com.pnb309.appgiaohang_android.Services;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface ICustomerOrderServices {
    @POST("customer-order/employee-id-OrderStatus")
    Call<ResponseInfo> getCustomerOrderByEmployeeIDAndStatus(@Body CustomerOrder customerOrder);
    @GET("customer-order/employee-id/{id}")
    Call<ResponseInfo> getCustomerOrderByEmployeeIDAndStatus(@Path("id") long id);
    @GET("customer-order/{orderID}")
    Call<ResponseInfo> getCustomerOrderByID(@Path("orderID") long orderID);
    @PUT("customer-order/{orderID}")
    Call<ResponseInfo> updateCustomerOrderByID(@Path("orderID") long orderID, @Body CustomerOrder customerOrder);
}
