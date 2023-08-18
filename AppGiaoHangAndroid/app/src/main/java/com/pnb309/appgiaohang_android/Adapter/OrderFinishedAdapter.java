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

public class OrderFinishedAdapter  extends RecyclerView.Adapter<OrderFinishedAdapter.OrderFinishedViewHolder>{
    private List<CustomerOrder> customerOrderList;
    private Fragment fragment;
    public OrderFinishedAdapter(List<CustomerOrder> customerOrderList, Fragment fragment)
    {
        this.customerOrderList = customerOrderList;
        this.fragment = fragment;
    }


    @NonNull
    @Override
    public OrderFinishedAdapter.OrderFinishedViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_order_finish_layout,parent,false);
        return new OrderFinishedViewHolder(view);
    }

    @SuppressLint("ResourceAsColor")
    @Override
    public void onBindViewHolder(@NonNull OrderFinishedAdapter.OrderFinishedViewHolder holder, int position) {
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

    public static class OrderFinishedViewHolder extends RecyclerView.ViewHolder{
        private TextView orderStatus;
        private TextView txtAddress;
        private TextView txtViewName;
        public OrderFinishedViewHolder(@NonNull View itemView){
            super(itemView);
            orderStatus = itemView.findViewById(R.id.orderStatus);
            txtAddress = itemView.findViewById(R.id.txtAddress);
            txtViewName = itemView.findViewById(R.id.txtViewName);
        }
    }
}