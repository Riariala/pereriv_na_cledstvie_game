using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "JournalInfo", menuName = "JournalInfo", order = 51)]
public class JournalInfo : ScriptableObject
{
    public List<int> playerHistoryID;
    public List<Evidences> playerEvidencesID;
    public List<InfoDeteiledID> playerInfoID;

    public List<int> evidListShift; //(x,y)

    public int newInHistory;
    public List<int> newInEvid;
    public List<int> newInInfo;

    public void clearAll()
    {
        playerHistoryID = new List<int>();
        playerEvidencesID = new List<Evidences>();
        playerInfoID = new List<InfoDeteiledID>();
    }

    public void addHistory(List<int> newid)
    {
        foreach (int id in newid)
        {
            if (!playerHistoryID.Contains(id))
            {
                playerHistoryID.Add(id);
                newInHistory++;
            }
        }
    }

    public void addHistory(int newid)
    {
        if (!playerHistoryID.Contains(newid))
        {
            playerHistoryID.Add(newid);
            newInHistory++;
        }
    }

    public void newInfoPerson(int personInfoId)
    {
        playerInfoID.Add(new InfoDeteiledID(personInfoId));
        if (!newInInfo.Contains(personInfoId)) { newInInfo.Add(personInfoId); }
    }

    public void newInfoPerson(int personInfoId, List<int> newlines)
    {
        playerInfoID.Add(new InfoDeteiledID(personInfoId, newlines));
        if (!newInInfo.Contains(personInfoId)) { newInInfo.Add(personInfoId); }
    }

    public void addToPersonInfo(int personInfoId, List<int> newLines)
    {
        foreach (InfoDeteiledID personInfo in playerInfoID)
        {
            if (personInfo.InfoId == personInfoId)
            {
                foreach (int lineid in newLines)
                { 
                    if (!personInfo.linesId.Contains(lineid))
                    {

                        if (!newInInfo.Contains(personInfoId)) { newInInfo.Add(personInfoId); }
                        personInfo.addLinesTo(newLines);
                    }
                }
                personInfo.linesId.Sort();
                return;
            }
        }
        newInfoPerson(personInfoId, newLines);
    }

    public void addEvidence(int newid, int newStatus)
    {
        playerEvidencesID.Add(new Evidences(newid, newStatus));
        newInEvid.Add(newid);
    }

    public void addEvidence(List<int> newid)
    {
        foreach (int id in newid)
        {
            playerEvidencesID.Add(new Evidences(id, 0));
            newInEvid.Add(id);
        }
    }

    public void changeEvidenceStatus(int evidID, int newStatus)
    {
        foreach (Evidences evidence in playerEvidencesID)
        {
            if (evidence.evidenceID == evidID)
            {
                if (evidence.status != newStatus)
                {
                    evidence.status = newStatus;
                    newInEvid.Add(evidID);
                }
                return;
            }
        }
        addEvidence(evidID, newStatus);
    }
}
