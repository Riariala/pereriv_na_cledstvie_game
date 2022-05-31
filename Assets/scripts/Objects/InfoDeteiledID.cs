using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InfoDeteiledID
{
    public int InfoId;
    public List<int> linesId;

    public InfoDeteiledID()
    {

    }

    public InfoDeteiledID(int newid)
    {
        InfoId = newid;
        linesId = new List<int>();
    }

    public InfoDeteiledID(int newid, List<int> newlines)
    {
        InfoId = newid;
        linesId = newlines;
        linesId.Sort();
    }

    public void addLinesTo(List<int> newLines)
    {
        foreach (int line in newLines)
        {
            linesId.Add(line);
        }
        linesId.Sort();
    }

    public void addLinesTo(int newLine)
    {
        linesId.Add(newLine);
        linesId.Sort();
    }
}
