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
    public DialogVariantsSaver dialogVariantsSaver;

    public List<ObjectDialogs> objects;

    private bool isdialogOver;

    public bool IsdialogOver {get { return isdialogOver;  } set { isdialogOver = value; } }

    public void setDefault()
    {
        IsdialogOver = false;
        objects = readFromJSON();
        effectChangesSaver.setDefault();
        dialogVariantsSaver.setDefault();
        journalInfo.newInHistory = 0;
        journalInfo.newInEvid = new List<int>();
        journalInfo.newInInfo = new List<int>();

    }

    public List<ObjectDialogs> readFromJSON()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string _path = Application.streamingAssetsPath + "/ObjectDialogs.json";
        WWW reader = new WWW(_path);
        while (!reader.isDone) { }
        if ( reader.error != null )
        {
            Debug.LogError("error : " + _path);
            return new List<ObjectDialogs>();
        }
        string file = reader.text;
#endif


#if !UNITY_ANDROID || UNITY_EDITOR
        string _path = Application.dataPath + "/StreamingAssets/" + "ObjectDialogs.json";
        string file = File.ReadAllText(_path, Encoding.UTF8);
#endif
        DialogesHolder itm = JsonConvert.DeserializeObject<DialogesHolder>(file);
        return itm.objects;
    }

    public void ReplaceActionSaver(int ObjectID, int ActionID)
    {
        foreach (ObjectActions change in objects[ObjectID].changes[ActionID])
        {
            actionsSaver.Rewrite(change.ID, change.firstPlayerActs, change.secPlayerActs);
        }
    }

    public void ReplaceActionSaver(List<ObjectActions> newActions)
    {
        foreach (ObjectActions change in newActions)
        {
            actionsSaver.Rewrite(change.ID, change.firstPlayerActs, change.secPlayerActs);
        }
    }

    public int takeEffectId(int objID, int dialogID)
    {
        int effectID = objects[objID].effects[dialogID];
        return effectID;
        //if (effectID != 0) { effectProceess(effectID); }
    }

    public void clickedEffectFind(int objID, int dialogID, int lineID)
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
        Debug.Log("clickedEffectFind " + objID.ToString() + " " + dialogID.ToString() + " " + lineID.ToString());
        if (effectID > -1)
        {
            effectProceess(effectID);
        }
    }

    public void effectProceess(int effectID)
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
        Debug.Log(" effect.dialog_open.Count " + effect.dialog_open.Count.ToString());
        if (effect.dialog_open.Count != 0)
        {
            foreach (OpenedDialogs opndia in effect.dialog_open)
            {
                Debug.Log(" opndia: " + opndia.dialogId.ToString() + " " + opndia.variantId.ToString() + " " + opndia.newMeaning.ToString());
                dialogVariantsSaver.changeAvailable(opndia.dialogId, opndia.variantId, opndia.newMeaning);
            }
        }
    }

    public void dialogVariantEffect(int diavarID, int variantIndex)
    {
        foreach (ObjectActions changes in dialogVariantsSaver.variants[diavarID].changes[variantIndex])
        {
            actionsSaver.Rewrite(changes.ID, changes.firstPlayerActs, changes.secPlayerActs);
        }
        int effectID = dialogVariantsSaver.variants[diavarID].effects[variantIndex];
        effectProceess(effectID);

        if (dialogVariantsSaver.variants[diavarID].specailActions.Count > 0)
        {
            foreach (List<int> special in dialogVariantsSaver.variants[diavarID].specailActions)
            {
                if (special[0] == variantIndex)
                {
                    switch (special[1])
                    {
                        case 0:
                            IsdialogOver = true;
                            Debug.Log(" IsdialogOver " + IsdialogOver);
                            break;
                        case 1:
                            string name;
                            if (playerData.isPlayer1) { name = "Rogers"; } else { name = "Mary"; }
                            Debug.Log(" This game is over for " + name);
                            break;

                        case 2:
                            break;
                    }
                }
                
            }
        }

    }


    public List<string> AskDialog(int ObjectID, int dialogId)
    {
        IsdialogOver = false;
        return objects[ObjectID].dialogs[dialogId];
    }

    public List<string> AskTitle(int ObjectID, int dialogId)
    {
        return objects[ObjectID].titles[dialogId];
    }

    public int AskDialogId(int ObjectID, int actionKind)
    {
        int dialogId;
        if (playerData.isPlayer1) 
        { 
            dialogId = actionsSaver.itemPlayerActions[ObjectID].firstPlayerActs[actionKind]; 
        }
        else { dialogId = actionsSaver.itemPlayerActions[ObjectID].secPlayerActs[actionKind]; }
        return dialogId;
    }

    public bool whichPlayer()
    {
        return playerData.isPlayer1;
    }
}
