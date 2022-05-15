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
        string _path = Application.dataPath + "/Resour/" + "DefaultitemAct.json";
        itemPlayerActions = readFromJSON(_path).itemPlayerActions;
    }

    public ObjectsHolder readFromJSON(string _path)
    {
        return JsonConvert.DeserializeObject<ObjectsHolder>(File.ReadAllText(_path, Encoding.UTF8));
    }

    public void Rewrite(int id, List<int> first, List<int> sec)
    {
        itemPlayerActions[id].firstPlayerActs = first;
        itemPlayerActions[id].secPlayerActs = sec;
    }

}