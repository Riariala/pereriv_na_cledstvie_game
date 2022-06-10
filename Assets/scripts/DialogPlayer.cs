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
    public NetworkCallbacks callbacks;

    void Update()
    {
        if (callbacks.click)
        {
            if (callbacks.next)
            {
                if (isHost = false)
                {
                    PlayNextCall();

                }
                else
                {
                    var next = NextDialog.Create();
                    next.Next = false;
                    next.Send();
                }
            }
        }
    }


    public void beginDialog(int ObjId, int actionKind)
    {
        dialogSaver.playerData.isBusy = true;
        dialogPanel.SetActive(true);
        ObjectId = ObjId;
        dialogId = dialogSaver.AskDialogId(ObjectId, actionKind);
        massageText = dialogSaver.AskDialog(ObjectId, dialogId);
        titleText = dialogSaver.AskTitle(ObjectId, dialogId);
        isPlayer1 = dialogSaver.whichPlayer();
        dialogCount = 0;
        Debug.Log(ObjectId);
        Debug.Log(dialogId);
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
        if (isHost) //ÚÓ„‰‡ ‡·ÓÚ‡ÂÚ
        {
                var next = NextDialog.Create();
                next.Next = true;
                next.Send();
                //¬Œ“ «ƒ≈—‹ Œ“œ–¿¬‹  ŒÃ¿Õƒ” ¬“Œ–ŒÃ” »√–Œ ”,◊“Œ¡€ Ã≈ÕﬂÀ. ¬€«¬¿“‹ ” Õ≈√Œ  ŒÃ¿Õƒ” PlayNextCall();
                PlayNextCall();
        }
    }

    public void PlayNextCall()
    {
        if (dialogCount < isFirstTalkList.Count)
        {
            massage_txt.text = massageText[dialogCount];
            string name;
            if (isFirstTalkList[dialogCount]) 
            { 
                name = "–Ó‰ÊÂÒ";
                if (isHost) { name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleRight; }
                else { name_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleLeft; }
                
            } 
            else 
            {
                name = "Ã‡Ë";
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
            playEffect(playersDialogiesSaver.dialogiesList[dialogId].effect);
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
                if (isPlayer1) { name = "–Ó‰ÊÂÒ"; } else { name = "Ã‡Ë"; }
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
            dialogId = 0;
        }
    }

    public void playEffect(int lastEffectid)
    {
        lastEffectID = lastEffectid;
        int dialogVariantID = dialogSaver.effectChangesSaver.effectsChanges[lastEffectID].dialog_variant_play;
        if (dialogVariantID != 0)
        {
            dialogSaver.playerData.isBusy = true;
            simple_dialog_panel.SetActive(false);
            variable_dialog_panel.SetActive(true);
            for (int i = 0; i < dialogSaver.dialogVariantsSaver.variants[dialogVariantID].optionLines.Count; i++)
            {
                Transform child = variable_dialog_panel.transform.GetChild(i);
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
        }
        dialogSaver.effectProceess(lastEffectID);
    }

    public void textClicked()
    {
        dialogSaver.clickedEffectFind(ObjectId, dialogId, dialogCount);
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
        if (! dialogSaver.IsdialogOver) { beginDialog(ObjectId, 0); }
        else { dialogSaver.playerData.isBusy = false; }
    }

    public void beginPlayersDialog(int locdialogId)
    {
        dialogSaver.playerData.isBusy = true;
        dialogPanel.SetActive(true);
        massageText = playersDialogiesSaver.AskDialog(locdialogId);
        isFirstTalkList = playersDialogiesSaver.AskTitles(locdialogId);
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
        Debug.Log("Ì‡˜‡ÎÓ ‰Ë‡ÎÓ„‡ ÏÂÊ‰Û Ë„ÓÍ‡ÏË");
        playNext();
    }


}
