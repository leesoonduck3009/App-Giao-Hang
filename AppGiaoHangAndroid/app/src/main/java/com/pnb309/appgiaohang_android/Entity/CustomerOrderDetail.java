package com.pnb309.appgiaohang_android.Entity;

import java.io.Serializable;

public class CustomerOrderDetail implements Serializable {
    private long CustomerOrderDetailId;
    private Long ProductId;
    private Long CustomerOrderId;
    private Integer Quantity;
    private Double OrderDetailPrice;

    private CustomerOrder CustomerOrder;
    private Product Product;

    public long getCustomerOrderDetailId() {
        return CustomerOrderDetailId;
    }

    public void setCustomerOrderDetailId(long customerOrderDetailId) {
        CustomerOrderDetailId = customerOrderDetailId;
    }

    public Long getProductId() {
        return ProductId;
    }

    public void setProductId(Long productId) {
        ProductId = productId;
    }

    public Long getCustomerOrderId() {
        return CustomerOrderId;
    }

    public void setCustomerOrderId(Long customerOrderId) {
        CustomerOrderId = customerOrderId;
    }

    public Integer getQuantity() {
        return Quantity;
    }

    public void setQuantity(Integer quantity) {
        Quantity = quantity;
    }

    public Double getOrderDetailPrice() {
        return OrderDetailPrice;
    }

    public void setOrderDetailPrice(Double orderDetailPrice) {
        OrderDetailPrice = orderDetailPrice;
    }

    public com.pnb309.appgiaohang_android.Entity.CustomerOrder getCustomerOrder() {
        return CustomerOrder;
    }

    public void setCustomerOrder(com.pnb309.appgiaohang_android.Entity.CustomerOrder customerOrder) {
        CustomerOrder = customerOrder;
    }

    public com.pnb309.appgiaohang_android.Entity.Product getProduct() {
        return Product;
    }

    public void setProduct(com.pnb309.appgiaohang_android.Entity.Product product) {
        Product = product;
    }
}

