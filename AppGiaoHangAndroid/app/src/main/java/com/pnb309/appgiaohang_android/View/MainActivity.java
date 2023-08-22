package com.pnb309.appgiaohang_android.View;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentManager;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.pnb309.appgiaohang_android.Contract.IMainActivityContract;
import com.pnb309.appgiaohang_android.Presenter.MainActivityPresenter;
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.Services.GoogleMapService;
import com.pnb309.appgiaohang_android.View.Fragment.OrderFragment;

import org.json.JSONException;


public class MainActivity extends AppCompatActivity implements IMainActivityContract.View {
    private FragmentManager fragmentManager;
    private OrderFragment orderFragment;
    private IMainActivityContract.Presenter presenter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        orderFragment = new OrderFragment();
        presenter = new MainActivityPresenter(this);
        fragmentManager = getSupportFragmentManager();
        fragmentManager.beginTransaction()
                .add(R.id.FrameLayoutMain,orderFragment, "orderMenu").commit();
        // Ẩn tất cả các fragment, chỉ hiển thị fragment home ban đầu
        fragmentManager.beginTransaction()
                .show(orderFragment)
                .commit();
        try {
            presenter.LoadEmployee();
        } catch (JSONException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public void LoadedEmployee(boolean isSuccess) {
        startService(new Intent(this, GoogleMapService.class));
    }
}