using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryLoader : MonoBehaviour
{
    public JournalInfo journalInfo;
    public EffectsSaver effectsSaver;

    public Transform newHistoryPin;
    public Transform newEvidPin;
    public Transform newInfoPin;

    public Text historyText;


    void Start()
    {
        effectsSaver.setDefault();
        updateHistory(0);
    }

    public void updateHistory(int newsPinAdd)
    {
        string history = "";
        foreach (int historyID in journalInfo.playerHistoryID)
        {
            history += " • " + effectsSaver.history[historyID] + "\n";
        }
        historyText.GetComponent<Text>().text = history;
        int newsPinCount = Int32.Parse(newHistoryPin.GetChild(0).GetComponent<Text>().text) + newsPinAdd;
        newHistoryPin.GetChild(0).GetComponent<Text>().text = newsPinCount.ToString();
        if (newsPinCount != 0) { newHistoryPin.gameObject.SetActive(true); }
        else { newHistoryPin.gameObject.SetActive(false); }
    }

    public void removeNewsPin(GameObject pin)
    {
        pin.transform.GetChild(0).GetComponent<Text>().text = "0";
        pin.SetActive(false);
    }
}
