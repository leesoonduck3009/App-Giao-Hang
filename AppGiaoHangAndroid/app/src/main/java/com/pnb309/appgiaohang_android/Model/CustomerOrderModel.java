package com.pnb309.appgiaohang_android.Model;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;
import com.pnb309.appgiaohang_android.Services.ICustomerOrderServices;
import com.pnb309.appgiaohang_android.Ultilities.TokenVariable;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class CustomerOrderModel implements ICustomerOrderModel {
    private ICustomerOrderServices customerOrderServices;
    private Retrofit retrofit;
    public CustomerOrderModel()
    {
        retrofit = TokenVariable.getRetrofitInstance();
        customerOrderServices = retrofit.create(ICustomerOrderServices.class);
    }

    @Override
    public void LoadCustomerOrderByEmployeeID(long id, OnLoadCustomerOrderByEmployeeIDListener listener) {
        customerOrderServices.getCustomerOrderByEmployeeID(id).enqueue(new Callback<ResponseInfo>() {
            @Override
            public void onResponse(Call<ResponseInfo> call, Response<ResponseInfo> response) {
                if(response.isSuccessful() && response.code() == 200)
                {
                    ResponseInfo responseInfo = response.body();
                    if(responseInfo.getStatusCode() == 200) {
                        listener.OnFinishLoadCustomerOrderByEmployeeID((List<CustomerOrder>) responseInfo.getData());
                    }
                    else
                        listener.OnFinishLoadCustomerOrderByEmployeeID(null);
                }
            }
            @Override
            public void onFailure(Call<ResponseInfo> call, Throwable t) {
                t.printStackTrace();
            }
        });
    }
}
