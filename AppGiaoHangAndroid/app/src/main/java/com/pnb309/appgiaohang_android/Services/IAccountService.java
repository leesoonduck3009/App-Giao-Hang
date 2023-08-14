package com.pnb309.appgiaohang_android.Services;

import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Entity.Account;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;
import com.pnb309.appgiaohang_android.Model.AccountModel;

import org.json.JSONObject;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface IAccountService {
    @POST("token")
    Call<ResponseInfo> Login(@Body Account account);
}
