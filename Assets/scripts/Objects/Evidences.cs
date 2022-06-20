using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidences
{
    public int evidenceID;
    public int status; //0 - �������������, 1 - �����������, 2 - ������
    public List<int> connected;

    public Evidences(int id, int newStatus)
    {
        evidenceID = id;
        status = newStatus;
        connected = new List<int>();
    }
}
