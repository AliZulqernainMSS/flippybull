using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mindstormstudios.hypercausalplugin
{

    public class HCAppFlyerController
    {
        static HCAppFlyerController instance = null;
        public static HCAppFlyerController Instance()
        {
            if (instance == null)
            {
                instance = new HCAppFlyerController();
                instance.Initialize();
            }
            return instance;
        }

        #region private methods
        private HCAppFlyerController()
        {

        }

        private void Initialize()
        {

            AppsFlyer.setAppsFlyerKey(HCConstants.appFlyerDevKey);
            /* For detailed logging */
            /* AppsFlyer.setIsDebug (true); */
#if UNITY_IOS
            /* Mandatory - set your apple app ID
               NOTE: You should enter the number only and not the "ID" prefix */
            AppsFlyer.setAppID(HCConstants.appFlyerAppId);
            AppsFlyer.trackAppLaunch();
#elif UNITY_ANDROID
   /* Mandatory - set your Android package name */
   AppsFlyer.setAppID (HCConstants.appFlyerPackageName);
   /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
   AppsFlyer.init (HCConstants.appFlyerDevKey,"AppsFlyerTrackerCallbacks");
#endif
        }

        void AppsFlyerTrackerCallbacks()
        {

        }
        #endregion

        #region public methods
        void SendPurchaseEvent(string currency, string price, string rewardAmount)
        {
            System.Collections.Generic.Dictionary<string, string> purchaseEvent = new
            System.Collections.Generic.Dictionary<string, string>();
            purchaseEvent.Add("af_currency", currency); //"USD");
            purchaseEvent.Add("af_revenue", price); //"0.99");
            purchaseEvent.Add("af_quantity", rewardAmount);// "1");
            AppsFlyer.trackRichEvent("af_purchase", purchaseEvent);
        }
        #endregion

    }
}