using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InfoDeteiledID
{
    public int InfoId;
    public List<int> linesId;

    public InfoDeteiledID(int newid)
    {
        InfoId = newid;
        linesId = new List<int>();
    }

    public InfoDeteiledID(int newid, List<int> newlines)
    {
        InfoId = newid;
        linesId = newlines;
    }

    public void addLinesTo(List<int> newLines)
    {
        foreach (int line in newLines)
        {
            linesId.Add(line);
        }
    }
}
