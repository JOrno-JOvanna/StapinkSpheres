using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public DelegateHolder delegateHolder;
    public GameObject loadPanel;
    public Transform levelsHolder, BGHolder;
    public List<Sprite> accessableBG;

    private string jsonFilePath = "Assets/StreamingAssets/LevelsInfo.json";
    private Dictionary<string, int> passedLevels = new Dictionary<string, int>();
    private UniWebView webView;

    public void Start()
    {
        delegateHolder.OnWinLevel += WriteJSON;
        StartCoroutine(SimulateLoading());
    }

    private IEnumerator SimulateLoading()
    {
        ReadJSON();
        CheckLevels();
        CheckBGAccess();

        yield return new WaitForSeconds(1f);

        loadPanel.SetActive(false);
    }

    public void ReadJSON()
    {
        string playersData = File.ReadAllText(jsonFilePath);

        passedLevels = JsonConvert.DeserializeObject<Dictionary<string, int>>(playersData);
    }

    private void CheckBGAccess()
    {
        int[] BGAccessLevels = { 3, 6, 9, 12 };

        for (int i = 0; i < BGAccessLevels.Length; i++)
        {
            if (passedLevels.ContainsKey((BGAccessLevels[i] + 1).ToString()))
            {
                BGHolder.GetChild(i + 1).GetComponent<Image>().sprite = accessableBG[i];
                BGHolder.GetChild(i + 1).GetComponent<Button>().enabled = true;
            }
        }
    }

    private void CheckLevels()
    {
        for(int i = 0; i < levelsHolder.childCount; i++)
        {
            if (passedLevels.ContainsKey((i + 1).ToString()))
            {
                int passedLevelID = 0;
                passedLevels.TryGetValue((i + 1).ToString(), out passedLevelID);
                levelsHolder.GetChild(i).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = passedLevelID.ToString();
                levelsHolder.GetChild(i).GetComponent<Button>().enabled = true;
                levelsHolder.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    private void WriteJSON(int passedLevel, int score)
    {
        passedLevels[passedLevel.ToString()] = score;
        passedLevels.Add((passedLevel + 1).ToString(), 0);

        string playersData = JsonConvert.SerializeObject(passedLevels);

        File.WriteAllText(jsonFilePath, playersData);

        CheckLevels();
        CheckBGAccess();
    }

    private IEnumerator Waiting(GameObject tab, bool open)
    {
        delegateHolder.InvokeSetPatternMenu();

        yield return new WaitForSeconds(1);

        tab.SetActive(open);
    }

    public void OpenHyperLink(string link)
    {
        var wbGameObj = new GameObject("UniWebView");

        webView = wbGameObj.AddComponent<UniWebView>();

        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        webView.Load(link);
        webView.Show();

        webView.SetShowToolbar(true);

        webView.OnShouldClose += (view) =>
        {
            webView = null;

            Destroy(wbGameObj);

            return true;
        };
    }

    public void ShareApp()
    {
        StartCoroutine(ShareAppProcess());
    }

    private IEnumerator ShareAppProcess()
    {
        yield return new WaitForEndOfFrame();

        new NativeShare().SetText("This game is SUPER! Come pop bubbles with me!").SetUrl("https://apps.apple.com/app/stapink-spheres/id6711349972").Share();
    }

    public void CloseTab(GameObject tabToClose)
    {
        StartCoroutine(Waiting(tabToClose, false));
    }

    public void OpenTab(GameObject tabToOpen)
    {
        StartCoroutine(Waiting(tabToOpen, true));
    }

    private void OnApplicationQuit()
    {
        delegateHolder.OnWinLevel -= WriteJSON;
    }
}
