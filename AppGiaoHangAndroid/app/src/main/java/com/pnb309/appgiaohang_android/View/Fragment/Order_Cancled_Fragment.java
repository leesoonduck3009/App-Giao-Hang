package com.pnb309.appgiaohang_android.View.Fragment;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Adapter.OrderAdapter;
import com.pnb309.appgiaohang_android.Contract.IOrderCancelledContract;
import com.pnb309.appgiaohang_android.Contract.IOrderOngoingFragmentContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Presenter.OrderCancelledPresenter;
import com.pnb309.appgiaohang_android.Presenter.OrderOngoingFragmentPresenter;
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.Ultilities.DateTypeAdapter;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link Order_Cancled_Fragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class Order_Cancled_Fragment extends Fragment implements IOrderCancelledContract.View {

    private RecyclerView rcvOrder;
    private OrderAdapter orderAdapter;
    private List<CustomerOrder> customerOrderList;
    private IOrderCancelledContract.Presenter presenter;


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        customerOrderList = new ArrayList<>();
        presenter = new OrderCancelledPresenter(this);
    }

    @Override
    public void LoadOrderCancelledCustomer(List<CustomerOrder> customerOrderList) {
        try {
            Gson gson = new GsonBuilder().registerTypeAdapter(Date.class,new DateTypeAdapter()).create();

            for (int i = 0; i < customerOrderList.size(); i++) {
                JsonObject jsonObject = gson.toJsonTree(customerOrderList.get(i)).getAsJsonObject();

                CustomerOrder customerOrder = gson.fromJson(jsonObject,CustomerOrder.class);
                this.customerOrderList.add(customerOrder);
            }
            orderAdapter.notifyDataSetChanged();
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }
    private void loadListCustomerOrder()
    {
        presenter.OnLoadingOrderCancelledCustomer(1);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view =inflater.inflate(R.layout.fragment_order__cancled_, container, false);
        rcvOrder = view.findViewById(R.id.rcvOrder);
        GridLayoutManager gridLayoutManager = new GridLayoutManager(requireContext(),1);
        orderAdapter =  new OrderAdapter(customerOrderList,this);
        rcvOrder.setAdapter(orderAdapter);
        rcvOrder.setLayoutManager(gridLayoutManager);
        loadListCustomerOrder();
        return view;
    }
}