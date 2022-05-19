using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidences
{
    public int evidenceID;
    public int status;

    public Evidences(int id, int newStatus)
    {
        evidenceID = id;
        status = newStatus;
    }
}
