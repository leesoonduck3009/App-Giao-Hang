package com.pnb309.appgiaohang_android.Entity;

import android.os.Parcel;
import android.os.Parcelable;

import androidx.annotation.NonNull;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class Customer implements Serializable {
    public Customer() {
        CustomerOrderInformations = new ArrayList<>();
    }

    private long CustomerId;
    private String CustomerCode;
    private Date DateCreate;
    private String CustomerName;
    private Date Birthday;
    private String CustomerRank;
    private List<CustomerOrderInformation> CustomerOrderInformations;

    public long getCustomerId() {
        return CustomerId;
    }

    public void setCustomerId(long customerId) {
        CustomerId = customerId;
    }

    public String getCustomerCode() {
        return CustomerCode;
    }

    public void setCustomerCode(String customerCode) {
        CustomerCode = customerCode;
    }

    public Date getDateCreate() {
        return DateCreate;
    }

    public void setDateCreate(Date dateCreate) {
        DateCreate = dateCreate;
    }

    public String getCustomerName() {
        return CustomerName;
    }

    public void setCustomerName(String customerName) {
        CustomerName = customerName;
    }

    public Date getBirthday() {
        return Birthday;
    }

    public void setBirthday(Date birthday) {
        Birthday = birthday;
    }

    public String getCustomerRank() {
        return CustomerRank;
    }

    public void setCustomerRank(String customerRank) {
        CustomerRank = customerRank;
    }

    public List<CustomerOrderInformation> getCustomerOrderInformations() {
        return CustomerOrderInformations;
    }

    public void setCustomerOrderInformations(List<CustomerOrderInformation> customerOrderInformations) {
        CustomerOrderInformations = customerOrderInformations;
    }

}

