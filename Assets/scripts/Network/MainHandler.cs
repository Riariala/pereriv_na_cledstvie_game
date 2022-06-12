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

    // Update is called once per frame
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
                //var actionsSaver = new ActionsSaver();
                var serializedActions = JsonConvert.SerializeObject(actions);
                //var newActionsSaver = JsonConvert.DeserializeObject<Person>(serializedPerson);

                var startData = StartData.Create();
                startData.DialogId = data.dialogId;
                startData.ActionsSaver = serializedActions;
                startData.Send();
                callbacks.ask = false;
                /*var ask = AskForData.Create();
                ask.Ask = false;
                ask.Send();*/
            }
        }

        if (BoltNetwork.IsClient)
        {
            Debug.Log("Ïðèâåò");
            Debug.Log(callbacks.actionsSaver);
            if (callbacks.actionsSaver != "")
            {
                data.dialogId = callbacks.dialogId;
                actions.newValue(callbacks.actionsSaver);
                Debug.Log(actions);
                callbacks.ask = false;
                callbacks.actionsSaver = "";

            }
        }
    }
}
