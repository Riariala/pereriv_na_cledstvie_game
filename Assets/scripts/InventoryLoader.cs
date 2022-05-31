using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryLoader : MonoBehaviour
{
    public GameObject button_prefub;
    public Transform content_btns;
    public JournalInfo journalInfo;
    public EffectsSaver effectsSaver;

    public Transform newHistoryPin;
    public Transform newEvidPin;
    public Transform newInfoPin;

    public Text historyText;
    public GameObject InfoField;
    public Text infoText;
    public GameObject infoImage;


    void Start()
    {
        effectsSaver.setDefault();
    }

    public void updateHistory()
    {
        string history = "";
        foreach (int historyID in journalInfo.playerHistoryID)
        {
            history += " Х " + effectsSaver.history[historyID] + "\n";
        }
        historyText.GetComponent<Text>().text = history;
    }

    public void updateInfo()
    {
        if (journalInfo.playerInfoID.Count != 0)
        {
            List<int> changeCopy = new List<int>(journalInfo.newInInfo);
            string test = "";
            foreach(int f in changeCopy)
            {
                test += f.ToString() + " ";
            }
            Debug.Log("Tested before " + test);
            foreach (Transform child in content_btns.GetChild(1).transform)
            {
                Debug.Log(content_btns.gameObject.name);
                Debug.Log(child.gameObject.name);
                int childId = Int32.Parse(child.gameObject.name);
                if (changeCopy.Contains(childId))
                {
                    child.GetChild(1).gameObject.SetActive(true);
                }
                changeCopy.Remove(childId);
            }
            foreach (Transform child in content_btns.GetChild(3).transform)
            {
                int childId = Int32.Parse(child.gameObject.name);
                if (changeCopy.Contains(childId))
                {
                    child.GetChild(1).gameObject.SetActive(true);
                }
                changeCopy.Remove(childId);
            }
            foreach (Transform child in content_btns.GetChild(5).transform)
            {
                int childId = Int32.Parse(child.gameObject.name);
                if (changeCopy.Contains(childId))
                {
                    child.GetChild(1).gameObject.SetActive(true);
                }
                changeCopy.Remove(childId);
            }
            Transform parent;
            test = "";
            foreach (int f in changeCopy)
            {
                test += f.ToString() + " ";
            }
            Debug.Log("Tested after " + test);
            foreach (var btnId in changeCopy)
            {
                switch (effectsSaver.info[btnId].type)
                {
                    case "Ћичности":
                        parent = content_btns.GetChild(1);
                        break;
                    case "ћеста":
                        parent = content_btns.GetChild(3);
                        break;

                    case " ультура":
                        parent = content_btns.GetChild(5);
                        break;
                    default:
                        parent = content_btns;
                        break;
                }
                GameObject btn = Instantiate(button_prefub, parent);
                btn.transform.GetChild(0).GetComponent<Text>().text = effectsSaver.info[btnId].title;
                btn.GetComponent<Button>().onClick.AddListener(() => callInfoPage(btnId));
                btn.GetComponent<Button>().onClick.AddListener(() => removeMarkFrom(btn));
                btn.transform.GetChild(1).gameObject.SetActive(true);
                btn.name = btnId.ToString();
            }
        }
    }

    public void removeMarkFrom(GameObject marked)
    {
        marked.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void callInfoPage(int ID)
    {
        InfoField.SetActive(true);
        //infoText.text = "";
        string newText = "";
        List<int> infoLines = new List<int>();
        foreach (InfoDeteiledID infoid in journalInfo.playerInfoID)
        {
            if (infoid.InfoId == ID)
            {
                infoLines = new List<int>(infoid.linesId);
                break;
            }
        }
        string f = "";
        foreach (int d in infoLines)
        {
            f += d.ToString() + " ";
        }
        Debug.Log(f);
        foreach (int lineid in infoLines)
        {
            newText += effectsSaver.info[ID].lines[lineid] + "\n";
            //infoText.text += effectsSaver.info[ID].lines[lineid] + "\n";
        }
        if (effectsSaver.info[ID].picture != "")
        {
            infoImage.SetActive(true);
        }
        else
        {
            infoImage.SetActive(false);
        }
        InfoField.transform.GetChild(0).GetComponent<Text>().text = effectsSaver.info[ID].title;
        infoText.text = newText;
    }

    public void closeInfoField()
    {
        InfoField.SetActive(false);
    }

    public void callInfoType(GameObject menu)
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    public void changeArrow(Transform calld_btn)
    {
        calld_btn.GetChild(1).GetChild(0).gameObject.SetActive(!calld_btn.GetChild(1).GetChild(0).gameObject.activeInHierarchy);
        calld_btn.GetChild(1).GetChild(1).gameObject.SetActive(!calld_btn.GetChild(1).GetChild(1).gameObject.activeInHierarchy);
    }

    public void markPins()
    {
        if (journalInfo.newInHistory != 0)
        {
            newHistoryPin.gameObject.SetActive(true);
            newHistoryPin.GetChild(0).GetComponent<Text>().text = journalInfo.newInHistory.ToString();
        }
        else { newHistoryPin.gameObject.SetActive(false); }

        //if (journalInfo.newInEvid.Count != 0)
        //{
        //    newEvidPin.gameObject.SetActive(true);
        //    newEvidPin.GetChild(0).GetComponent<Text>().text = journalInfo.newInEvid.Count.ToString();
        //}
        //else { newEvidPin.gameObject.SetActive(false); }

        if (journalInfo.newInInfo.Count != 0)
        {
            newInfoPin.gameObject.SetActive(true);
            newInfoPin.GetChild(0).GetComponent<Text>().text = journalInfo.newInInfo.Count.ToString();
        }
        else { newInfoPin.gameObject.SetActive(false); }
    }

    public void removeHistoryPin()
    {
        journalInfo.newInHistory = 0;
        newHistoryPin.gameObject.SetActive(false);
    }

    public void removeInfoPin()
    {
        journalInfo.newInInfo = new List<int>();
        newInfoPin.gameObject.SetActive(false);
    }
}
