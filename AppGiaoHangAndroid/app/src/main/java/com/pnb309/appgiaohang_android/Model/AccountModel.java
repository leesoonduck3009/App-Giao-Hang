package com.pnb309.appgiaohang_android.Model;

import android.os.Build;
import android.util.Log;
import android.widget.Toast;

import androidx.annotation.NonNull;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Entity.Account;
import com.pnb309.appgiaohang_android.Entity.Customer;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;
import com.pnb309.appgiaohang_android.Services.IAccountService;
import com.pnb309.appgiaohang_android.Services.ICustomerService;
import com.pnb309.appgiaohang_android.Services.IEmployeeService;
import com.pnb309.appgiaohang_android.Ultilities.TokenVariable;
import com.squareup.moshi.Moshi;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.converter.moshi.MoshiConverterFactory;

public class AccountModel implements IAccountModel{

    private IAccountService accountService;
    private ICustomerService customerService;
    private Retrofit retrofit;
    public AccountModel()
    {
        retrofit = TokenVariable.getRetrofitWithoutAuthorizeInstance();
        accountService = retrofit.create(IAccountService.class);
      //  customerService = retrofit.create(ICustomerService.class);
    }
    @Override
    public void checkLogin(String username, String password, OnLoginFinishedListener listener) {
        Account account = new Account();
        account.setPassword(password);
        account.setUserName(username);
        try {
            JsonObject jsonObject = new JsonObject();
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
                jsonObject.addProperty("UserName", username);
                jsonObject.addProperty("Password", password);
            }
            accountService.Login(account).enqueue(new Callback<ResponseInfo>() {
                @Override
                public void onResponse(Call<ResponseInfo> call, Response<ResponseInfo> response) {
                    if (response.isSuccessful() && response.code() == 200) {
                        ResponseInfo responseInfo = response.body();
                        assert responseInfo != null;
                        if(!responseInfo.getData().toString().isEmpty()) {
                            try {
                                TokenVariable.token = responseInfo.getData().toString();
                                TokenVariable.setTokenExpiredTime();
                            } catch (JSONException e) {
                                throw new RuntimeException(e);
                            }
                            listener.OnLoginListener(responseInfo.getData().toString(), null);
                        }
                    }
                }

                @Override
                public void onFailure(Call<ResponseInfo> call, @NonNull Throwable t) {
                    t.printStackTrace();
                }
            });
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

}

