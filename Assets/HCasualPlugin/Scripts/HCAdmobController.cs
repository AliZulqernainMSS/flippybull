using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace mindstormstudios.hypercausalplugin 
{
    public class HCAdmobController  
    {
        #region variables
        Action<string> updateCallback;
        static HCAdmobController instance = null;
        public static HCAdmobController Instance() 
        {
            if(instance == null)
            {
                instance = new HCAdmobController();
                instance.Initialize();
            }
            return instance;
        }

        private HCAdmobController()
        {

        }

        private RewardBasedVideoAd rewardBasedVideo;
        private BannerView bannerView = null;
        private InterstitialAd interstitial;
        #endregion


        #region public methods

        public void SetUpdateLabel(Action<string> callback)
        {
            updateCallback = callback;
        }

        public bool ShowRewardedVideoAd()
        {
            if (rewardBasedVideo.IsLoaded())
            {
                rewardBasedVideo.Show();
                HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_AD_REWARDED_IMPRESSION);
                return true;
            }
            this.RequestRewardBasedVideo();
            return false;
        }

        public void ShowBanner()
        {
            // Create a 320x50 banner at the top of the screen.
            if (bannerView == null)
            {
                bannerView = new BannerView(HCConstants.adUnitIdBanner, AdSize.Banner, AdPosition.Bottom);
                RegisterBannerAdsCallbacks();
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                bannerView.LoadAd(request);
            }
        }

        public void HideBanner()
        {
            bannerView.Destroy();
            bannerView = null;
        }

        public bool ShowInterstitial()
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
                HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_AD_INTERSTITIAL_IMPRESSION);
                return true;
            }
            this.RequestInterstitial();
            return false;
        }
        #endregion

        #region private methods
        private void Initialize()
        {
            MobileAds.Initialize(HCConstants.admobId);
            // Get singleton reward based video ad reference.
            this.rewardBasedVideo = RewardBasedVideoAd.Instance;
            RegisterRewardedAdsCallbacks();

            this.RequestRewardBasedVideo();
            this.RequestInterstitial();
        }

        private void RequestRewardBasedVideo()
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded video ad with the request.
            this.rewardBasedVideo.LoadAd(request, HCConstants.adUnitIdRewardedVideo);
        }

        private void RequestInterstitial()
        {
            this.interstitial = new InterstitialAd(HCConstants.adUnitIdInterestrials);
            RegisterInterstitialCallbacks();
            AdRequest request = new AdRequest.Builder().Build();
            this.interstitial.LoadAd(request);
        }

        private void UpdateText(string text)
        {
            if(updateCallback != null)
            {
                updateCallback(text);
            }
        }
        #endregion

        #region BannerView Callbacks
        void RegisterBannerAdsCallbacks()
        {
            // Called when an ad request has successfully loaded.
            bannerView.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerView.OnAdOpening += HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerView.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.Message);
            UpdateText("HandleFailedToReceiveAd Banner");

        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
        #endregion

        #region Rewarded callbacks
        void RegisterRewardedAdsCallbacks()
        {
            // Called when an ad request has successfully loaded.
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // Called when the ad starts to play.
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            // Called when the user should be rewarded for watching a video.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // Called when the ad click caused the user to leave the application.
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        }

        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardBasedVideoFailedToLoad event received with message: "
                                 + args.Message);
            UpdateText("HandleRewardBasedVideoFailedToLoad");
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
        }

        public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
            this.RequestRewardBasedVideo();

        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print(
                "HandleRewardBasedVideoRewarded event received for "
                            + amount.ToString() + " " + type);
            UpdateText("HandleRewardBasedVideoRewarded");
        }

        public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
        }
        #endregion

        #region Interstitial Callbacks
        void RegisterInterstitialCallbacks()
        {
            this.interstitial.OnAdLoaded += HandleOnAdLoadedInterstitial;
            // Called when an ad request failed to load.
            this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoadInterstitial;
            // Called when an ad is shown.
            this.interstitial.OnAdOpening += HandleOnAdOpenedInterstitial;
            // Called when the ad is closed.
            this.interstitial.OnAdClosed += HandleOnAdClosedInterstitial;
            // Called when the ad click caused the user to leave the application.
            this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplicationInterstitial;
        }

        public void HandleOnAdLoadedInterstitial(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoadInterstitial(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.Message);
            UpdateText("HandleFailedToReceiveAd Interstitial");

        }

        public void HandleOnAdOpenedInterstitial(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosedInterstitial(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
            this.RequestInterstitial();
        }

        public void HandleOnAdLeavingApplicationInterstitial(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
        #endregion
    }
}