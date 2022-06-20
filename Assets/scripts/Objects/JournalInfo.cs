using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "JournalInfo", menuName = "JournalInfo", order = 51)]
public class JournalInfo : ScriptableObject
{
    public List<List<int>> playerHistoryID; //0 - isPlayer
    public List<List<Evidences>> playerEvidencesID;
    public List<List<InfoDeteiledID>> playerInfoID;

    public List<List<int>> evidListShift; //(x,y)

    public List<int> newInHistory;
    public List<List<int>> newInEvid;
    public List<List<int>>  newInInfo;
    public PlayerData playerData;

    public void clearAll()
    {
        playerHistoryID = new List<List<int>>();
        playerHistoryID.Add(new List<int>());
        playerHistoryID.Add(new List<int>());
        playerEvidencesID = new List<List<Evidences>>();
        playerEvidencesID.Add(new List<Evidences>());
        playerEvidencesID.Add(new List<Evidences>());
        playerInfoID = new List<List<InfoDeteiledID>>();
        playerInfoID.Add(new List<InfoDeteiledID>());
        playerInfoID.Add(new List<InfoDeteiledID>());

        newInHistory = new List<int>();
        newInHistory.Add(0);
        newInHistory.Add(0);

        newInEvid = new List<List<int>>();
        newInEvid.Add(new List<int>());
        newInEvid.Add(new List<int>());

        newInInfo = new List<List<int>>();
        newInInfo.Add(new List<int>());
        newInInfo.Add(new List<int>());
    }

    public void addHistory(List<int> newid)
    {
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        foreach (int id in newid)
        {
            if (!playerHistoryID[ind].Contains(id))
            {
                playerHistoryID[ind].Add(id);
                newInHistory[ind]++;
            }
        }
    }

    public void addHistory(int newid)
    {
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        if (!playerHistoryID[ind].Contains(newid))
        {
            playerHistoryID[ind].Add(newid);
            newInHistory[ind]++;
        }
    }

    public void newInfoPerson(int personInfoId)
    {
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        playerInfoID[ind].Add(new InfoDeteiledID(personInfoId));
        if (!newInInfo[ind].Contains(personInfoId)) { newInInfo[ind].Add(personInfoId); }
    }

    public void newInfoPerson(int personInfoId, List<int> newlines)
    {
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        playerInfoID[ind].Add(new InfoDeteiledID(personInfoId, newlines));
        if (!newInInfo[ind].Contains(personInfoId)) { newInInfo[ind].Add(personInfoId); }
    }

    public void addToPersonInfo(int personInfoId, List<int> newLines)
    {
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        foreach (InfoDeteiledID personInfo in playerInfoID[ind])
        {
            if (personInfo.InfoId == personInfoId)
            {
                foreach (int lineid in newLines)
                { 
                    if (!personInfo.linesId.Contains(lineid))
                    {

                        if (!newInInfo[ind].Contains(personInfoId)) { newInInfo[ind].Add(personInfoId); }
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
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        playerEvidencesID[ind].Add(new Evidences(newid, newStatus));
        newInEvid[ind].Add(newid);
    }

    public void addEvidence(List<int> newid)
    {
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        foreach (int id in newid)
        {
            playerEvidencesID[ind].Add(new Evidences(id, 0));
            newInEvid[ind].Add(id);
        }
    }

    public void changeEvidenceStatus(int evidID, int newStatus)
    {
        int ind;
        if (playerData.isPlayer1) { ind = 0; } else { ind = 1; }
        foreach (Evidences evidence in playerEvidencesID[ind])
        {
            if (evidence.evidenceID == evidID)
            {
                if (evidence.status != newStatus)
                {
                    evidence.status = newStatus;
                    newInEvid[ind].Add(evidID);
                }
                return;
            }
        }
        addEvidence(evidID, newStatus);
    }
}
