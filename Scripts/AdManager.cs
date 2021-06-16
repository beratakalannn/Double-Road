using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "4085850";
    private string devam = "OyunaDevamEt";

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    public void devamBtn()
    {
        if (Advertisement.IsReady(devam))
        {
            Advertisement.Show(devam);
        }
        else
        {
            Debug.Log("Olmadı");
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished || showResult == ShowResult.Skipped)
        {
            GamePlayController.instance.ContinueGame();
        }
        
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

   

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Hata" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Başladı" + placementId);
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
}
