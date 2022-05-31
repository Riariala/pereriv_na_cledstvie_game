using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;

[CreateAssetMenu(fileName = "ActionsSaver", menuName = "Action-Player", order = 51)]
public class ActionsSaver : ScriptableObject
{
    public List<ObjectActions> itemPlayerActions;

    public void setDefault()
    {
        itemPlayerActions = readFromJSON();
    }

    public List<ObjectActions> readFromJSON()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string _path = Application.streamingAssetsPath + "/DefaultitemAct.json";
        WWW reader = new WWW(_path);
        while (!reader.isDone) { }
        if ( reader.error != null )
        {
            Debug.LogError("error : " + _path);
            return new List<ObjectActions>();
        }
        string file = reader.text;
#endif
#if !UNITY_ANDROID//UNITY_EDITOR
        string _path = Application.dataPath + "/Resources/" + "DefaultitemAct.json";
        string file = File.ReadAllText(_path, Encoding.UTF8);
#endif
        ObjectsHolder itm = JsonConvert.DeserializeObject<ObjectsHolder>(file);
        return itm.itemPlayerActions;
    }


    public void Rewrite(int id, List<int> first, List<int> sec)
    {
        itemPlayerActions[id].firstPlayerActs = first;
        itemPlayerActions[id].secPlayerActs = sec;
    }

}