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

    public List<ObjectDialogs> objects;

    public void setDefault()
    {
        string _path = Application.dataPath + "/Resour/" + "ObjectDialogs.json";
        Debug.Log(_path);
        objects = readFromJSON(_path).objects;
        Debug.Log(objects);
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
