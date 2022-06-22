using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject loading_menu;
    public Text load_code_text;
    public PlayerData playerData;

    void Start()
    {
        bool meaning = playerData.gametype == 1 || playerData.gametype == 2;
        showLoading(meaning);
        if (playerData.gametype != 0 && playerData.gametype != 4)
        { 
            load_code_text.text = playerData.gameCode; 
        }

    }

    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
        BoltLauncher.Shutdown();
    }

    public void showLoading(bool meaning)
    {
        loading_menu.SetActive(meaning);
    }

}
