package com.pnb309.appgiaohang_android.View;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.EditText;
import android.widget.Toast;

import com.pnb309.appgiaohang_android.Contract.ILoginActivityContract;
import com.pnb309.appgiaohang_android.Presenter.LoginActivityPresenter;
import com.pnb309.appgiaohang_android.R;
import com.google.android.material.button.MaterialButton;
import com.pnb309.appgiaohang_android.Ultilities.Constants;
import com.pnb309.appgiaohang_android.Ultilities.PreferenceManager;
import com.pnb309.appgiaohang_android.Ultilities.TokenVariable;

public class LoginActivity extends AppCompatActivity implements ILoginActivityContract.ILoginActivityView {

    private EditText edtTenDangNhap;
    private EditText edtPassword;
    private MaterialButton btnDangNhap;
    private String username;
    private String password;
    private ILoginActivityContract.ILoginActivityPresenter presenter;
    private PreferenceManager preferenceManager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        preferenceManager = new PreferenceManager(getApplicationContext());
        presenter = new LoginActivityPresenter(this);
        if(preferenceManager.getString(Constants.KEY_USER_NAME)!=null && preferenceManager.getString(Constants.KEY_PASSWORD) !=null)
        {
            username = preferenceManager.getString(Constants.KEY_USER_NAME);
            password = preferenceManager.getString(Constants.KEY_PASSWORD);
            presenter.LoginButtonClicked(preferenceManager.getString(Constants.KEY_USER_NAME),preferenceManager.getString(Constants.KEY_PASSWORD));
        }
        edtTenDangNhap = findViewById(R.id.edtTenDangNhap);
        edtPassword = findViewById(R.id.edtPassword);
        btnDangNhap = findViewById(R.id.btnDangNhap);
        setListener();
    }
    private void setListener()
    {
        btnDangNhap.setOnClickListener(view -> {
             username = edtTenDangNhap.getText().toString();
             password = edtPassword.getText().toString();
            if(username.isEmpty() || password.isEmpty())
                Toast.makeText(this, "Vui lòng nhập đủ email và password", Toast.LENGTH_SHORT).show();
            else
                presenter.LoginButtonClicked(username,password);
        });
    }

    @Override
    public void Login(boolean isSuccess, String token) {
        if(isSuccess)
        {
            preferenceManager.putString(Constants.KEY_USER_NAME,username);
            preferenceManager.putString(Constants.KEY_PASSWORD,password);
            TokenVariable.token = token;
            Intent intent = new Intent(getApplicationContext(),MainActivity.class);
            startActivity(intent);
            finish();
        }
        else
            Toast.makeText(this, "Tài khoản hoặc mật khẩu không hợp lệ", Toast.LENGTH_SHORT).show();
    }
}