using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlayer : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject title_txt;
    public Text massage_txt;
    public DialogSaver dialogSaver;

    private List<string> titleText;
    private List<string> massageText;
    private int dialogCount;
    private bool isPlayer1;
    private int ObjectId;
    private int dialogId;


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
                if (isPlayer1) { name = "Player1"; } else { name = "Player2"; }
                title_txt.GetComponent<Text>().text = name;
                title_txt.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            }
            else
            {
                title_txt.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                title_txt.GetComponent<Text>().text = titleText[dialogCount];
            }
            dialogCount++;
        }
        else 
        {
            dialogCount = 0;
            dialogPanel.SetActive(false);
            dialogSaver.ReplaceActionSaver(ObjectId, dialogId);
        }
    }
}
