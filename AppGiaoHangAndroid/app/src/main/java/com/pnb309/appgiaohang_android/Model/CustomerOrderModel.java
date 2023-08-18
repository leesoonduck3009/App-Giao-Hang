package com.pnb309.appgiaohang_android.Model;

import android.content.Context;
import android.net.Uri;
import android.webkit.MimeTypeMap;

import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.Tasks;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;
import com.google.firebase.storage.UploadTask;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.pnb309.appgiaohang_android.Entity.CustomerOrder;
import com.pnb309.appgiaohang_android.Entity.ResponseInfo;
import com.pnb309.appgiaohang_android.Services.ICustomerOrderServices;
import com.pnb309.appgiaohang_android.Ultilities.TokenVariable;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class CustomerOrderModel implements ICustomerOrderModel {
    private ICustomerOrderServices customerOrderServices;
    private Retrofit retrofit;
    private FirebaseStorage firebaseStorage;
    private Context context;
    public CustomerOrderModel(Context context)
    {
        retrofit = TokenVariable.getRetrofitInstance();
        this.context = context;
        customerOrderServices = retrofit.create(ICustomerOrderServices.class);
        firebaseStorage = FirebaseStorage.getInstance();
    }

    @Override
    public void LoadCustomerOrderByEmployeeIDAndStatus(long id,String status, OnLoadCustomerOrderByEmployeeIDListener listener) {
        CustomerOrder customerOrder = new CustomerOrder();
        customerOrder.setEmployeeId(id);
        customerOrder.setOrderStatus(status);
        customerOrderServices.getCustomerOrderByEmployeeIDAndStatus(customerOrder).enqueue(new Callback<ResponseInfo>() {
            @Override
            public void onResponse(Call<ResponseInfo> call, Response<ResponseInfo> response) {
                if(response.isSuccessful() && response.code() == 200)
                {
                    ResponseInfo responseInfo = response.body();
                    if(responseInfo.getStatusCode() == 200) {
                        listener.OnFinishLoadCustomerOrderByEmployeeID((List<CustomerOrder>) responseInfo.getData());
                    }
                    else
                        listener.OnFinishLoadCustomerOrderByEmployeeID(null);
                }
            }
            @Override
            public void onFailure(Call<ResponseInfo> call, Throwable t) {
                t.printStackTrace();
            }
        });
    }

    @Override
    public void updateCustomerOrderStatus(CustomerOrder customerOrder,Uri updateImage, OnUpdateCustomerOrderListener listener) {
        if(updateImage!=null) {
            upLoadImageToFireStorage(updateImage,customerOrder,linkImage -> {
                try {
                    customerOrder.setLinkImage(linkImage);
                    ICustomerOrderServices customerOrderServices = TokenVariable.getRetrofitExcludeInstance().create(ICustomerOrderServices.class);
                    String json = new GsonBuilder()
                            .excludeFieldsWithoutExposeAnnotation().create().toJson(customerOrder);
                     customerOrderServices.updateCustomerOrderByID(customerOrder.getCustomerOrderId(), customerOrder).enqueue(new Callback<ResponseInfo>() {
                         @Override
                         public void onResponse(Call<ResponseInfo> call, Response<ResponseInfo> response) {
                             ResponseInfo responseInfo = response.body();

                             listener.OnFinishhUpdateCustomerOrder(true,null);
                         }

                         @Override
                         public void onFailure(Call<ResponseInfo> call, Throwable t) {
                            t.printStackTrace();
                         }
                     });
                    // listener.OnFinishhUpdateCustomerOrder(true,null);
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            });
        }
        else {
            ICustomerOrderServices customerOrderServices = TokenVariable.getRetrofitExcludeInstance().create(ICustomerOrderServices.class);
            String json = new GsonBuilder()
                    .excludeFieldsWithoutExposeAnnotation().create().toJson(customerOrder);
            customerOrderServices.updateCustomerOrderByID(customerOrder.getCustomerOrderId(),customerOrder).enqueue(new Callback<ResponseInfo>() {
                @Override
                public void onResponse(Call<ResponseInfo> call, Response<ResponseInfo> response) {
                    if(response.isSuccessful()) {
                        ResponseInfo responseInfo = response.body();
                        listener.OnFinishhUpdateCustomerOrder(true, null);
                    }
                }

                @Override
                public void onFailure(Call<ResponseInfo> call, Throwable t) {

                }
            });
        }
    }

    private void upLoadImageToFireStorage(Uri Image, CustomerOrder customerOrder, onFinishUploadIamgeListener listener)
    {
        String extension = MimeTypeMap.getSingleton().getExtensionFromMimeType(context.getContentResolver().getType(Image));

            if (extension == null) {
                extension = "jpg";
            }
            StorageReference imageRef = firebaseStorage.getReference().child(CustomerOrder.TABLE_NAME).child(customerOrder.getCustomerOrderId() + "/Image."+ extension);
            UploadTask uploadTask = imageRef.putFile(Image);
            uploadTask.addOnCompleteListener(task -> {
                if(!task.isSuccessful())
                    task.getException().printStackTrace();
                else {
                    task.getResult().getStorage().getDownloadUrl().addOnSuccessListener(uri -> {
                        String imageURL = uri.toString();
                        listener.FinishUploadImage(imageURL);
                    }).addOnFailureListener(e -> {e.printStackTrace();});
                }
            });
        }

    }
    interface onFinishUploadIamgeListener {
        void FinishUploadImage(String linkImage);
    }


