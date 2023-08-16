package com.pnb309.appgiaohang_android.Entity;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class CustomerOrder {
    public static final String ORDER_ONGOING = "Ðang giao";
    public static final String ORDER_CANCLED = "Đã hủy";
    public static final String ORDER_FINISH = "Đã hoàn thành";
    public CustomerOrder() {
        CustomerOrderDetails = new ArrayList<>();
    }

    private long CustomerOrderId;
    private Date OrderDate;
    private Long CustomerOrderInformationId;
    private Long EmployeeId;
    private Double TotalPrice;
    private String OrderStatus;
    private CustomerOrderInformation CustomerOrderInformation;
    private Employee Employee;
    private List<CustomerOrderDetail> CustomerOrderDetails;

    public long getCustomerOrderId() {
        return CustomerOrderId;
    }

    public void setCustomerOrderId(long customerOrderId) {
        CustomerOrderId = customerOrderId;
    }

    public Date getOrderDate() {
        return OrderDate;
    }

    public void setOrderDate(Date orderDate) {
        OrderDate = orderDate;
    }

    public Long getCustomerOrderInformationId() {
        return CustomerOrderInformationId;
    }

    public void setCustomerOrderInformationId(Long customerOrderInformationId) {
        CustomerOrderInformationId = customerOrderInformationId;
    }

    public Long getEmployeeId() {
        return EmployeeId;
    }

    public void setEmployeeId(Long employeeId) {
        EmployeeId = employeeId;
    }

    public Double getTotalPrice() {
        return TotalPrice;
    }

    public void setTotalPrice(Double totalPrice) {
        TotalPrice = totalPrice;
    }

    public String getOrderStatus() {
        return OrderStatus;
    }

    public void setOrderStatus(String orderStatus) {
        OrderStatus = orderStatus;
    }

    public com.pnb309.appgiaohang_android.Entity.CustomerOrderInformation getCustomerOrderInformation() {
        return CustomerOrderInformation;
    }

    public void setCustomerOrderInformation(com.pnb309.appgiaohang_android.Entity.CustomerOrderInformation customerOrderInformation) {
        CustomerOrderInformation = customerOrderInformation;
    }

    public com.pnb309.appgiaohang_android.Entity.Employee getEmployee() {
        return Employee;
    }

    public void setEmployee(com.pnb309.appgiaohang_android.Entity.Employee employee) {
        Employee = employee;
    }

    public List<CustomerOrderDetail> getCustomerOrderDetails() {
        return CustomerOrderDetails;
    }

    public void setCustomerOrderDetails(List<CustomerOrderDetail> customerOrderDetails) {
        CustomerOrderDetails = customerOrderDetails;
    }
}

