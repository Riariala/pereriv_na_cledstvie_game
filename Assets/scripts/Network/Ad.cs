using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour
{
    [SerializeField] private bool testMode = true;

    private string gameId = "4756393";

    private string rewarded = "Rewarded_Android";
    private string banner = "Banner_Android";


    void Start()
    {
        Advertisement.Initialize(gameId, testMode);

        #region Banner
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerWhenInitialized());
        

        #endregion

    }

    public static void ShowAdsVideo(string placementId)
    {
        if (Advertisement.isShowing)
        {
            Advertisement.Show(placementId);
        }

        else
        {
            Debug.Log("Реклама не готова");
        }

    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Жди рекламу");
        }
        Advertisement.Banner.Show(banner);
        Debug.Log("Реклама");
    }

}
