using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;

[CreateAssetMenu(fileName = "EffectsSaver", menuName = "EffectsSaver", order = 51)]
public class EffectsSaver : ScriptableObject
{
    public List<string> history;
    public List<EvidencesSaver> evidences;
    public List<InfoSaver> info;

    public void setDefault()
    {
        string _path = Application.dataPath + "/Resour/" + "EffectsData.json";
        EffectsHolder objects = readFromJSON(_path);
        history = objects.history;
        evidences = objects.evidences;
        info = objects.info;
    }

    public EffectsHolder readFromJSON(string _path)
    {
        return JsonConvert.DeserializeObject<EffectsHolder>(File.ReadAllText(_path, Encoding.GetEncoding("utf-8")));
    }
}
