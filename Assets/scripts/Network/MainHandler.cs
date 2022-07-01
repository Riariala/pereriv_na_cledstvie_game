using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;

public class MainHandler : Photon.Bolt.EntityBehaviour<ICustomPlayer>//MonoBehaviour
{

    public NetworkCallbacks callbacks;
    public PlayerData data;
    public ActionsSaver actions;
    public JournalInfo journal;
    public DialogSaver dialogSaver;
    public changeCharacter _changeChars;

    void Update()
    {
        if (callbacks.clickDialog)
        {
            if (!dialogSaver.isInitiator)
            {
                if (callbacks.actionsSaver != "")
                {
                    ObjectActions dialogData = JsonConvert.DeserializeObject<ObjectActions>(callbacks.dialogSaverData);
                    actions.Rewrite(dialogData.ID, dialogData.firstPlayerActs, dialogData.secPlayerActs);
                    callbacks.clickDialog = false;
                }
            }
            else
            {
                dialogSaver.isInitiator = false;
                callbacks.clickDialog = false;
            }
        }
        if (BoltNetwork.IsServer)
        {
            if (callbacks.ask)
            {
                var startData = StartData.Create();
                startData.DialogId = data.dialogId;
                //startData.JournalInfo = JsonConvert.SerializeObject(journal);
                List<string> journalInf = journal.SerializeInfo();
                startData.JournalInfo = JsonConvert.SerializeObject(journalInf);
                startData.isPlayer1 = !data.isPlayer1;
                Vector3 pos;
                if (data.isPlayer1 && !(_changeChars.MarySingle is null)) 
                {
                    if (!(_changeChars.MarySingle == null))
                    {
                        pos = _changeChars.MarySingle.transform.position;
                    }
                    else { pos = new Vector3(-1.5f, 0, 43f); }
                }
                else
                {
                    if (!(_changeChars.RogersSingle == null))
                    {
                        pos = _changeChars.RogersSingle.transform.position;
                    }
                    else { pos = new Vector3(6f, 0, -9f); }
                }
                startData.Position = pos;
                startData.ActionsSaver = JsonConvert.SerializeObject(actions);
                startData.Send();
                _changeChars.KillCharacter(!data.isPlayer1);
                callbacks.ask = false;
            }
        }

        if (BoltNetwork.IsClient)
        {
            if (callbacks.actionsSaver != "")
            {
                data.dialogId = callbacks.dialogId;
                actions.newValue(callbacks.actionsSaver);
                List<string> journinf = JsonConvert.DeserializeObject<List<string>>(callbacks.journalInfo);
                journal.DeserializeInfo(journinf);
                //Debug.Log(actions);
                callbacks.ask = false;
                callbacks.actionsSaver = "";
                callbacks.createSecondPlayer(callbacks.isPlayer1_forStart, callbacks.newcharPosition);
            }
        }
    }
}
