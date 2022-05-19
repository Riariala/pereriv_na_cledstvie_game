using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;

[CreateAssetMenu(fileName = "DialogSaver", menuName = "Dialog Saver", order = 51)]
public class DialogSaver : ScriptableObject
{
    public ActionsSaver actionsSaver;
    public PlayerData playerData;
    public JournalInfo journalInfo;
    public EffectChangesSaver effectChangesSaver;

    public List<ObjectDialogs> objects;

    public void setDefault()
    {
        string _path = Application.dataPath + "/Resour/" + "ObjectDialogs.json";
        Debug.Log(_path);
        objects = readFromJSON(_path).objects;
        effectChangesSaver.setDefault();
    }

    public DialogesHolder readFromJSON(string _path)
    {
        return JsonConvert.DeserializeObject<DialogesHolder>(File.ReadAllText(_path, Encoding.GetEncoding("utf-8")));
    }

    public void ReplaceActionSaver(int ObjectID, int ActionID)
    {
        foreach (ObjectActions change in objects[ObjectID].changes[ActionID])
        {
            actionsSaver.Rewrite(change.ID, change.firstPlayerActs, change.secPlayerActs);
        }
    }

    public void clickedEffectProcess(int objID, int dialogID, int lineID)
    {
        int effectID = -1;
        foreach (ChoosingDialogs choosdia in objects[objID].clickedEffect[dialogID])
        {
            if (choosdia.queue == lineID)
            {
                effectID = choosdia.effect;
                break;
            }
        }
        if (effectID > -1)
        {
            Debug.Log("effectID" + effectID);
            Effectschanges effect = effectChangesSaver.takeEffect(effectID);
            if (effect.history.Count != 0) { journalInfo.addHistory(effect.history); }
            if (effect.evidences.Count != 0) 
            { 
                foreach (Evidences evid in effect.evidences)
                {
                    journalInfo.changeEvidenceStatus(evid.evidenceID, evid.status);
                }
            }
            if (effect.info.Count != 0)
            {
                foreach (InfoDeteiledID inf in effect.info)
                {
                    journalInfo.addToPersonInfo(inf.InfoId, inf.linesId);
                }
            }
        }

    }

    public List<string> AskDialog(int ObjectID, int dialogId)
    {
        return objects[ObjectID].dialogs[dialogId];
    }

    public List<string> AskTitle(int ObjectID, int dialogId)
    {
        return objects[ObjectID].titles[dialogId];
    }

    public int AskDialogId(int ObjectID, int actionKind)
    {
        int dialogId;
        if (playerData.isPlayer1) { dialogId = actionsSaver.itemPlayerActions[ObjectID].firstPlayerActs[actionKind]; }
        else { dialogId = actionsSaver.itemPlayerActions[ObjectID].firstPlayerActs[actionKind]; }
        return dialogId;
    }

    public bool whichPlayer()
    {
        return playerData.isPlayer1;
    }
}
