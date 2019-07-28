using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomBtnFunction : MonoBehaviour
{
    [SerializeField] private Transform tower4Parent;
    [SerializeField] private TextMeshProUGUI loaderTextHolder, errorTextHolder;
    public static GameObject tower4;
    public void LoadTower4Fern()
    {
        if (tower4 == null)
        {
            StartCoroutine(LoadAssetBuilding(AssetBundleLinkManager.Instance.tower4BundleUrl));
        }
        else
        {
            if (tower4.activeSelf == true)
                tower4.SetActive(false);
            else
                tower4.SetActive(true);
        }
    }
    IEnumerator LoadAssetBuilding(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return uwr.SendWebRequest();

            while (!uwr.isDone)
            {
                if (loaderTextHolder.transform.parent.gameObject.activeSelf == false)
                    loaderTextHolder.transform.parent.gameObject.SetActive(true);

                yield return null;
            }
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
                errorTextHolder.transform.parent.gameObject.SetActive(true);
                errorTextHolder.text = uwr.error + ".\n\nPlease check your internet.";
            }
            else
            {
                AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
                var prefab = assetBundle.LoadAsset("Tower4") as GameObject;
                tower4 = Instantiate(prefab, tower4Parent, false);
                loaderTextHolder.transform.parent.gameObject.SetActive(false);
            }

        }
    }
}
