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
            playerHistoryID.Add(id);
        }
    }

    public void addHistory(int newid)
    {
            playerHistoryID.Add(newid);
    }

    public void addEvidence(int newid, int newStatus)
    {
        playerEvidencesID.Add(new Evidences(newid, newStatus));
    }

    public void addEvidence(List<int> newid)
    {
        foreach (int id in newid)
        {
            playerEvidencesID.Add(new Evidences(id, 0));
        }
    }

    public void newInfoPerson(int personInfoId)
    {
        playerInfoID.Add(new InfoDeteiledID(personInfoId));
    }

    public void newInfoPerson(int personInfoId, List<int> newlines)
    {
        playerInfoID.Add(new InfoDeteiledID(personInfoId, newlines));
    }

    public void addToPersonInfo(int personInfoId, List<int> newLines)
    {
        foreach (InfoDeteiledID personInfo in playerInfoID)
        {
            if (personInfo.InfoId == personInfoId)
            {
                personInfo.addLinesTo(newLines);
                return;
            }
        }
    }

    public void changeEvidenceStatus(int evidID, int newStatus)
    {
        foreach (Evidences evidence in playerEvidencesID)
        {
            if (evidence.evidenceID == evidID)
            {
                evidence.status = newStatus;
                return;
            }
        }
        addEvidence(evidID, newStatus);
    }
}
