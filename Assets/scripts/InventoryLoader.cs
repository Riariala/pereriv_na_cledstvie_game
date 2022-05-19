using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLoader : MonoBehaviour
{
    public JournalInfo journalInfo;
    public EffectsSaver effectsSaver;

    public Text historyText;


    void Start()
    {
        effectsSaver.setDefault();

    }

    public void updateHistory()
    {
        string history = "";
        foreach (int historyID in journalInfo.playerHistoryID)
        {
            history += " • " + effectsSaver.history[historyID] + "\n";
        }
        historyText.GetComponent<Text>().text = history;
    }
}
