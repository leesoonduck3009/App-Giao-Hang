package com.pnb309.appgiaohang_android.Entity;

import android.os.Bundle;
import android.os.Parcelable;

import java.io.Serializable;
import java.util.Date;

public class Account implements Serializable {
    private long AccountId;
    private String UserName;
    private String Password;
    private Date AccountCreateTime;
    private String Roles;
    private Long EmployeeId;
    private Employee Employee;

    // Getters and Setters
    public long getAccountId() {
        return AccountId;
    }

    public void setAccountId(long accountId) {
        AccountId = accountId;
    }

    public String getUserName() {
        return UserName;
    }

    public void setUserName(String userName) {
        UserName = userName;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public Date getAccountCreateTime() {
        return AccountCreateTime;
    }

    public void setAccountCreateTime(Date accountCreateTime) {
        AccountCreateTime = accountCreateTime;
    }

    public String getRoles() {
        return Roles;
    }

    public void setRoles(String roles) {
        Roles = roles;
    }

    public Long getEmployeeId() {
        return EmployeeId;
    }

    public void setEmployeeId(Long employeeId) {
        EmployeeId = employeeId;
    }

    public Employee getEmployee() {
        return Employee;
    }

    public void setEmployee(Employee employee) {
        Employee = employee;
    }
}

