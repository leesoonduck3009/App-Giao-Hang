package com.pnb309.appgiaohang_android.View.Fragment;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ProgressBar;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Adapter.OrderOngoingAdapter;
import com.pnb309.appgiaohang_android.Contract.IOrderOngoingFragmentContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Presenter.OrderOngoingFragmentPresenter;
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.Ultilities.DateTypeAdapter;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link Order_Ongoing_Fragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class Order_Ongoing_Fragment extends Fragment implements IOrderOngoingFragmentContract.View {

    private RecyclerView rcvOrder;
    private OrderOngoingAdapter orderOngoingAdapter;
    private List<CustomerOrder> customerOrderList;
    private IOrderOngoingFragmentContract.Presenter presenter;
    private ProgressBar progressBar;
    private SwipeRefreshLayout swipeRefreshLayoutOrderOnGoing;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        customerOrderList = new ArrayList<>();
        presenter = new OrderOngoingFragmentPresenter(this,getContext());
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_order__ongoing_, container, false);
        rcvOrder = view.findViewById(R.id.rcvOrder);
        GridLayoutManager gridLayoutManager = new GridLayoutManager(requireContext(),1);
        progressBar = view.findViewById(R.id.progressBar);
        swipeRefreshLayoutOrderOnGoing = view.findViewById(R.id.swipeRefreshLayoutOrderOnGoing);
        orderOngoingAdapter =  new OrderOngoingAdapter(customerOrderList,this);
        rcvOrder.setAdapter(orderOngoingAdapter);
        rcvOrder.setLayoutManager(gridLayoutManager);
        loadListCustomerOrder();
        setListener();
        return view;
    }
    void setListener()
    {
        swipeRefreshLayoutOrderOnGoing.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                loadListCustomerOrder();
                swipeRefreshLayoutOrderOnGoing.setRefreshing(false);
            }
        });
    }
    @Override
    public void LoadOrderCustomer(List<CustomerOrder> customerOrderList) {
        try {
            this.customerOrderList.clear();
            Gson gson = new GsonBuilder().registerTypeAdapter(Date.class,new DateTypeAdapter()).create();
            for (int i = 0; i < customerOrderList.size(); i++) {
                JsonObject jsonObject = gson.toJsonTree(customerOrderList.get(i)).getAsJsonObject();

                CustomerOrder customerOrder = gson.fromJson(jsonObject,CustomerOrder.class);
                this.customerOrderList.add(customerOrder);
            }
            orderOngoingAdapter.notifyDataSetChanged();
            progressBar.setVisibility(View.GONE);
            rcvOrder.setVisibility(View.VISIBLE);
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }



    private void loadListCustomerOrder()
    {
        progressBar.setVisibility(View.VISIBLE);
        rcvOrder.setVisibility(View.GONE);
        presenter.OnLoadingOrderCustomer(1);
    }
}