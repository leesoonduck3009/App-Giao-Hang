package com.pnb309.appgiaohang_android.Entity;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class Employee {
    public Employee() {
        Accounts = new ArrayList<>();
        CustomerOrders = new ArrayList<>();
    }

    private long EmployeeId;
    private String EmployeeCode;
    private String EmployeeName;
    private Date DateJoin;
    private String PhoneNumber;
    private String IdentityNumber;
    private Date Birthday;

    private List<Account> Accounts;
    private List<CustomerOrder> CustomerOrders;

    public long getEmployeeId() {
        return EmployeeId;
    }

    public void setEmployeeId(long employeeId) {
        EmployeeId = employeeId;
    }

    public String getEmployeeCode() {
        return EmployeeCode;
    }

    public void setEmployeeCode(String employeeCode) {
        EmployeeCode = employeeCode;
    }

    public String getEmployeeName() {
        return EmployeeName;
    }

    public void setEmployeeName(String employeeName) {
        EmployeeName = employeeName;
    }

    public Date getDateJoin() {
        return DateJoin;
    }

    public void setDateJoin(Date dateJoin) {
        DateJoin = dateJoin;
    }

    public String getPhoneNumber() {
        return PhoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        PhoneNumber = phoneNumber;
    }

    public String getIdentityNumber() {
        return IdentityNumber;
    }

    public void setIdentityNumber(String identityNumber) {
        IdentityNumber = identityNumber;
    }

    public Date getBirthday() {
        return Birthday;
    }

    public void setBirthday(Date birthday) {
        Birthday = birthday;
    }

    public List<Account> getAccounts() {
        return Accounts;
    }

    public void setAccounts(List<Account> accounts) {
        Accounts = accounts;
    }

    public List<CustomerOrder> getCustomerOrders() {
        return CustomerOrders;
    }

    public void setCustomerOrders(List<CustomerOrder> customerOrders) {
        CustomerOrders = customerOrders;
    }
}

