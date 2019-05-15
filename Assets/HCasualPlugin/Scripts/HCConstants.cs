using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mindstormstudios.hypercausalplugin
{
    public class HCConstants
    {
        #region configs
        public static bool enableAdmob = true;
        public static bool enableFacebook = true;
        public static bool enableGameAnalytics = true;
        public static bool enableAdjust = true;
        public static bool enableAppsflyer = true;
        public static bool enableUnityAnalytics = true;
        #endregion

        #region sdk ids
        // Adjust
        public static string ADJUST_APP_TOKEN                   = "w57orvgvlk3k";
        // need  to be sent by game
        public static string ADJUST_TUTORIAL_COMPLETE           = "iy09ug";
        public static string ADJUST_ACTIVATED_USER              = "pvowsn";
        public static string ADJUST_IAP_CLICKED                 = "tv1tfk";
        public static string ADJUST_GAME_OPEN                   = "9urptr";
        public static string ADJUST_MARKET_OPEN                 = "xajnas";
        public static string ADJUST_PLAYSESSION_END             = "cyszd4";
        // will be sent by sdk internally
        public static string ADJUST_AD_REWARDED_IMPRESSION      = "9l6ric";
        public static string ADJUST_AD_INTERSTITIAL_IMPRESSION  = "31eepk";

        // App Flyer
        public static string appFlyerDevKey = "{YOUR_APPSFLYER_DEV_KEY}";
        public static string appFlyerAppId = "{YOUR_APPSFLYER_APP_ID}";
        public static string appFlyerPackageName = "{YOUR_APPSFLYER_APP_PACKAGE_NAME}";

        // Game Analytics
#if UNITY_ANDROID
        public static string GAGameKey = "56357243709dfd76df0169e86f75d476";
        public static string GASecretKey = "919ecc50124293ca8a4ad70b35cbd9a44550bb9f";
#elif UNITY_IPHONE
        public static string GAGameKey   = "2fe191b5880487e67cdbb43b2a85551e";
        public static string GASecretKey = "0273a78595044e1a0e14c07072b0038c25f95ef1";
#endif

        #endregion

        #region admob
#if UNITY_ANDROID
        public static string admobId                = "ca-app-pub-7418823270776132~4501485401";
        public static string adUnitIdRewardedVideo  = "ca-app-pub-7418823270776132/5978218607";
        public static string adUnitIdBanner         = "ca-app-pub-5568002574023697/8046641449";
        public static string adUnitIdInterestrials  = "ca-app-pub-7418823270776132/9784476942";

#elif UNITY_IPHONE
        public static string admobId                = "ca-app-pub-7418823270776132~5174512931";
        public static string adUnitIdRewardedVideo  = "ca-app-pub-7418823270776132/3302070200";
        public static string adUnitIdBanner         = "ca-app-pub-5568002574023697/8046641449";
        public static string adUnitIdInterestrials  = "ca-app-pub-7418823270776132/3262665462";

#endif
      
#endregion

    }
}
