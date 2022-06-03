using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InventoryLoader : MonoBehaviour
{
    public GameObject button_prefub;
    public Transform content_btns;
    public JournalInfo journalInfo;
    public EffectsSaver effectsSaver;
    public DialogSaver dialogSaver;

    public Transform newHistoryPin;
    public Transform newEvidPin;
    public Transform newInfoPin;

    public Text historyText;
    public GameObject InfoField;
    public Text infoText;
    public GameObject infoImage;
    public GameObject evidPrefub;
    public GameObject evidLinePrefub;
    public Transform evidContent;


    private int choosenEvidId;
    private GameObject choosenEvidObj;
    private Evidences choosenEvidInfo;
    private int evidListScale;
    private float timeEvidClickOn;

    void Start()
    {
        effectsSaver.setDefault();
        redrawEvidPage();
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

    public void redrawEvidPage()
    {
        List < int[] > existConnections;
        foreach (Evidences evid in journalInfo.playerEvidencesID)
        {
            drawEvid(evid);
        }
    }

    public void addNewEvid()
    {
        choosenEvidId = -1;
        List<int> changeCopy = new List<int>(journalInfo.newInEvid);

        foreach (Transform child in evidContent.GetChild(1).transform)
        {
            int id = Int32.Parse(child.gameObject.name);
            if (changeCopy.Contains(id))
            {
                changeEvidColor(child.gameObject, journalInfo.playerEvidencesID[id]);
                changeCopy.Remove(id);
            }
        }
        if (changeCopy.Count != 0)
        {
            foreach (Evidences evid in journalInfo.playerEvidencesID)
            {
                if (changeCopy.Contains(evid.evidenceID))
                {
                    drawEvid(evid);
                }
            }
            journalInfo.newInEvid = new List<int>();
        }
    }

    public void drawEvid(Evidences evid)
    {
        GameObject newEvid = Instantiate(evidPrefub, evidContent.GetChild(1));
        EvidencesSaver evidInfo = effectsSaver.evidences[evid.evidenceID];
        newEvid.transform.localPosition = new Vector2(evidInfo.startPosition[0], evidInfo.startPosition[1]);
        newEvid.transform.GetChild(0).gameObject.GetComponent<Text>().text = evidInfo.title;
        newEvid.name = evid.evidenceID.ToString();
        changeEvidColor(newEvid, evid);
        EventTrigger trigger = newEvid.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { clickEvidence(evid.evidenceID); });
        trigger.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { unclickedEvidence(evid.evidenceID, newEvid, evid); });
        trigger.triggers.Add(entry);
    }

    public void changeEvidColor(GameObject newEvid, Evidences evid)
    {
        Color evidColor;
        switch (evid.status)
        {
            case 0:
                evidColor = new Color(255, 200, 0, 255);
                break;
            case 1:
                evidColor = new Color(0, 255, 100, 255);
                break;
            case 2:
                evidColor = new Color(0, 100, 255, 255);
                break;
            default:
                evidColor = new Color(255, 255, 255, 255);
                break;
        }
        newEvid.GetComponent<Image>().color = evidColor;
    }

    public void clickEvidence(int ID)
    {
        timeEvidClickOn = Time.time;
    }

    public void unclickedEvidence(int ID, GameObject evidObj, Evidences evid)
    {
        float timePass = Time.time - timeEvidClickOn;
        if (timePass > 0.7f)
        {
            Debug.Log("timePass " + timePass.ToString()); //в ожидании дизайна уллик
        }
        else
        {
            if (choosenEvidId != -1)
            {
                changeEvidColor(choosenEvidObj, choosenEvidInfo);
                if (effectsSaver.evidences[ID].connectionList.Contains(choosenEvidId))
                {
                    int connectionId = effectsSaver.evidences[ID].connectionList.IndexOf(choosenEvidId);
                    dialogSaver.effectProceess(effectsSaver.evidences[ID].effects[connectionId]);
                    addNewEvid();
                }
                choosenEvidId = -1;
            }
            else
            {
                choosenEvidId = ID;
                evidObj.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                choosenEvidObj = evidObj;
                choosenEvidInfo = evid;
            }
        }
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

        if (journalInfo.newInEvid.Count != 0)
        {
            newEvidPin.gameObject.SetActive(true);
            newEvidPin.GetChild(0).GetComponent<Text>().text = journalInfo.newInEvid.Count.ToString();
        }
        else { newEvidPin.gameObject.SetActive(false); }

        if (journalInfo.newInInfo.Count != 0)
        {
            newInfoPin.gameObject.SetActive(true);
            newInfoPin.GetChild(0).GetComponent<Text>().text = journalInfo.newInInfo.Count.ToString();
        }
        else { newInfoPin.gameObject.SetActive(false); }
    }

    public void removeEvidPin()
    {
        journalInfo.newInEvid = new List<int>();
        newEvidPin.gameObject.SetActive(false);
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
