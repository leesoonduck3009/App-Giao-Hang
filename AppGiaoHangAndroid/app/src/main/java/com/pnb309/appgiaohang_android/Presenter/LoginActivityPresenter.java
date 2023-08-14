package com.pnb309.appgiaohang_android.Presenter;

import com.pnb309.appgiaohang_android.Contract.ILoginActivityContract;
import com.pnb309.appgiaohang_android.Model.AccountModel;
import com.pnb309.appgiaohang_android.Model.IAccountModel;

public class LoginActivityPresenter implements ILoginActivityContract.ILoginActivityPresenter {
    private ILoginActivityContract.ILoginActivityView view;
    private IAccountModel accountModel;
    public LoginActivityPresenter(ILoginActivityContract.ILoginActivityView view)
    {
        this.view = view;
        accountModel = new AccountModel();
    }
    @Override
    public void LoginButtonClicked(String username, String password) {
        accountModel.checkLogin(username,password,((token, e) -> {
            if(e==null) {
                if (token == null || token.isEmpty()) {
                    view.Login(false, null);
                } else {
                    view.Login(true, token);
                }
            }
            else
                e.printStackTrace();
        }));
    }
}
