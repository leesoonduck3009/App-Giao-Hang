package com.pnb309.appgiaohang_android.Services;

import android.Manifest;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.media.MediaPlayer;
import android.os.Bundle;
import android.os.IBinder;
import android.provider.Settings;
import android.util.Log;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.core.content.ContextCompat;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.pnb309.appgiaohang_android.Model.EmployeeModel;
import com.pnb309.appgiaohang_android.Model.IEmployeeModel;
import com.pnb309.appgiaohang_android.Ultilities.GlobalVariable;

import java.util.Timer;
import java.util.TimerTask;

public class GoogleMapService extends Service implements LocationListener{
    private LocationManager locationManager;
    private LocationListener locationListener;
    private MediaPlayer player;
    private Timer timer ;
    private IEmployeeModel employeeModel;
    TimerTask task;

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {

        if (ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
            LocationManager locationManager =
                    (LocationManager) this.getSystemService(Context.LOCATION_SERVICE);
            employeeModel = new EmployeeModel();
            locationListener = new LocationListener() {
                @Override
                public void onLocationChanged(Location location) {
                    // Lấy thông tin vị trí mới nhất
                    double latitude = location.getLatitude();
                    double longitude = location.getLongitude();
                    Log.i("longitude", String.valueOf(longitude));
                    Log.i("latitude",String.valueOf(latitude));
                    // Hiển thị vị trí trên bản đồ
                    GlobalVariable.CurrentEmployee.setLastLatitude(latitude);
                    GlobalVariable.CurrentEmployee.setLastLongitude(longitude);
                    employeeModel.updateLocation(GlobalVariable.CurrentEmployee);
                }

                @Override
                public void onStatusChanged(String provider, int status, Bundle extras) {
                }

                @Override
                public void onProviderEnabled(String provider) {
                }

                @Override
                public void onProviderDisabled(String provider) {
                }
            };
            locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 3000, 0, locationListener);
        }

        return Service.START_STICKY;
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
    }

    @Override
    public void onLocationChanged(@NonNull Location location) {

    }
}
