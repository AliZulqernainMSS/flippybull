using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using System;
using UnityEngine.Analytics;

namespace mindstormstudios.hypercausalplugin
{
    public class HCController
    {
        Action<string> updateCallback;

        static HCController instance = null;
        public static HCController Instance()
        {
            if (instance == null)
            {
                instance = new HCController();
                instance.Initialize();
            }
            return instance;
        }

        public void SetUpdateLabel(Action<string> callback)
        {
            updateCallback = callback;
            if (HCConstants.enableFacebook)
            {
                HCFacebookController.Instance().SetUpdateLabel(updateCallback);
            }

            if (HCConstants.enableAdmob)
            {
                HCAdmobController.Instance().SetUpdateLabel(updateCallback);
            }
        }

        private void Initialize()
        {
            if (HCConstants.enableGameAnalytics)
            {
                HCGameAnalytics.Instance();
            }

            if (HCConstants.enableFacebook)
            {
                HCFacebookController.Instance();
            }

            if (HCConstants.enableAdmob)
            {
                HCAdmobController.Instance();
            }

            if (HCConstants.enableAdjust)
            {
                HCAdjustController.Instance();
            }

            if (HCConstants.enableAppsflyer)
            {
                HCAppFlyerController.Instance();
            }

        }

        #region ads
        public bool ShowRewardedAds()
        {
            if (HCConstants.enableAdmob)
            {
               return HCAdmobController.Instance().ShowRewardedVideoAd();
            }
                return false;
        }

        public bool ShowInterstitialAds()
        {
            if (HCConstants.enableAdmob)
            {
                return HCAdmobController.Instance().ShowInterstitial();
            }
            return false;
        }

        public void ShowBannerAds()
        {
            if (HCConstants.enableAdmob)
            {
                 HCAdmobController.Instance().ShowBanner();
            }
        }

        public void HideBannerAds()
        {
            if (HCConstants.enableAdmob)
            {
                HCAdmobController.Instance().HideBanner();
            }
        }
        #endregion

        #region events
        // Game Analytics
        public void SendGAPurchaseEvent(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string signature)
        {
            if (HCConstants.enableGameAnalytics)
            {
                HCGameAnalytics.Instance().sendBusinessEvent(currency,amount,itemType,itemId,cartType,receipt,signature);
            }
        }

        public void SendGADesigntEvent(string eventName, float eventValue)
        {
            if (HCConstants.enableGameAnalytics)
            {
                HCGameAnalytics.Instance().sendDesignEvent(eventName,eventValue);
            }
        }

        // Adjust
        public void SendAdjustEvent(string eventName)
        {
            if (HCConstants.enableAdjust)
            {
                HCAdjustController.Instance().SendAdjustEvent(eventName);
            }
        }

        // Facebook
        public void SendFacebookEvent(string eventName, string contentId = "", string description = "", string level = "")
        {
            if (HCConstants.enableFacebook)
            {
                HCFacebookController.Instance().sendAnalytics(eventName, contentId, description, level);
            }
        }

        // UA
        public void SendUnityAnalyticsEvent(string eventName, Dictionary<string,object> data = null)
        {
            if (HCConstants.enableUnityAnalytics)
            {
                if (data != null)
                {
                    Analytics.CustomEvent(eventName, data);
                }
                else
                {
                    Analytics.CustomEvent(eventName);
                }
            }
        }
        #endregion
    }
}
