using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidences
{
    public int evidenceID;
    public int status; //0 - неподтвержден, 1 - подтвержден, 2 - ложный
    public List<int> connected;

    public Evidences(int id, int newStatus)
    {
        evidenceID = id;
        status = newStatus;
        connected = new List<int>();
    }
}
