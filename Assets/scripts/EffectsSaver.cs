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
        EffectsHolder objects = readFromJSON();
        history = objects.history;
        evidences = objects.evidences;
        info = objects.info;
    }

    public EffectsHolder readFromJSON()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string _path = Application.streamingAssetsPath + "/EffectsData.json";
        WWW reader = new WWW(_path);
        while (!reader.isDone) { }
        if ( reader.error != null )
        {
            Debug.LogError("error : " + _path);
            return new EffectsHolder();
        }
        string file = reader.text;
#endif

#if !UNITY_ANDROID//UNITY_EDITOR
        string _path = Application.dataPath + "/StreamingAssets/" + "EffectsData.json";
        string file = File.ReadAllText(_path, Encoding.UTF8);
#endif
        return JsonConvert.DeserializeObject<EffectsHolder>(file);
    }
}
