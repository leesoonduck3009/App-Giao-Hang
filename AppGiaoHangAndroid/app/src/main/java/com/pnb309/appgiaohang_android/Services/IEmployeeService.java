package com.pnb309.appgiaohang_android.Services;

import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Entity.Employee;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;
public interface IEmployeeService {
    @PUT("employee/location/{id}")
    Call<ResponseInfo> updateEmployeeLocation(@Path("id") long id, @Body Employee employee);
    @GET("employee/{id}")
    Call<ResponseInfo> getEmployee(@Path("id") long employeeID);
}
