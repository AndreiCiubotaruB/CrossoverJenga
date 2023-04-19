using UnityEngine;

public class JsonHelper {
    public static T getJsonArray<T>(string json, string name) {
        string newJson = "{ \""+name+"\": " + json + "}";
        return JsonUtility.FromJson<T>(newJson); ;
    }
}