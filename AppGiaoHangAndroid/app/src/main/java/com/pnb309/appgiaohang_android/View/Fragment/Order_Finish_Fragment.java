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
import com.pnb309.appgiaohang_android.Adapter.OrderFinishedAdapter;
import com.pnb309.appgiaohang_android.Adapter.OrderOngoingAdapter;
import com.pnb309.appgiaohang_android.Contract.IOrderFinishedContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Presenter.OrderFinishedPresenter;
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.Ultilities.DateTypeAdapter;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link Order_Finish_Fragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class Order_Finish_Fragment extends Fragment implements IOrderFinishedContract.View {
    private RecyclerView rcvOrder;
    private OrderFinishedAdapter orderFinishedAdapter;
    private List<CustomerOrder> customerOrderList;
    private IOrderFinishedContract.Presenter presenter;
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        customerOrderList = new ArrayList<>();
        presenter = new OrderFinishedPresenter(this,getContext());
    }
    @Override
    public void LoadOrderFinishedCustomer(List<CustomerOrder> customerOrderList) {
        try {
            Gson gson = new GsonBuilder().registerTypeAdapter(Date.class,new DateTypeAdapter()).create();
            for (int i = 0; i < customerOrderList.size(); i++) {
                JsonObject jsonObject = gson.toJsonTree(customerOrderList.get(i)).getAsJsonObject();

                CustomerOrder customerOrder = gson.fromJson(jsonObject,CustomerOrder.class);
                this.customerOrderList.add(customerOrder);
            }
            orderFinishedAdapter.notifyDataSetChanged();
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }
    private void loadListCustomerOrder()
    {
        customerOrderList.clear();
        presenter.OnLoadingOrderFinishedCustomer(1);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view =inflater.inflate(R.layout.fragment_order__finish_, container, false);
        rcvOrder = view.findViewById(R.id.rcvOrder);
        GridLayoutManager gridLayoutManager = new GridLayoutManager(requireContext(),1);
        orderFinishedAdapter =  new OrderFinishedAdapter(customerOrderList,this);
        rcvOrder.setAdapter(orderFinishedAdapter);
        rcvOrder.setLayoutManager(gridLayoutManager);
        loadListCustomerOrder();
        return view;
    }
}