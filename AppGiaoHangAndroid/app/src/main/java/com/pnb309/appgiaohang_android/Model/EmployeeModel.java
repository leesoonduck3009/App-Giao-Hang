package com.pnb309.appgiaohang_android.Model;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Entity.Employee;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;
import com.pnb309.appgiaohang_android.Services.IEmployeeService;
import com.pnb309.appgiaohang_android.Ultilities.DateTypeAdapter;
import com.pnb309.appgiaohang_android.Ultilities.GlobalVariable;
import com.pnb309.appgiaohang_android.Ultilities.TokenVariable;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;

public class EmployeeModel implements IEmployeeModel{
    private IEmployeeService employeeService;
    private Retrofit retrofit;
    public EmployeeModel()
    {
        retrofit = TokenVariable.getRetrofitInstance();
        employeeService = retrofit.create(IEmployeeService.class);
    }

    @Override
    public void updateLocation(Employee employee) {
        ResponseInfo responseInfo;
        try{
/*            String json = new GsonBuilder().registerTypeAdapter(Date.class,new DateTypeAdapter())
                    .excludeFieldsWithoutExposeAnnotation().create().toJson(employee);
            JsonObject jsonObject = new JsonParser().parse(json).getAsJsonObject();*/
           employeeService.updateEmployeeLocation(employee.getEmployeeId(),employee).enqueue(new Callback<ResponseInfo>() {
               @Override
               public void onResponse(Call<ResponseInfo> call, Response<ResponseInfo> response) {
                   if(response.isSuccessful() && response.code() == 200)
                   {
                       Log.d("DetailSend","Success");
                   }
                   else
                       Log.d("DetailSend","Fail");

               }
               @Override
               public void onFailure(Call<ResponseInfo> call, Throwable t) {
                   t.printStackTrace();
               }
           });
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    @Override
    public void getEmployee(long employeeID, OnFinishedGetEmployeeListener listener) {
        employeeService.getEmployee(employeeID).enqueue(new Callback<ResponseInfo>() {
            @Override
            public void onResponse(Call<ResponseInfo> call, Response<ResponseInfo> response) {
                ResponseInfo responseInfo = response.body();
                List<Employee> employees = (List<Employee>) responseInfo.getData();
                Gson gson = new GsonBuilder().registerTypeAdapter(Date.class,new DateTypeAdapter()).create();
                JsonObject jsonObject = gson.toJsonTree(employees.get(0)).getAsJsonObject();

                GlobalVariable.CurrentEmployee = gson.fromJson(jsonObject,Employee.class);
                listener.FinishGetEmployee(responseInfo,null);
            }

            @Override
            public void onFailure(Call<ResponseInfo> call, Throwable t) {
                t.printStackTrace();
            }
        });
    }
}
