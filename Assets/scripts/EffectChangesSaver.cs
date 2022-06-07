using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Text;

[CreateAssetMenu(fileName = "EffectChangesSaver", menuName = "EffectChangesSaver", order = 51)]
public class EffectChangesSaver : ScriptableObject
{
    public List<Effectschanges> effectsChanges;

    public void setDefault()
    {
        effectsChanges = readFromJSON();
    }

    public List<Effectschanges> readFromJSON()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string _path = Application.streamingAssetsPath + "/Effects.json";
        WWW reader = new WWW(_path);
        while (!reader.isDone) { }
        if ( reader.error != null )
        {
            Debug.LogError("error : " + _path);
            return new List<Effectschanges>();
        }
        string file = reader.text;
#endif


#if !UNITY_ANDROID || UNITY_EDITOR
        string _path = Application.dataPath + "/StreamingAssets/" + "Effects.json";

        string file = File.ReadAllText(_path, Encoding.UTF8);
#endif
        EffectChangesHolder itm = JsonConvert.DeserializeObject<EffectChangesHolder>(file);
        return itm.effect;
    }

    public Effectschanges takeEffect(int effectID)
    {
        if (effectID < effectsChanges.Count)
        {
            return effectsChanges[effectID];
        }
        else return null;
    }
}
