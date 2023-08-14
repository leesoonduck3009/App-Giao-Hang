package com.pnb309.appgiaohang_android.View.Fragment;

import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.pnb309.appgiaohang_android.Adapter.OrderAdapter;
import com.pnb309.appgiaohang_android.Contract.IOrderFragmentContract;
import com.pnb309.appgiaohang_android.Entity.Customer;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Presenter.OrderFragmentPresenter;
import com.pnb309.appgiaohang_android.R;

import java.util.ArrayList;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link OrderFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class OrderFragment extends Fragment implements IOrderFragmentContract.View {
    private RecyclerView rcvOrder;
    private IOrderFragmentContract.Presenter presenter;
    private List<CustomerOrder> customerOrderList;
    private OrderAdapter orderAdapter;
    public OrderFragment() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        customerOrderList = new ArrayList<>();
        presenter = new OrderFragmentPresenter(this);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_order, container, false);
        rcvOrder = view.findViewById(R.id.rcvOrder);
        orderAdapter =  new OrderAdapter(customerOrderList);
        rcvOrder.setAdapter(orderAdapter);
        loadListCustomerOrder();
        return view;
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {

    }

    private void loadListCustomerOrder()
    {
        presenter.OnLoadingOrderCustomer(1);
    }

    @Override
    public void LoadOrderCustomer(List<CustomerOrder> customerOrderList) {
        for (int i = 0; i < customerOrderList.size(); i++) {
            this.customerOrderList.add(customerOrderList.get(i));
        }
    }
}