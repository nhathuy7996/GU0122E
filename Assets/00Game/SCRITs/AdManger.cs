using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;

public class AdManger : Singleton<AdManger>
{
    [SerializeField]
    private string _bannerAdUnit = "ca-app-pub-3940256099942544/6300978111",
        _interAdUnit = "ca-app-pub-3940256099942544/1033173712",
        _rewardAdUnit = "ca-app-pub-3940256099942544/5224354917",
        _AoAdUnit = "ca-app-pub-3940256099942544/9257395921";
    BannerView _bannerView;

    InterstitialAd _interstitialAd;

    RewardedAd _rewardAd;

    AppOpenAd _appOpenAd;

    bool isShowing = false;

    float delayLoadBanner = 1,
        delayLoadInter =1,
        delayLoadReward = 1,
        delayLoadAoA = 1;

    Action<bool> InterCallBack, RewardCallback,AoACallback = null;

    // Start is called before the first frame update
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => {
            InitBaner();
            LoadInter();
            LoadRewardedAd();
            LoadAoa();
        });

        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    private void OnAppStateChanged(AppState state)
    {
        if (state == AppState.Background)
            this.ShowAoA();
    }

    #region Banner
    void InitBaner()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            _bannerView = new BannerView(_bannerAdUnit, AdSize.Banner,AdPosition.Bottom);
        }

        // create our request used to load the ad.
     

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
       
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());

            delayLoadBanner = 1;
             
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
            Invoke( "LoadBanner",delayLoadBanner);
            delayLoadBanner *= 2;
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
            isShowing = true;
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };

       

        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };

        LoadBanner();
    }

    void LoadBanner()
    {
        var adRequest = new AdRequest();
        _bannerView.LoadAd(adRequest);
    }

    public void ShowBanner()
    {
        if (_bannerView == null)
            return;

        _bannerView.Show();
    }

    public void HideBanner()
    {
     
        if (_bannerView == null)
            return;

        Debug.LogError("tessttt");
        _bannerView.Hide();
    }

    #endregion


    #region Inter
    

    void LoadInter()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        var adRequest = new AdRequest();

        InterstitialAd.Load(_interAdUnit, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);

                  Invoke("LoadInter", delayLoadInter);
                  delayLoadInter *= 2;
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              _interstitialAd = ad;
              delayLoadInter = 1;
              _interstitialAd.OnAdPaid += (AdValue adValue) =>
              {
                  Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                      adValue.Value,
                      adValue.CurrencyCode));
              };
              // Raised when an impression is recorded for an ad.
              _interstitialAd.OnAdImpressionRecorded += () =>
              {
                  Debug.Log("Interstitial ad recorded an impression.");
              };
              // Raised when a click is recorded for an ad.
              _interstitialAd.OnAdClicked += () =>
              {
                  Debug.Log("Interstitial ad was clicked.");
                  isShowing = true;
              };
              // Raised when an ad opened full screen content.
              _interstitialAd.OnAdFullScreenContentOpened += () =>
              {
                  Debug.Log("Interstitial ad full screen content opened.");
              };
              // Raised when the ad closed full screen content.
              _interstitialAd.OnAdFullScreenContentClosed += () =>
              {
                  Debug.Log("Interstitial ad full screen content closed.");
                  isShowing = false;
                  this.InterCallBack?.Invoke(true);
              };
              // Raised when the ad failed to open full screen content.
              _interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
              {
                  Debug.LogError("Interstitial ad failed to open full screen content " +
                                 "with error : " + error);

                  Invoke("LoadInter", delayLoadInter);
                  delayLoadInter *= 2;

                  this.InterCallBack?.Invoke(false);
              };

          });
    }

    public void ShowInter(Action<bool> callback = null)
    {
        if (_interstitialAd == null)
        {
            callback?.Invoke(false);
            return;
        }

        this.InterCallBack = callback;
        _interstitialAd.Show();
    }

    #endregion


    #region Reward
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardAd != null)
        {
            _rewardAd.Destroy();
            _rewardAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_rewardAdUnit, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);

                    Invoke("LoadRewardedAd", delayLoadReward);
                    delayLoadReward *= 2;
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardAd = ad;
                delayLoadReward = 1;
                // Raised when the ad is estimated to have earned money.
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                        adValue.Value,
                        adValue.CurrencyCode));
                };
                // Raised when an impression is recorded for an ad.
                ad.OnAdImpressionRecorded += () =>
                {
                    Debug.Log("Rewarded ad recorded an impression.");
                };
                // Raised when a click is recorded for an ad.
                ad.OnAdClicked += () =>
                {
                    Debug.Log("Rewarded ad was clicked.");
                };
                // Raised when an ad opened full screen content.
                ad.OnAdFullScreenContentOpened += () =>
                {
                    Debug.Log("Rewarded ad full screen content opened.");
                    isShowing = true;
                };
                // Raised when the ad closed full screen content.
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Rewarded ad full screen content closed.");
                    isShowing = false;
                    RewardCallback?.Invoke(false);
                };
                // Raised when the ad failed to open full screen content.
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.LogError("Rewarded ad failed to open full screen content " +
                                   "with error : " + error);
                    Invoke("LoadRewardedAd", delayLoadReward);
                    delayLoadReward *= 2;
                };

                
            });
    }
    
    public void ShowReward(Action<bool> callback = null)
    {
        if(_rewardAd == null)
        {
            callback?.Invoke(false);
            return;
        }

        if(!_rewardAd.CanShowAd())
        {
            callback?.Invoke(false);
            return;
        }

        RewardCallback = callback;
        _rewardAd.Show(Reward =>
        {
            callback?.Invoke(true);
            RewardCallback = null;
        });
    }

    #endregion

    #region AoA
    void LoadAoa()
    {
        // Clean up the old ad before loading a new one.
        if (_appOpenAd != null)
        {
            _appOpenAd.Destroy();
            _appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(_AoAdUnit, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);

                    Invoke("LoadAoa", delayLoadAoA);
                    delayLoadAoA *= 2;
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                _appOpenAd = ad;

                // Raised when the ad is estimated to have earned money.
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    Debug.Log(String.Format("App open ad paid {0} {1}.",
                        adValue.Value,
                        adValue.CurrencyCode));
                };
                // Raised when an impression is recorded for an ad.
                ad.OnAdImpressionRecorded += () =>
                {
                    Debug.Log("App open ad recorded an impression.");
                };
                // Raised when a click is recorded for an ad.
                ad.OnAdClicked += () =>
                {
                    Debug.Log("App open ad was clicked.");
                };
                // Raised when an ad opened full screen content.
                ad.OnAdFullScreenContentOpened += () =>
                {
                    Debug.Log("App open ad full screen content opened.");
                    isShowing = true;
                };
                // Raised when the ad closed full screen content.
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("App open ad full screen content closed.");
                    isShowing = false;
                    AoACallback?.Invoke(true);
                };
                // Raised when the ad failed to open full screen content.
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.LogError("App open ad failed to open full screen content " +
                                   "with error : " + error);

                    Invoke("LoadAoa", delayLoadAoA);
                    delayLoadAoA *= 2;
                    AoACallback?.Invoke(false);
                };
            });
    }
    
    public void ShowAoA(Action<bool> callback = null)
    {
        if (_appOpenAd != null && _appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            AoACallback = callback;
            _appOpenAd.Show();
            return;
        }

        callback?.Invoke(false);
    }
    
    #endregion

    
}
