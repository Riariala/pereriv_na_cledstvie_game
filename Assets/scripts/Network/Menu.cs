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
    private BoltConfig _config;
    public GameObject refresh_btn;

    private void Awake()
    {
        _config = BoltRuntimeSettings.instance.GetConfigCopy();
        _config.serverConnectionLimit = 2; // Set here the max number of clients
    }

    public override void Disconnected(BoltConnection connection)
    {
        if (BoltNetwork.Server.Equals(connection))
        {
            BoltLog.Warn("Disconnected from the server");
        }
    }
    public void StartServer()
    {
        if (BoltNetwork.IsClient) {
            BoltLauncher.Shutdown();
        }
        matchName = UnityEngine.Random.Range(1000, 99999).ToString();
        playerData.gameCode = matchName;
        roomCode.text = matchName;
        BoltLauncher.StartServer(_config);
       
    }

    public override void BoltStartDone()
    {
        friendBool = menuscript.isCoworker;
        unknownBool = menuscript.isUnknowns;
        if (friendBool && !unknownBool)
        {
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

    public void RefreshSessionList()
    {
        StartClient();
        refresh_btn.SetActive(false);
    }

    public void StartClient()
    {
        if (BoltNetwork.IsServer)
        {
            BoltLauncher.Shutdown(); 
        }
        BoltLauncher.StartClient(_config);
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> SessionList)
    {
        ClearSessions();
        foreach (var session in SessionList)
        {
            
            UdpSession photonSession = session.Value as UdpSession;
            // Button joinClone = Instantiate(joinBtnInList, SessionListPanel.transform);
            // joinClone.transform.localPosition = new Vector3(0, BtnSpacing*joinServerBtns.Count, 0);
            Button joinClone = Instantiate(joinBtnInList, SessionListPanel.transform);
            //joinClone.transform.localPosition = new Vector3(0, joinClone.GetComponent<RectTransform>().sizeDelta.y*joinServerBtns.Count, 0);
            joinClone.transform.localPosition = new Vector3(0, -BtnSpacing*joinServerBtns.Count+100, 0);
            joinClone.transform.GetChild(0).GetComponent<Text>().text = session.Key.ToString(); //здесь надо имя, потом еще понять, откуда взять номер главы, которая проходится
            if (photonSession.ConnectionsCurrent < 2)
            {
                joinClone.gameObject.SetActive(true);
                //joinClone.onClick.AddListener(() => JoinGame(photonSession));
                joinClone.onClick.AddListener(() => BoltMatchmaking.JoinSession(photonSession));

                joinServerBtns.Add(joinClone);
            }
        }
    }

    public void JoinGame()
    {
        if (BoltNetwork.IsRunning && BoltNetwork.IsClient)
        {
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
