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
    public PlayerData playerData;
    public float BtnSpacing;
    private List<Button> joinServerBtns = new List<Button>();
    public MenuScript menuscript;
    public bool friendBool=false;
    public bool unknownBool=false;
    public Text roomCode;
    public InputField inputCode;
    public string matchName;

    public void StartServer()
    {
        if (BoltNetwork.IsClient) {
            BoltLauncher.Shutdown();
        }
        matchName = UnityEngine.Random.Range(1000, 99999).ToString();
        roomCode.text = matchName;
        BoltLauncher.StartServer();
       
    }

    public override void BoltStartDone()
    {
        //int randInt = UnityEngine.Random.Range(0, 99999);
        //string matchName = Guid.NewGuid().ToString();
        
        friendBool = menuscript.isCoworker;
        unknownBool = menuscript.isUnknowns;
        Debug.Log(friendBool);
        Debug.Log(unknownBool);
        if (friendBool && !unknownBool)
        {
            
            Debug.Log("уууууууууууу");
            //BoltNetwork.SetServerInfo("Private_" + randInt.ToString(), null);
            //BoltMatchmaking.CreateSession(sessionID: "Private_" + randInt.ToString(), sceneToLoad: "level0");
            var props = new PhotonRoomProperties();

            props.IsOpen = true;
            props.IsVisible = false; // Make the session invisible

            props["type"] = "game01";
            props["map"] = "Tutorial1";

            BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: "level0",
                token: props
            );
        }
        else
        {
            //BoltNetwork.SetServerInfo(randInt.ToString(), null);
            //BoltMatchmaking.CreateSession(sessionID: randInt.ToString(), sceneToLoad: "level0");
            var props = new PhotonRoomProperties();
            Debug.Log("ввввввввв");
            props.IsOpen = true;
            props.IsVisible = true; // Make the session invisible

            props["type"] = "game01";
            props["map"] = "Tutorial1";

            BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: "level0",
                token: props
            );
        }
            
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
        Debug.Log("авыав");
        foreach (var session in SessionList)
        {
            
            UdpSession photonSession = session.Value as UdpSession;
            // Button joinClone = Instantiate(joinBtnInList, SessionListPanel.transform);
            // joinClone.transform.localPosition = new Vector3(0, BtnSpacing*joinServerBtns.Count, 0);
            Button joinClone = Instantiate(joinBtnInList, SessionListPanel.transform);
            //joinClone.transform.localPosition = new Vector3(0, joinClone.GetComponent<RectTransform>().sizeDelta.y*joinServerBtns.Count, 0);
            joinClone.transform.localPosition = new Vector3(0, -BtnSpacing*joinServerBtns.Count+100, 0);
            joinClone.transform.GetChild(0).GetComponent<Text>().text = session.Key.ToString(); //здесь надо имя, потом еще понять, откуда взять номер главы, которая проходится
            //var token = new TestToken();
            Debug.Log(photonSession.Id);
            joinClone.gameObject.SetActive(true);
            //joinClone.onClick.AddListener(() => JoinGame(photonSession));
            joinClone.onClick.AddListener(() => BoltMatchmaking.JoinSession(photonSession));

            joinServerBtns.Add(joinClone);


            /*if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }*/



        }
    }

    public void JoinGame()
    {
        //BoltMatchmaking.JoinSession(photonSession);
        if (BoltNetwork.IsRunning && BoltNetwork.IsClient)
        {
            //var userToken = new UserToken();
            //userToken.user = user;
            //userToken.password = password;
            Debug.Log(inputCode.text);
            BoltMatchmaking.JoinSession(inputCode.text, null);
                   
        }
        else
        {
            BoltLog.Warn("Only a started client can join sessions");
        }
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
