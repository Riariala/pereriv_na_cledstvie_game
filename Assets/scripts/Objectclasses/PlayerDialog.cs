using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialog
{
    public int ID;
    public List<bool> isFirstTalk;
    public List<string> dialogLines;
    public List<ObjectActions> changes;
    public int effect;
    public int nextDialog;
}
