package com.pnb309.appgiaohang_android.Entity;

import java.util.ArrayList;
import java.util.List;

public class Product {
    public Product() {
        CustomerOrderDetails = new ArrayList<>();
    }
    private long ProductId;
    private String ProductName;
    private String Description;
    private Double Price;

    private List<CustomerOrderDetail> CustomerOrderDetails;

    public long getProductId() {
        return ProductId;
    }

    public void setProductId(long productId) {
        ProductId = productId;
    }

    public String getProductName() {
        return ProductName;
    }

    public void setProductName(String productName) {
        ProductName = productName;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public Double getPrice() {
        return Price;
    }

    public void setPrice(Double price) {
        Price = price;
    }

    public List<CustomerOrderDetail> getCustomerOrderDetails() {
        return CustomerOrderDetails;
    }

    public void setCustomerOrderDetails(List<CustomerOrderDetail> customerOrderDetails) {
        CustomerOrderDetails = customerOrderDetails;
    }
}

