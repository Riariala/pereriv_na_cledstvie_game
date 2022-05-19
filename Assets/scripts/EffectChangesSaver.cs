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
        string _path = Application.dataPath + "/Resour/" + "Effects.json";
        effectsChanges = readFromJSON(_path).effect;
    }

    public EffectChangesHolder readFromJSON(string _path)
    {
        return JsonConvert.DeserializeObject<EffectChangesHolder>(File.ReadAllText(_path, Encoding.UTF8));
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
