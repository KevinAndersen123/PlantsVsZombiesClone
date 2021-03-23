using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

[System.Serializable]
public class GameState
{
    public float completion_time = 0;
    public int level = 1;
    public string deviceID;
    public int peaUsages = 0;
    public int iceUsages = 0;
    public int sunflowerUsages = 0;
    public int cherryUsages = 0;
    public int wallnutUsages = 0;
    public int shovelUsages = 0;
    public bool completeLevel;

}

public class AnalyticsManager : MonoBehaviour
{
    public void SendData(bool t_completed) 
    {
        GameState data = new GameState 
        { deviceID = SystemInfo.deviceUniqueIdentifier, level = Data.s_currentLevel, completion_time = Data.s_levelTime, completeLevel = t_completed, 
            peaUsages = Data.s_peaShooterCounter, iceUsages = Data.s_iceShooterCounter,  sunflowerUsages = Data.s_sunflowerCounter, cherryUsages = Data.s_cherryCounter, wallnutUsages = Data.s_wallnutCounter, shovelUsages = Data.s_shovelCounter };
        string jsonData = JsonUtility.ToJson(data);
        StartCoroutine(PostMethod(jsonData));
    }
    public static IEnumerator PostMethod(string jsonData)
    {
        string url = "http://3.85.142.181:5000/upload_data";
        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
                Debug.Log("Data successfully sent to the server");
            else
                Debug.Log("Error sending data to the server: Error " + request.responseCode);
        }
    }
}
