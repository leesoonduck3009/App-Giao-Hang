package com.pnb309.appgiaohang_android.Contract;

public interface ILoginActivityContract {
    interface ILoginActivityPresenter{
        void LoginButtonClicked(String username, String password);
    }
    interface ILoginActivityView{
        void Login(boolean isSuccess, String token);
    }
}
