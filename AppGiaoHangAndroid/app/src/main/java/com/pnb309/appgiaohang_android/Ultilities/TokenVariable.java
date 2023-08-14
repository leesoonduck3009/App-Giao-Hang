package com.pnb309.appgiaohang_android.Ultilities;

import android.util.Log;

import com.squareup.moshi.Moshi;

import java.io.IOException;
import java.util.Base64;
import java.util.Date;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.converter.moshi.MoshiConverterFactory;

public class TokenVariable {
    public static String token;
    private static final String BASE_URL = "http://192.168.1.115:8080/api/";
    public static Date getTokenExpiredTime(String token)
    {
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
            Log.d("payload",payload);
        }
        return date;
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
                .addConverterFactory(MoshiConverterFactory.create())
                .client(httpClient.build())
                .build();
    }
}
