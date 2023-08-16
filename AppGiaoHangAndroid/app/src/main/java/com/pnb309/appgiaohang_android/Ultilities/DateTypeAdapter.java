package com.pnb309.appgiaohang_android.Ultilities;

import com.google.gson.TypeAdapter;
import com.google.gson.stream.JsonReader;
import com.google.gson.stream.JsonToken;
import com.google.gson.stream.JsonWriter;

import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class DateTypeAdapter extends TypeAdapter<Date> {
    private final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS");
    private final SimpleDateFormat dateFormat2 = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");

    @Override
    public void write(JsonWriter out, Date value) throws IOException {
        if (value == null) {
            out.nullValue();
        } else {
            out.value(dateFormat.format(value));
        }
    }

    @Override
    public Date read(JsonReader in) throws IOException {
        if (in.peek() == JsonToken.NULL) {
            in.nextNull();
            return null;
        }
        String dateStr = in.nextString();
        try {
            return dateFormat.parse(dateStr);
        } catch (ParseException e) {
           try{
               return dateFormat2.parse(dateStr);

           } catch (ParseException ex) {
               throw new RuntimeException(ex);
           }
        }
    }
    public static Date convertStringToDate(String dateStr) {
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS");
        final SimpleDateFormat dateFormat2 = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        try {
            return dateFormat.parse(dateStr);
        } catch (ParseException e) {
            try {
                return dateFormat2.parse(dateStr);

            } catch (ParseException ex) {
                throw new RuntimeException(ex);
            }
        }
    }
}