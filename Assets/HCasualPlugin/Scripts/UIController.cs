using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mindstormstudios.hypercausalplugin;
public class UIController : MonoBehaviour {

    public Text status;
	// Use this for initialization
	void Start () 
    {
        HCController.Instance();
        HCController.Instance().SetUpdateLabel(UpdateText);
        HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_GAME_OPEN);

#if UNITY_IOS
        Debug.Log("IDFA:" + UnityEngine.iOS.Device.advertisingIdentifier);
#endif

    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SessionEnded();
        }
    }

    public void OnPressRewardedVideoAds()
    {
        HCController.Instance().ShowRewardedAds();
    }

    public void OnPressInterstitialVideoAds()
    {
        HCController.Instance().ShowInterstitialAds();
    }

    public void ShowBannerAds()
    {
        HCController.Instance().ShowBannerAds();
    }

    public void HideBannerAds()
    {
        HCController.Instance().HideBannerAds();
    }

    public void UpdateText(string text)
    {
        status.text = status.text + text + "\n";
    }

    public void OpenMarket()
    {
        HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_MARKET_OPEN);
    }

    public void PurchaseClicked()
    {
        HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_IAP_CLICKED);
    }

    public void ActivatedUserClicked()
    {
        HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_ACTIVATED_USER);
    }

    public void TutorialCompleted()
    {
        HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_TUTORIAL_COMPLETE);
    }


    public void SessionEnded()
    {
        HCController.Instance().SendAdjustEvent(HCConstants.ADJUST_PLAYSESSION_END);
    }
}
