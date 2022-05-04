using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdMobs : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _IOSGameId;
    [SerializeField] private bool _testMode;

    private string _gameId;

    private void Awake()
    {
        IntializeAds();
       // AdsButton.S.LoadAd();
    }

    private void IntializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _IOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("unity Ads initialization comple:");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

}

