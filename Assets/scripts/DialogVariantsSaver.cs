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
        string _path = Application.dataPath + "/Resour/" + "DialogVariants.json";
        variants = readFromJSON(_path).variants;
    }

    public DialogVariantsHolder readFromJSON(string _path)
    {
        return JsonConvert.DeserializeObject<DialogVariantsHolder>(File.ReadAllText(_path, Encoding.UTF8));
    }

    public void changeAvailable(int diaId, int varId, bool mean)
    {
        variants[diaId].available[varId] = mean;
    }
}
