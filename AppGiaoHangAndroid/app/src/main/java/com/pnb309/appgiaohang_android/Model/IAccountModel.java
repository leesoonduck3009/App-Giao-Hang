package com.pnb309.appgiaohang_android.Model;

public interface IAccountModel {
    void checkLogin(String username, String password, OnLoginFinishedListener listener);
    interface OnLoginFinishedListener{
        void OnLoginListener(String token, Exception e);
    }
}
