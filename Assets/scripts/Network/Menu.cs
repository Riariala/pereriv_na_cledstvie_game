using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class Menu : GlobalEventListener
{
    public Button joinBtnInList;
    public GameObject SessionListPanel;
    public float BtnSpacing;
    private List<Button> joinServerBtns = new List<Button>();

    public void StartServer()
    {
        if (BoltNetwork.IsClient) {
            BoltLauncher.Shutdown();
        }
        BoltLauncher.StartServer();
       
    }
    public override void BoltStartDone()
    {

        //if (BoltNetwork.IsServer)
        //{
            int randInt = UnityEngine.Random.Range(0, 99999);
            BoltMatchmaking.CreateSession(sessionID: randInt.ToString(), sceneToLoad: "level0");
            //BoltNetwork.setServerInfo(randInt, null);
            //BoltNetwork.LoadScene("level0");
        //}
    }

    public void StartClient()
    {
        if (BoltNetwork.IsServer)
        {
            BoltLauncher.Shutdown(); 
        }

        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> SessionList)
    {
        ClearSessions();
        foreach (var session in SessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            Button joinClone = Instantiate(joinBtnInList, SessionListPanel.transform);
            joinClone.transform.localPosition = new Vector3(0, BtnSpacing*joinServerBtns.Count, 0);
            joinClone.gameObject.SetActive(true);
            joinClone.onClick.AddListener(() => JoinGame(photonSession));

            joinServerBtns.Add(joinClone);


            /*if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }*/



        }
    }

    private void JoinGame(UdpSession photonSession)
    {
        BoltMatchmaking.JoinSession(photonSession);
    }

    private void ClearSessions()
    {
        foreach (Button button in joinServerBtns)
        {
            Destroy(button.gameObject);
        }
        joinServerBtns.Clear();
    }

}
