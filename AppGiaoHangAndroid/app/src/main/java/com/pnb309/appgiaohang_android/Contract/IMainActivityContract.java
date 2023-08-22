package com.pnb309.appgiaohang_android.Contract;

import org.json.JSONException;

public interface IMainActivityContract {
    interface View{
        void LoadedEmployee(boolean isSuccess);
    }
    interface Presenter{
        void LoadEmployee() throws JSONException;
    }
}
