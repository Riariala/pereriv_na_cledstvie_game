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

    public void newValue(string NewJson)
    {
        //itemPlayerActions = readFromJSON();
        ObjectsHolder itm = JsonConvert.DeserializeObject<ObjectsHolder>(NewJson);
        itemPlayerActions = itm.itemPlayerActions;
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

#if !UNITY_ANDROID || UNITY_EDITOR
        string _path = Application.dataPath + "/StreamingAssets/" + "DefaultitemAct.json";

        string file = File.ReadAllText(_path, Encoding.UTF8);
#endif
        ObjectsHolder itm = JsonConvert.DeserializeObject<ObjectsHolder>(file);
        return itm.itemPlayerActions;
    }


    public void Rewrite(int id, List<int> first, List<int> sec)
    {
        for (int newactid = 0; newactid < first.Count; newactid++)
        {
            if (first[newactid] >= 0)
            {
                itemPlayerActions[id].firstPlayerActs[newactid] = first[newactid];
            }
        }
        for (int newactid = 0; newactid < sec.Count; newactid++)
        {
            if (sec[newactid] >= 0)
            {
                itemPlayerActions[id].secPlayerActs[newactid] = sec[newactid];
                Debug.Log(sec[newactid]);
            }
            
        }
        //itemPlayerActions[id].firstPlayerActs = first;
        //itemPlayerActions[id].secPlayerActs = sec;
    }

}