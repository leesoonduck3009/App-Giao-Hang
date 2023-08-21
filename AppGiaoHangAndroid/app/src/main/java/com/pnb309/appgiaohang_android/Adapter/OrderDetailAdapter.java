package com.pnb309.appgiaohang_android.Adapter;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.RecyclerView;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Entity.CustomerOrderDetail;
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.View.OrderOngoingDetailActivity;

import java.util.List;

public class OrderDetailAdapter extends RecyclerView.Adapter<OrderDetailAdapter.OrderDetailViewHolder>{
    private List<CustomerOrderDetail> customerOrderDetails;
    public OrderDetailAdapter(List<CustomerOrderDetail> customerOrderDetails)
    {
        this.customerOrderDetails = customerOrderDetails;
    }


    @NonNull
    @Override
    public OrderDetailAdapter.OrderDetailViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_customer_order_detail,parent,false);
        return new OrderDetailAdapter.OrderDetailViewHolder(view);
    }

    @SuppressLint("ResourceAsColor")
    @Override
    public void onBindViewHolder(@NonNull OrderDetailAdapter.OrderDetailViewHolder holder, int position) {
        CustomerOrderDetail customerOrderDetail = customerOrderDetails.get(position);
        holder.txtViewSTT.setText(String.valueOf(position+1));
        holder.txtViewSoLuong.setText(String.valueOf(customerOrderDetail.getQuantity()));
        holder.txtViewThanhTien.setText(String.valueOf(customerOrderDetail.getOrderDetailPrice()));
        holder.textViewTenSanPham.setText(String.valueOf(customerOrderDetail.getProduct().getProductName()));
    }

    @Override
    public int getItemCount() {
        return customerOrderDetails.size();
    }

    public static class OrderDetailViewHolder extends RecyclerView.ViewHolder{
        private TextView txtViewSTT;
        private TextView textViewTenSanPham;
        private TextView txtViewSoLuong;
        private TextView txtViewThanhTien;
        public OrderDetailViewHolder(@NonNull View itemView){
            super(itemView);
            txtViewSTT = itemView.findViewById(R.id.txtViewSTT);
            textViewTenSanPham = itemView.findViewById(R.id.textViewTenSanPham);
            txtViewSoLuong = itemView.findViewById(R.id.txtViewSoLuong);
            txtViewThanhTien = itemView.findViewById(R.id.txtViewThanhTien);

        }
    }
}
