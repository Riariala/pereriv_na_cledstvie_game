using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;

[CreateAssetMenu(fileName = "PlayersDialogiesSaver", menuName = "Players Dialogies Saver", order = 51)]
public class PlayersDialogiesSaver : ScriptableObject
{
    public List<PlayerDialog> dialogiesList;

    public void setDefault()
    {
        dialogiesList = readFromJSON();
    }

    public List<PlayerDialog> readFromJSON()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string _path = Application.streamingAssetsPath + "/PlayersDialogs.json";
        WWW reader = new WWW(_path);
        while (!reader.isDone) { }
        if ( reader.error != null )
        {
            Debug.LogError("error : " + _path);
            return new List<PlayerDialog>();
        }
        string file = reader.text;
#endif

#if !UNITY_ANDROID || UNITY_EDITOR
        string _path = Application.dataPath + "/StreamingAssets/" + "PlayersDialogs.json";

        string file = File.ReadAllText(_path, Encoding.UTF8);
#endif
        PlayerDialogsHolder itm = JsonConvert.DeserializeObject<PlayerDialogsHolder>(file);
        return itm.dialogiesList;
    }

    public List<string> AskDialog(int dialogId)
    {
        return dialogiesList[dialogId].dialogLines;
    }

    public List<bool> AskTitles(int dialogId)
    {
        return dialogiesList[dialogId].isFirstTalk;
    }
}
