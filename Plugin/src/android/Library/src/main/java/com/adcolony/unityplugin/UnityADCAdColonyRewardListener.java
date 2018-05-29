package com.adcolony.unityplugin;

import android.net.Uri;
import java.util.*;
import android.util.Log;
import com.adcolony.sdk.*;
import java.util.UUID;

public class UnityADCAdColonyRewardListener implements AdColonyRewardListener {

    public void onReward(AdColonyReward reward) {
        UnityADCAds.sendUnityMessage("_OnRewardGranted", UnityADCUtils.rewardToJson(reward));
    }
}
