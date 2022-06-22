using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class DialogPlayer : Photon.Bolt.EntityBehaviour<ICustomPlayer>//MonoBehaviour
{
    public GameObject dialogPanel;
    public Text title_txt;
    public Text massage_txt;
    public GameObject name_panel;
    public GameObject simple_dialog_panel;
    public GameObject variable_dialog_panel;
    public DialogSaver dialogSaver;
    public PlayersDialogiesSaver playersDialogiesSaver;

    public GameObject massage_click_btn;
    private List<string> titleText;
    private List<bool> isFirstTalkList;
    private List<string> massageText;
    private int dialogCount;
    private bool isPlayer1;
    private int ObjectId;
    private int dialogId;
    private int lastEffectID;
    private bool isPlayersDialog;
    private bool isHost;
    private int lastActionKind;
    public NetworkCallbacks callbacks;
    public Transform gameOverMenu;
    public bool isOverInit;

    void Update()
    {

        if (dialogSaver.playerData.gametype != 0)
        {
            if (callbacks.next)
            {
                if (!isHost)
                {
                    PlayNextCall();
                }
                callbacks.next = false;
            }


            if (callbacks.isGameOver)
            {
                if (isOverInit)
                {
                    if (callbacks.isOverAns)
                    {
                        gameOverMenu.GetChild(1).gameObject.SetActive(true);
                    }
                }
                else
                {
                    int ind;
                    if (dialogSaver.playerData.isPlayer1) { ind = 0; } else { ind = 1; }
                    if (dialogSaver.playerData.isGameOver[ind])
                    {
                        var isOverAns = IsGameOverAns.Create();
                        isOverAns.IsOverAns = dialogSaver.playerData.isGameOver[ind];
                        isOverAns.Send();
                        gameOverMenu.GetChild(1).gameObject.SetActive(true);
                    }
                }

            }
        }
    }

    void FixedUpdate()
    {
        int ind;
        if (dialogSaver.playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        if (dialogSaver.playerData.isGameJustStarted[ind])
        {
            dialogSaver.playerData.isGameJustStarted[ind] = false;
            beginDialog(0, 0);
        }
    }

    public void beginDialog(int ObjId, int actionKind)
    {
        dialogSaver.playerData.isBusy = true;
        dialogPanel.SetActive(true);
        ObjectId = ObjId;
        lastActionKind = actionKind;
        dialogId = dialogSaver.AskDialogId(ObjectId, lastActionKind);
        massageText = dialogSaver.AskDialog(ObjectId, dialogId);
        titleText = dialogSaver.AskTitle(ObjectId, dialogId);
        isPlayer1 = dialogSaver.whichPlayer();
        dialogCount = 0;
        isPlayersDialog = false;
        playNext();
    }

    public void playNext()
    {
        if (isPlayersDialog) { PlayNextPlayersDialog(); }
        else { PlayNextSimpleDialog(); }
    }

    public void PlayNextPlayersDialog()
    {
        if (dialogSaver.playerData.gametype != 0)
        {
            if (isHost)
            {
                var next = NextDialog.Create();
                next.Next = true;
                next.Send();
                PlayNextCall();
            }
        }
        else { PlayNextCall(); }
    }

    public void PlayNextCall()
    {
        if (dialogCount < isFirstTalkList.Count)
        {
            massage_txt.text = massageText[dialogCount];
            string name;
            if (isFirstTalkList[dialogCount]) 
            { 
                name = "Роджерс";
                if (isHost) { name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleRight; }
                else { name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleLeft; }
                
            } 
            else 
            {
                name = "Мари";
                if (isHost) { name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleLeft; }
                else { name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleRight; }
            }
            title_txt.text = name;
            dialogCount++;
        }
        else
        {
            dialogCount = 0;
            dialogPanel.SetActive(false);
            dialogSaver.ReplaceActionSaver(playersDialogiesSaver.dialogiesList[dialogId].changes);
            dialogSaver.playerData.isBusy = false;
            int effectid;
            if (dialogSaver.playerData.isPlayer1) { effectid = 0;  } else { effectid = 1; }
            playEffect(playersDialogiesSaver.dialogiesList[dialogId].effect[effectid]);
            dialogSaver.playerData.dialogId = playersDialogiesSaver.dialogiesList[dialogId].nextDialog;
        }
    }

    public void PlayNextSimpleDialog()
    {
        if (dialogCount < titleText.Count)
        {
            massage_txt.text = massageText[dialogCount];
            if (titleText[dialogCount] == "player")
            {
                string name;
                if (isPlayer1) { name = "Роджерс"; } else { name = "Мари"; }
                title_txt.text = name;
                name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
            }
            else
            {
                name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleRight;
                title_txt.GetComponent<Text>().text = titleText[dialogCount];
            }
            if (dialogSaver.objects[ObjectId].clickedEffect[dialogId].Count > 0)
            {
                massage_click_btn.SetActive(true);
            }
            else { massage_click_btn.SetActive(false); }
            dialogCount++;
        }
        else
        {
            dialogCount = 0;
            dialogPanel.SetActive(false);
            dialogSaver.ReplaceActionSaver(ObjectId, dialogId);
            dialogSaver.playerData.isBusy = false;
            playEffect(dialogSaver.takeEffectId(ObjectId, dialogId));
            checkGameOver();
            dialogId = 0;
        }
    }

    public void playEffect(int lastEffectid)
    {
        lastEffectID = lastEffectid;
        int dialogVariantID = dialogSaver.effectChangesSaver.effectsChanges[lastEffectID].dialog_variant_play;
        dialogSaver.effectProceess(lastEffectID);
        if (dialogVariantID != 0)
        {
            dialogSaver.playerData.isBusy = true;
            simple_dialog_panel.SetActive(false);
            variable_dialog_panel.SetActive(true);
            dialogPanel.SetActive(true);
            int dialogCount = dialogSaver.dialogVariantsSaver.variants[dialogVariantID].optionLines.Count;
            for (int i = 0; i < 3; i++)
            {
                Transform child = variable_dialog_panel.transform.GetChild(i);
                if (i < dialogCount)
                {
                    child.gameObject.SetActive(true);
                    child.GetChild(0).gameObject.GetComponent<Text>().text = dialogSaver.dialogVariantsSaver.variants[dialogVariantID].optionLines[i];
                    if (dialogSaver.dialogVariantsSaver.variants[dialogVariantID].available[i])
                    {
                        child.gameObject.GetComponent<Button>().interactable = true;
                        child.GetChild(0).gameObject.GetComponent<Text>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
                    }
                    else
                    {
                        child.gameObject.GetComponent<Button>().interactable = false;
                        child.GetChild(0).gameObject.GetComponent<Text>().color = new Color(128 / 255f, 128 / 255f, 128 / 255f);
                    }
                }
                else { child.gameObject.SetActive(false); }
            }
        }
        else { dialogSaver.IsdialogOver = true; }
        checkGameOver();
    }

    public void checkGameOver()
    {
        int ind;
        if (dialogSaver.playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        if (dialogSaver.IsdialogOver && dialogSaver.isGameOverloc)
        {
            dialogSaver.playerData.isGameOver[ind] = dialogSaver.isGameOverloc;
            dialogSaver.IsdialogOver = false;
            dialogSaver.isGameOverloc = false;
        }
        if (dialogSaver.playerData.gametype != 0)
        {
            if (dialogSaver.playerData.isGameOver[ind] && !callbacks.isGameOver)
            {
                var isGameOver = IsGameOverCheck.Create();
                isGameOver.IsGameOver = true;
                isGameOver.Send();
                isOverInit = true;
                gameOverMenu.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            if (dialogSaver.playerData.isGameOver[ind])
            {
                gameOverMenu.GetChild(0).gameObject.SetActive(true);
            }
            else
            { gameOverMenu.GetChild(0).gameObject.SetActive(false); }
            if (dialogSaver.playerData.isGameOver[0] && dialogSaver.playerData.isGameOver[1])
            {
                gameOverMenu.GetChild(1).gameObject.SetActive(true);
            }
        }
    }


    public void textClicked()
    {
        dialogSaver.clickedEffectFind(ObjectId, dialogId, dialogCount-1);
    }

    public void dialogVariantClicked(int btn_ind)
    {
        int dialogVariantID = dialogSaver.effectChangesSaver.effectsChanges[lastEffectID].dialog_variant_play;
        dialogSaver.dialogVariantEffect(dialogVariantID, btn_ind);
        for (int i = 0; i < 3; i++)
        {
            variable_dialog_panel.transform.GetChild(i).gameObject.SetActive(false);
        }
        simple_dialog_panel.SetActive(true);
        variable_dialog_panel.SetActive(false);
        if (dialogSaver.IsdialogOver) {
            checkGameOver();
            dialogSaver.playerData.isBusy = false;
            dialogPanel.SetActive(false);
        }
        else 
        {
            beginDialog(ObjectId, lastActionKind);
        }
    }

    public void beginPlayersDialog(int locdialogId)
    {
        dialogSaver.IsdialogOver = false;
        dialogSaver.playerData.isBusy = true;
        dialogPanel.SetActive(true);
        massageText = playersDialogiesSaver.AskDialog(locdialogId);
        Debug.Log(massageText[0]);
        isFirstTalkList = playersDialogiesSaver.AskTitles(locdialogId);
        Debug.Log(isFirstTalkList[0]);
        dialogId = locdialogId;
        isPlayersDialog = true;
        if (BoltNetwork.IsServer)
        {
            isHost=true;
        }
        else
        {
            isHost = false;
        }
        dialogCount = 0;
        playNext();
    }
}
