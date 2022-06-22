using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UdpKit;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class MenuScript : MonoBehaviour
{
    public GameObject first_menu;
    public GameObject second_menu;
    public GameObject third_menu;
    public GameObject fourth_menu;
    public GameObject new_game_modal;
    public GameObject store_menu;
    public GameObject reg_menu;
    public GameObject auth_menu;
    public Transform loading_menu;
    public Text load_inform_text;
    public ActionsSaver actionsSaver;
    public DialogSaver dialogSaver;
    public PlayerData playerData;
    public JournalInfo journalInfo;
    public PlayersDialogiesSaver playersDialogiesSaver;
    public GameObject refresh_btn;
    private string[] loadInformTexts = new string[] { "Во время одиночной игры можно свободно переключаться между персонажами.",
            "Запретив незнакомых игроков, вы не позволяете присоединяться к игре из лобби - только по коду подключения.",
            "Вы не сможете начать игру с напарником, пока не поднлючится второй игрок - придется ожидать.",
            "При игре без напарника, но с разрешением подключаться незнакомцам, вы можете играть в одиночном режиме, пока не присоединится второй игрок.",
            "Подключаясь к готовой сессии, вы займете свободного персонажа."};

    public Menu NetworkMenu;

    private bool _isCoworker;
    private bool _isUnknowns;

    public bool isCoworker {get {return _isCoworker;} set {_isCoworker = value;} }
    public bool isUnknowns {get {return _isUnknowns;} set {_isUnknowns = value;} }

    void Start()
    {
        dialogSaver.setDefault();
        playersDialogiesSaver.setDefault();
    }

    public void changeCoworker(GameObject checkp) 
    {
        isCoworker = !isCoworker;
        checkp.SetActive(!checkp.activeSelf);
    }

    public void changeUnknowns(GameObject checkp) 
    {
        isUnknowns = !isUnknowns;
        checkp.SetActive(!checkp.activeSelf);
    }

    public void openModal(GameObject modal_menu)
    {
        modal_menu.SetActive(true);
        foreach (Transform child in first_menu.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Button")) {child.GetComponent<Button>().interactable = false;}
        }
        foreach (Transform child in fourth_menu.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Button")) {child.GetComponent<Button>().interactable = false;}

        }
    }

    public void closeModal(GameObject modal_menu)
    {
        StartCoroutine(closing_modals(modal_menu));
        foreach (Transform child in first_menu.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Button")) {child.GetComponent<Button>().interactable = true;}
        }
        foreach (Transform child in fourth_menu.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Button")) {child.GetComponent<Button>().interactable = true;}
        }
    }

    public void gameExit()
    {
        Debug.Log("Exit_game");
        Application.Quit();
    }

    public void goToFirstMenu(GameObject prev_menu)
    {
        StartCoroutine(changing_menu(prev_menu, first_menu));
    }

    public void goToSecMenu(GameObject prev_menu)
    {
        StartCoroutine(changing_menu(prev_menu, second_menu));
    }

    public void goToThirdMenu(GameObject prev_menu)
    {
        StartCoroutine(changing_menu(prev_menu, third_menu));
    }

    public void goToFourthMenu(GameObject prev_menu)
    {
        StartCoroutine(changing_menu(prev_menu, fourth_menu));
        refresh_btn.SetActive(true);
    }

    public void goToStorehMenu(GameObject prev_menu)
    {
        StartCoroutine(changing_menu(prev_menu, store_menu));
    }

    public void goToRegMenu(GameObject prev_menu)
    {
        StartCoroutine(changing_menu(prev_menu, reg_menu));
    }
    public void goToAuthMenu(GameObject prev_menu)
    {
        StartCoroutine(changing_menu(prev_menu, auth_menu));
    }

    public void createNewGame()
    {
        if (!isCoworker)
        {
            if (_isUnknowns) 
            { 
                playerData.gametype = 3;
                NetworkMenu.StartServer();
            } 
            else 
            {    
                playerData.gametype = 0;
                if (BoltNetwork.IsClient)
                {
                    BoltLauncher.Shutdown();
                }
                SceneManager.LoadScene("level0");
            }
        }
        else 
        {
            if (_isUnknowns) { playerData.gametype = 2; } else { playerData.gametype = 1; }
            new_game_modal.SetActive(true);
            foreach (Transform child in fourth_menu.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("Button")) {child.GetComponent<Button>().interactable = true;}                
            }
            NetworkMenu.StartServer();
        }
        actionsSaver.setDefault();
        journalInfo.clearAll();
        playersDialogiesSaver.setDefault();
    }

    public void OpenLoadingMenu()
    {
        loading_menu.gameObject.SetActive(true);
        switch (playerData.gametype)
        {
            case 0:
                loading_menu.GetChild(0).gameObject.SetActive(false);
                loading_menu.GetChild(1).gameObject.SetActive(true);
                load_inform_text.text = loadInformTexts[0];
                break;
            case 1:
                loading_menu.GetChild(0).gameObject.SetActive(true);
                loading_menu.GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                loading_menu.GetChild(0).gameObject.SetActive(true);
                loading_menu.GetChild(1).gameObject.SetActive(false);
                break;
            case 3:
                loading_menu.GetChild(0).gameObject.SetActive(false);
                loading_menu.GetChild(1).gameObject.SetActive(true);
                load_inform_text.text = loadInformTexts[3];
                break;
            default:
                loading_menu.GetChild(0).gameObject.SetActive(false);
                loading_menu.GetChild(1).gameObject.SetActive(true);
                load_inform_text.text = loadInformTexts[4];
                break;
        }
    }

    public void closeLoading()
    {
        loading_menu.gameObject.SetActive(false);
    }

    IEnumerator closing_modals(GameObject modal_menu)
    {
        modal_menu.GetComponent<Animation>().Play("modal_disappear");
        yield return new WaitForSeconds(0.25f);
        modal_menu.SetActive(false);
    }

    IEnumerator changing_menu(GameObject prev_menu, GameObject cur_menu)
    {
        yield return new WaitForSeconds(0.25f);
        prev_menu.SetActive(false);
        cur_menu.SetActive(true);
    }




}
