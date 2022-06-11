using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogVariant
{
    public int variantId;
    public List<string> optionLines;
    public List<List<ObjectActions>> changes;
    public List<int> effects;
    public List<bool> available;
    public List<List<int>> specailActions;
}
