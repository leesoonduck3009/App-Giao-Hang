package com.pnb309.appgiaohang_android.View.Fragment;

import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.viewpager2.adapter.FragmentStateAdapter;
import androidx.viewpager2.widget.ViewPager2;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;

import com.google.android.material.bottomnavigation.BottomNavigationMenuView;
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.navigation.NavigationBarView;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Adapter.OrderAdapter;
import com.pnb309.appgiaohang_android.Contract.IOrderOngoingFragmentContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.Ultilities.DateTypeAdapter;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;

public class OrderFragment extends Fragment  {
    private IOrderOngoingFragmentContract.Presenter presenter;
    private FragmentManager fragmentManager;
    private Order_Ongoing_Fragment order_ongoing_fragment;
    private ViewPager2 viewPager;
    private List<Fragment> listOrderFragment;
    private FragmentStateAdapter fragmentStateAdapter;
    private int currentSelectedItem = 0;

    private BottomNavigationView navOrder;
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_order, container, false);
        viewPager = view.findViewById(R.id.frameLayoutOrder);
        navOrder = view.findViewById(R.id.navOrder);
        fragmentManager = getChildFragmentManager();
        listOrderFragment = new ArrayList<>();
        listOrderFragment.add(new Order_Ongoing_Fragment());
        listOrderFragment.add(new Order_Finish_Fragment());
        listOrderFragment.add(new Order_Cancled_Fragment());
        fragmentStateAdapter = new FragmentStateAdapter(this) {
            @NonNull
            @Override
            public Fragment createFragment(int position) {
                return listOrderFragment.get(position);
            }

            @Override
            public int getItemCount() {
                return listOrderFragment.size();
            }
        };
        viewPager.setAdapter(fragmentStateAdapter);
        navOrder.setOnItemSelectedListener(new NavigationBarView.OnItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                int itemId = item.getItemId();
                if (itemId == R.id.orderOnGoingMenu && currentSelectedItem != 0) {
                    viewPager.setCurrentItem(0);
                    currentSelectedItem = 0;
                }
                else if(itemId == R.id.orderFinishMenu && currentSelectedItem !=1)
                {
                    viewPager.setCurrentItem(1);
                    currentSelectedItem = 1;
                }
                else if(itemId == R.id.orderCancledMenu && currentSelectedItem !=2)
                {
                    viewPager.setCurrentItem(2);
                    currentSelectedItem = 2;
                }
                return true;
            }
        });
        viewPager.registerOnPageChangeCallback(new ViewPager2.OnPageChangeCallback() {
            @Override
            public void onPageSelected(int position) {
                navOrder.getMenu().getItem(position).setChecked(true);
                currentSelectedItem = position;
            }
        });
        return view;
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {



    }



}