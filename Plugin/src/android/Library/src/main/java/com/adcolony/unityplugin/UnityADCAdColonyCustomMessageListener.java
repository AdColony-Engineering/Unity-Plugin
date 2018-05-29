package com.adcolony.unityplugin;

import android.net.Uri;
import java.util.*;
import android.util.Log;
import com.adcolony.sdk.*;
import java.util.UUID;

public class UnityADCAdColonyCustomMessageListener implements AdColonyCustomMessageListener {

    public void onAdColonyCustomMessage(AdColonyCustomMessage message) {
        Log.d("UnityADCAds", "onAdColonyCustomMessage: type: " + message.getType() + ", message: " + message.getMessage());
        Map<String, Object> data = new HashMap<String, Object>();
        if (message != null) {
            data.put("type", message.getType());
            data.put("message", message.getMessage());
        }
        UnityADCAds.sendUnityMessage("_OnCustomMessageReceived", UnityADCUtils.toJson(data));
    }
}
