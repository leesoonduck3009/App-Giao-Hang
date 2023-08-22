package com.pnb309.appgiaohang_android.Presenter;

import com.pnb309.appgiaohang_android.Contract.IMainActivityContract;
import com.pnb309.appgiaohang_android.Model.EmployeeModel;
import com.pnb309.appgiaohang_android.Model.IEmployeeModel;
import com.pnb309.appgiaohang_android.Ultilities.TokenVariable;

import org.json.JSONException;

public class MainActivityPresenter implements IMainActivityContract.Presenter {
    private IMainActivityContract.View view;
    private IEmployeeModel model;
    public MainActivityPresenter(IMainActivityContract.View view)
    {
        this.view = view;
        model = new EmployeeModel();
    }
    @Override
    public void LoadEmployee() throws JSONException {
        long EmployeeID = TokenVariable.getEmployeeID();
        model.getEmployee(EmployeeID,(responseInfo, e) -> {
            if(e==null)
                view.LoadedEmployee(true);
        });
    }
}
