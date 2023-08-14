package com.pnb309.appgiaohang_android.View;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentManager;

import android.os.Bundle;

import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.View.Fragment.OrderFragment;


public class MainActivity extends AppCompatActivity {
    private FragmentManager fragmentManager;
    private OrderFragment orderFragment;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        orderFragment = new OrderFragment();
        fragmentManager = getSupportFragmentManager();
        fragmentManager.beginTransaction()
                .add(R.id.FrameLayoutMain,orderFragment, "orderMenu").commit();
        // Ẩn tất cả các fragment, chỉ hiển thị fragment home ban đầu
        fragmentManager.beginTransaction()
                .show(orderFragment)
                .commit();
    }
}