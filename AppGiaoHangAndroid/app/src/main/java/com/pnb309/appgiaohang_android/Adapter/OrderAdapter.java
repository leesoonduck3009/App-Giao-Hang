package com.pnb309.appgiaohang_android.Adapter;

import android.annotation.SuppressLint;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.R;

import java.util.List;

public class OrderAdapter extends RecyclerView.Adapter<OrderAdapter.OrderViewHolder>{

    private List<CustomerOrder> customerOrderList;
    public OrderAdapter(List<CustomerOrder> customerOrderList)
    {
        this.customerOrderList = customerOrderList;
    }


    @NonNull
    @Override
    public OrderViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_order_layout,parent,false);
        return new OrderViewHolder(view);
    }

    @SuppressLint("ResourceAsColor")
    @Override
    public void onBindViewHolder(@NonNull OrderViewHolder holder, int position) {
        CustomerOrder customerOrder = customerOrderList.get(position);
        holder.orderStatus.setText(customerOrder.getOrderStatus());
        holder.txtViewName.setText(customerOrder.getEmployee().getEmployeeName());
        holder.txtAddress.setText(customerOrder.getCustomerOrderInformation().getAddress());
    }

    @Override
    public int getItemCount() {
        return customerOrderList.size();
    }

    public class OrderViewHolder extends RecyclerView.ViewHolder{
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
