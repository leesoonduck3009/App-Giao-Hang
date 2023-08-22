package com.pnb309.appgiaohang_android.Ultilities;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Entity.Account;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;
import com.pnb309.appgiaohang_android.Services.IAccountService;
import com.squareup.moshi.Moshi;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.Base64;
import java.util.Date;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.converter.moshi.MoshiConverterFactory;

public class TokenVariable {
    public static String token;
    public static LocalDateTime timeExpired;
    public static Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
            .create();
    private static final String BASE_URL = "http://192.168.1.118:8080/api/";
    public static void setTokenExpiredTime() throws JSONException {
        String[] chunks = token.split("\\.");
        Base64.Decoder decoder = null;
        String header;
        String payload;
        Date date = new Date();
        Byte[] bytes;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.O) {
            decoder = Base64.getUrlDecoder();
            header = new String(decoder.decode(chunks[0]));
            payload = new String(decoder.decode(chunks[1]));
            JSONObject jsonObject = new JSONObject(payload);
            long unixTimestamp = jsonObject.getLong("exp");
            Instant instant = Instant.ofEpochSecond(unixTimestamp);
            LocalDateTime dateTime = instant.atZone(ZoneId.systemDefault()).toLocalDateTime();
            timeExpired = dateTime;
            Log.d("payload",payload);
        }
    }
    public static long getEmployeeID() throws JSONException {
        String[] chunks = token.split("\\.");
        Base64.Decoder decoder = null;
        String header;
        String payload;
        Date date = new Date();
        Byte[] bytes;
        long employeeID = 0;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.O) {
            decoder = Base64.getUrlDecoder();
            header = new String(decoder.decode(chunks[0]));
            payload = new String(decoder.decode(chunks[1]));
            JSONObject jsonObject = new JSONObject(payload);
            employeeID = jsonObject.getLong("employeeID");
            return employeeID;
        }
        return employeeID;
    }
    public static Retrofit getRetrofitInstance() {
        OkHttpClient.Builder httpClient = new OkHttpClient.Builder();
        httpClient.addInterceptor(new Interceptor() {
            @Override
            public Response intercept(Chain chain) throws IOException {
                Request original = chain.request();

                // Thêm token vào header Authorization
                Request request = original.newBuilder()
                        .header("Authorization", "Bearer " + token)
                        .method(original.method(), original.body())
                        .build();
                return chain.proceed(request);
            }
        });

        return new Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .client(httpClient.build())
                .build();
    }
    public static Retrofit getRetrofitWithoutAuthorizeInstance() {

        return new Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();
    }
    public static Retrofit getRetrofitExcludeInstance() {
        OkHttpClient.Builder httpClient = new OkHttpClient.Builder();
        httpClient.addInterceptor(new Interceptor() {
            @Override
            public Response intercept(Chain chain) throws IOException {
                Request original = chain.request();

                // Thêm token vào header Authorization
                Request request = original.newBuilder()
                        .header("Authorization", "Bearer " + token)
                        .method(original.method(), original.body())
                        .build();

                return chain.proceed(request);
            }
        });

        return new Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(new GsonBuilder()
                        .excludeFieldsWithoutExposeAnnotation().create()))

                .client(httpClient.build())
                .build();
    }
    public static void checkTokenValid(PreferenceManager preferenceManager, OnFinishGetTokenListener listener) {
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.O) {
            LocalDateTime localDateTime = LocalDateTime.now();
            if (localDateTime.isAfter(timeExpired)) {
                Account account = new Account();
                account.setPassword(preferenceManager.getString(Constants.KEY_PASSWORD));
                account.setUserName(preferenceManager.getString(Constants.KEY_USER_NAME));
                IAccountService accountService = getRetrofitInstance().create(IAccountService.class);
                accountService.Login(account).enqueue(new Callback<ResponseInfo>() {
                    @Override
                    public void onResponse(Call<ResponseInfo> call, retrofit2.Response<ResponseInfo> response) {
                        ResponseInfo responseInfo = response.body();
                        token = responseInfo.getData().toString();
                        if(token.isEmpty())
                            listener.OnFinishGetToken(false,null);
                        else
                            listener.OnFinishGetToken(true,token);
                    }

                    @Override
                    public void onFailure(Call<ResponseInfo> call, Throwable t) {
                        t.printStackTrace();
                    }
                });
            }
        }
    }
        public interface OnFinishGetTokenListener{
            void OnFinishGetToken(boolean isSuccess,String token);
        }

}
