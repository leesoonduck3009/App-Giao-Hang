package com.pnb309.appgiaohang_android.Model;

import com.pnb309.appgiaohang_android.Entity.Employee;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;

public interface IEmployeeModel {
    void updateLocation(Employee employee);
    void getEmployee(long employeeID, OnFinishedGetEmployeeListener listener);
    interface OnFinishedGetEmployeeListener{
        void FinishGetEmployee(ResponseInfo responseInfo, Exception e);
    }
}
