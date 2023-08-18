package com.pnb309.appgiaohang_android.View;

import androidx.activity.result.ActivityResultCallback;
import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.AppCompatButton;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.core.app.ActivityCompat;
import androidx.core.content.FileProvider;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.pnb309.appgiaohang_android.Contract.IOrderOngoingDetailActivityContract;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Presenter.OrderOngoingDetailActivityPresenter;
import com.pnb309.appgiaohang_android.R;
import com.pnb309.appgiaohang_android.Ultilities.MyDialog;

import java.io.File;
import java.io.IOException;
import java.text.DecimalFormat;
import java.text.NumberFormat;

public class OrderOngoingDetailActivity extends AppCompatActivity implements IOrderOngoingDetailActivityContract.View {
    private TextView txtDonHangID;
    private static final int CAMERA_PERMISSION_CODE = 1;
    private TextView tenKhachHang;
    private TextView DiaChi;
    private TextView txtSoDienThoai;
    private ImageButton btnMap;
    private ImageButton btnCall;
    private AppCompatButton OpenImageBtn;
    private ImageView imgAnhXuatHien;
    private Button btnRecamera;
    private ConstraintLayout activePictureLayout;
    private Button btnHoanThanhDon;
    private Button btnHuyDon;
    private TextView tongtienTxtView;
    private ActivityResultLauncher<Uri> takePictureLauncher;
    private Uri imageUri;
    private CustomerOrder customerOrder;
    private IOrderOngoingDetailActivityContract.Presenter presenter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        imageUri = null;
        setContentView(R.layout.activity_order_ongoing_detail);
        txtDonHangID = findViewById(R.id.txtDonHangID);
        tenKhachHang = findViewById(R.id.tenKhachHang);
        DiaChi = findViewById(R.id.DiaChi);
        txtSoDienThoai = findViewById(R.id.txtSoDienThoai);
        btnMap = findViewById(R.id.btnMap);
        btnCall = findViewById(R.id.btnCall);
        OpenImageBtn = findViewById(R.id.OpenImageBtn);
        imgAnhXuatHien = findViewById(R.id.imgAnhXuatHien);
        btnRecamera = findViewById(R.id.btnRecamera);
        activePictureLayout = findViewById(R.id.activePictureLayout);
        btnHoanThanhDon = findViewById(R.id.btnHoanThanhDon);
        btnHuyDon = findViewById(R.id.btnHuyDon);
        tongtienTxtView = findViewById(R.id.tongtienTxtView);
        presenter = new OrderOngoingDetailActivityPresenter(getApplicationContext(),this);
        setListener();
        registerPictureLauncher();
        loadDataOrder();
    }
    private Uri createUri(){
        File imageFile =new File(getApplicationContext().getFilesDir(),"camera_photo.jpg");
        return FileProvider.getUriForFile(
                getApplicationContext(), "com.pnb309.appgiaohang_android.fileProvider"
                ,imageFile
        );
    }
    private void setListener()
    {
        OpenImageBtn.setOnClickListener(view -> {
            imageUri = createUri();
            checkCameraPermissionAndOpenCamera();
        });
        btnRecamera.setOnClickListener(view -> {
            checkCameraPermissionAndOpenCamera();
        });
        btnCall.setOnClickListener(view -> {
            dialPhoneNumber(txtSoDienThoai.getText().toString());
        });
        btnHoanThanhDon.setOnClickListener(view -> {
            if(imageUri==null)
            {
                Toast.makeText(this, "Vui lòng chụp ảnh xác nhận", Toast.LENGTH_SHORT).show();
            }
            else {
                MyDialog.showDialog(this, "Bạn có chắc chắn với thông tin trên chưa", MyDialog.DialogType.YES_NO, new MyDialog.DialogClickListener() {
                    @Override
                    public void onYesClick() {
                        customerOrder.setOrderStatus(CustomerOrder.ORDER_FINISH);
                        presenter.OnUpdateCustomerOrderStatus(customerOrder,imageUri);
                    }

                    @Override
                    public void onNoClick() {

                    }

                    @Override
                    public void onOkClick() {

                    }
                });

            }
        });
        btnHuyDon.setOnClickListener(view -> {
            MyDialog.showDialog(this, "Bạn có chắc chắn với thông tin trên chưa", MyDialog.DialogType.YES_NO, new MyDialog.DialogClickListener() {
                @Override
                public void onYesClick() {
                    customerOrder.setOrderStatus(CustomerOrder.ORDER_CANCLED);
                    presenter.OnUpdateCustomerOrderStatus(customerOrder,null);
                }

                @Override
                public void onNoClick() {

                }
                @Override
                public void onOkClick() {

                }
            });
        });
    }
    private void registerPictureLauncher(){
        takePictureLauncher = registerForActivityResult(
                new ActivityResultContracts.TakePicture(),
                new ActivityResultCallback<Boolean>() {
                    @Override
                    public void onActivityResult(Boolean result) {
                        try{
                            if(result)
                            {
                                OpenImageBtn.setVisibility(View.GONE);
                                activePictureLayout.setVisibility(View.VISIBLE);
                                imgAnhXuatHien.setImageURI(null);
                                imgAnhXuatHien.setImageURI(imageUri);
                            }
                        }
                        catch (Exception e)
                        {
                            e.printStackTrace();
                        }
                    }
                }
        );
    }
    private void checkCameraPermissionAndOpenCamera(){
        if(ActivityCompat.checkSelfPermission(getApplicationContext(), Manifest.permission.CAMERA)!= PackageManager.PERMISSION_GRANTED){
            ActivityCompat.requestPermissions(OrderOngoingDetailActivity.this,new String[]{Manifest.permission.CAMERA},CAMERA_PERMISSION_CODE);
        }
        else {
            takePictureLauncher.launch(imageUri);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        if(requestCode == CAMERA_PERMISSION_CODE){
           if( grantResults.length >0 && grantResults[0] == PackageManager.PERMISSION_GRANTED  )
           {
               takePictureLauncher.launch(imageUri);
           }
           else
           {
               Toast.makeText(this, "Vui lòng cho phép quyền truy cập camera", Toast.LENGTH_SHORT).show();
           }
        }
    }
    private void dialPhoneNumber(String phoneNumber) {
        Intent intent = new Intent(Intent.ACTION_DIAL);
        intent.setData(Uri.parse("tel:" + phoneNumber));
        if (intent.resolveActivity(getPackageManager()) != null) {
            startActivity(intent);
        }
    }
    private void loadDataOrder()
    {
        Intent intent = getIntent();
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
            customerOrder = intent.getSerializableExtra("order", CustomerOrder.class);
        }
        txtSoDienThoai.setText(customerOrder.getCustomerOrderInformation().getPhoneNumber());
        DiaChi.setText(customerOrder.getCustomerOrderInformation().getAddress());
        NumberFormat numberFormat = NumberFormat.getNumberInstance();
        numberFormat.setGroupingUsed(true); // Bật chế độ hiển thị hàng nghìn
        numberFormat.setMaximumFractionDigits(0); // Số lượng chữ số phần thập phân
        tongtienTxtView.setText(numberFormat.format(customerOrder.getTotalPrice()));
    }
    @Override
    public void FinishUpdateCustomerOrder(boolean isSuccess) {
        if(isSuccess){
            Toast.makeText(getApplicationContext(), "Hoàn thành đơn hàng", Toast.LENGTH_SHORT).show();
            finish();
        }
        else
            Toast.makeText(this, "Cập nhật đơn hàng thất bại", Toast.LENGTH_SHORT).show();
    }
}