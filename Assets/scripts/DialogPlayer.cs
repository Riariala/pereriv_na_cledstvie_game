using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlayer : MonoBehaviour
{
    public GameObject dialogPanel;
    public Text title_txt;
    public Text massage_txt;
    public GameObject name_panel;
    public GameObject simple_dialog_panel;
    public GameObject variable_dialog_panel;
    public DialogSaver dialogSaver;

    public GameObject massage_click_btn;
    private List<string> titleText;
    private List<string> massageText;
    private int dialogCount;
    private bool isPlayer1;
    private int ObjectId;
    private int dialogId;
    private int lastEffectID;


    public void beginDialog(int ObjId, int actionKind)
    {
        dialogPanel.SetActive(true);
        ObjectId = ObjId;
        dialogId = dialogSaver.AskDialogId(ObjectId, actionKind);
        massageText = dialogSaver.AskDialog(ObjectId, dialogId);
        titleText = dialogSaver.AskTitle(ObjectId, dialogId);
        isPlayer1 = dialogSaver.whichPlayer();
        dialogCount = 0;
        Debug.Log(ObjectId);
        Debug.Log(dialogId);
        playNext();
    }

    public void playNext()
    {
        Debug.Log(titleText.Count);
        Debug.Log(dialogCount);
        if (dialogCount < titleText.Count)
        {
            massage_txt.text = massageText[dialogCount];
            if (titleText[dialogCount] == "player")
            {
                string name;
                if (isPlayer1) { name = "Роджерс"; } else { name = "Мери"; }
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
            playEffect();
        }
    }

    public void playEffect()
    {
        lastEffectID = dialogSaver.takeEffectId(ObjectId, dialogId);
        int dialogVariantID = dialogSaver.effectChangesSaver.effectsChanges[lastEffectID].dialog_variant_play;
        if (dialogVariantID != 0)
        {
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
        Debug.Log("textClicked");
    }

    public void dialogVariantClicked(int btn_ind)
    {
        //int effectId = dialogSaver.takeEffectId(ObjectId, dialogId);
        int dialogVariantID = dialogSaver.effectChangesSaver.effectsChanges[lastEffectID].dialog_variant_play;
        dialogSaver.dialogVariantEffect(dialogVariantID, btn_ind);
        for (int i = 0; i < 3; i++)
        {
            variable_dialog_panel.transform.GetChild(i).gameObject.SetActive(false);
        }
        simple_dialog_panel.SetActive(true);
        variable_dialog_panel.SetActive(false);
        if (! dialogSaver.IsdialogOver) { beginDialog(ObjectId, 0); }
    }
}
