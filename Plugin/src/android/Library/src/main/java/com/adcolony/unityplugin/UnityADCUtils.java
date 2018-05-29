package com.adcolony.unityplugin;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONObject;
import org.json.JSONException;
import com.adcolony.sdk.*;

public class UnityADCUtils {
    static String toJson(Map<String, Object> data) {
        if (data == null) {
            return null;
        }

        JSONObject json = new JSONObject(data);
        return json.toString();
    }

    static String toJson(List<Object> data) {
        if (data == null) {
            return null;
        }

        System.out.println(data.toString());
        JSONArray json = new JSONArray(data);
        return json.toString();
    }

    static String toJsonFromStringList(List<String> data) {
        if (data == null) {
            return null;
        }

        System.out.println(data.toString());
        JSONArray json = new JSONArray(data);
        return json.toString();
    }

    static List<Object> jsonToList(String json) {
        if (json == null) {
            return null;
        }

        List<Object> value = null;
        try {
            JSONArray jsonObject = new JSONArray(json);

            if (json != JSONObject.NULL) {
                value = toList(jsonObject);
            }
        } catch (JSONException e) {
        }
        return value;
    }

    static Map<String, Object> jsonToMap(String json) {
        if (json == null) {
            return null;
        }

        Map<String, Object> value = null;
        try {
            JSONObject jsonObject = new JSONObject(json);

            if (json != JSONObject.NULL) {
                value = toMap(jsonObject);
            }
        } catch (JSONException e) {
        }
        return value;
    }

    static Map<String, Object> toMap(JSONObject object) {
        Map<String, Object> map = new HashMap<String, Object>();

        Iterator<String> keysItr = object.keys();
        while (keysItr.hasNext()) {
            String key = keysItr.next();
            try {
                Object value = object.get(key);

                if (value instanceof JSONArray) {
                    value = toList((JSONArray) value);
                } else if (value instanceof JSONObject) {
                    value = toMap((JSONObject) value);
                }

                if (value == null) {
                    return null;
                }

                map.put(key, value);
            } catch (JSONException e) {
                return null;
            }
        }
        return map;
    }

    static List<Object> toList(JSONArray array) {
        List<Object> list = new ArrayList<Object>();
        for (int i = 0; i < array.length(); i++) {
            try {
                Object jsonObject = array.get(i);
                Object value = jsonObject;

                if (jsonObject instanceof JSONArray) {
                    value = toList((JSONArray) jsonObject);
                } else if (jsonObject instanceof JSONObject) {
                    value = toMap((JSONObject) jsonObject);
                }

                if (value == null) {
                    return null;
                }

                list.add(value);
            } catch (JSONException e) {
                return null;
            }
        }
        return list;
    }

    static String zoneToJson(AdColonyZone zone) {
        Map<String, Object> map = new HashMap<String, Object>();
        if (zone != null) {
            map.put("zone_id", zone.getZoneID());
            map.put("type", zone.getZoneType());
            map.put("enabled", zone.isValid());
            map.put("rewarded", zone.isRewarded());
            map.put("views_per_reward", zone.getViewsPerReward());
            map.put("views_until_reward", zone.getRemainingViewsUntilReward());
            map.put("reward_amount", zone.getRewardAmount());
            map.put("reward_name", zone.getRewardName());
        }
        return UnityADCUtils.toJson(map);
    }

    static String rewardToJson(AdColonyReward reward) {
        Map<String, Object> map = new HashMap<String, Object>();
        if (reward != null) {
            map.put("zone_id", reward.getZoneID());
            map.put("success", reward.success());
            map.put("name", reward.getRewardName());
            map.put("amount", reward.getRewardAmount());
        }
        return UnityADCUtils.toJson(map);
    }
}
