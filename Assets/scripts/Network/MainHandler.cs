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


    // Update is called once per frame
    void Update()
    {
        if (BoltNetwork.IsServer)
        {
            if (callbacks.ask)
            {
                var actionsSaver = new ActionsSaver();
                var serializedActions = JsonConvert.SerializeObject(actionsSaver);
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
            Debug.Log("Привет");
            Debug.Log(callbacks.actionsSaver);
            if (callbacks.actionsSaver != "")
            {
                data.dialogId = callbacks.dialogId;
                actions.newValue(callbacks.actionsSaver);
                Debug.Log(actions);
                callbacks.ask = false;

            }
            //BoltLauncher.Shutdown();
        }
    }
}
