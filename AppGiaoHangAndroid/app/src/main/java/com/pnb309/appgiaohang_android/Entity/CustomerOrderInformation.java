package com.pnb309.appgiaohang_android.Entity;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class CustomerOrderInformation implements Serializable {
    public CustomerOrderInformation() {
        CustomerOrders = new ArrayList<>();
    }

    private long CustomerOrderInformationId;
    private Long CustomerId;
    private String PhoneNumber;
    private String Address;

    private Customer Customer;
    private List<CustomerOrder> CustomerOrders;

    public long getCustomerOrderInformationId() {
        return CustomerOrderInformationId;
    }

    public void setCustomerOrderInformationId(long customerOrderInformationId) {
        CustomerOrderInformationId = customerOrderInformationId;
    }

    public Long getCustomerId() {
        return CustomerId;
    }

    public void setCustomerId(Long customerId) {
        CustomerId = customerId;
    }

    public String getPhoneNumber() {
        return PhoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        PhoneNumber = phoneNumber;
    }

    public String getAddress() {
        return Address;
    }

    public void setAddress(String address) {
        Address = address;
    }

    public com.pnb309.appgiaohang_android.Entity.Customer getCustomer() {
        return Customer;
    }

    public void setCustomer(com.pnb309.appgiaohang_android.Entity.Customer customer) {
        Customer = customer;
    }

    public List<CustomerOrder> getCustomerOrders() {
        return CustomerOrders;
    }

    public void setCustomerOrders(List<CustomerOrder> customerOrders) {
        CustomerOrders = customerOrders;
    }
}
