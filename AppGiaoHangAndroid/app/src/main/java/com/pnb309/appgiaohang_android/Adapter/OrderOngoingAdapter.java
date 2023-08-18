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
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.View.OrderOngoingDetailActivity;

import java.util.List;

public class OrderOngoingAdapter extends RecyclerView.Adapter<OrderOngoingAdapter.OrderViewHolder>{
    private List<CustomerOrder> customerOrderList;
    private int orderType;
    private Fragment fragment;
    public OrderOngoingAdapter(List<CustomerOrder> customerOrderList, Fragment fragment)
    {
        this.customerOrderList = customerOrderList;
        this.fragment = fragment;
    }


    @NonNull
    @Override
    public OrderViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_order_ongoing_layout,parent,false);
        return new OrderViewHolder(view);
    }

    @SuppressLint("ResourceAsColor")
    @Override
    public void onBindViewHolder(@NonNull OrderViewHolder holder, int position) {
        CustomerOrder customerOrder = customerOrderList.get(position);
        holder.orderStatus.setText(customerOrder.getOrderStatus());
        holder.txtViewName.setText(customerOrder.getEmployee().getEmployeeName());
        holder.txtAddress.setText(customerOrder.getCustomerOrderInformation().getAddress());
        holder.itemView.setOnClickListener(view -> {
            Intent intent = new Intent(fragment.getActivity(), OrderOngoingDetailActivity.class);
            intent.putExtra("order",customerOrderList.get(position));
            fragment.startActivity(intent);
        });
    }

    @Override
    public int getItemCount() {
        return customerOrderList.size();
    }

    public static class OrderViewHolder extends RecyclerView.ViewHolder{
        private TextView orderStatus;
        private TextView txtAddress;
        private TextView txtViewName;
        public OrderViewHolder(@NonNull View itemView){
            super(itemView);
            orderStatus = itemView.findViewById(R.id.orderStatus);
            txtAddress = itemView.findViewById(R.id.txtAddress);
            txtViewName = itemView.findViewById(R.id.txtViewName);
        }
    }
}
