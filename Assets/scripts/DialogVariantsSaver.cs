using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;

[CreateAssetMenu(fileName = "DialogVariantsSaver", menuName = "Dialog Variants Saver", order = 51)]
public class DialogVariantsSaver : ScriptableObject
{
    public List<DialogVariant> variants;


    public void setDefault()
    {
        variants = readFromJSON();
    }

    public List<DialogVariant> readFromJSON()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string _path = Application.streamingAssetsPath + "/DialogVariants.json";
        WWW reader = new WWW(_path);
        while (!reader.isDone) { }
        if ( reader.error != null )
        {
            Debug.LogError("error : " + _path);
            return new List<DialogVariant>();
        }
        string file = reader.text;
#endif

#if !UNITY_ANDROID//UNITY_EDITOR
        string _path = Application.dataPath + "/Resources/" + "Effects.json";
        string file = File.ReadAllText(_path, Encoding.UTF8);
#endif
        DialogVariantsHolder itm = JsonConvert.DeserializeObject<DialogVariantsHolder>(file);
        return itm.variants;
    }

    public void changeAvailable(int diaId, int varId, bool mean)
    {
        variants[diaId].available[varId] = mean;
    }
}
