using UnityEngine;
using System;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class ServerCallbacks : Photon.Bolt.GlobalEventListener
{
    public changeCharacter cngChar;
    public PlayerData playerData;
    public GameMenu gameMenu;
    public static event Action<bool> OnConnectResult;

    public override void Connected(BoltConnection connection)
    {
        if (BoltNetwork.IsServer)
        {
            if (playerData.gametype == 3)
            {
                cngChar.cngCharBtnSet(false);
                // �����: ��������� "������� ������" ������ �� JournalInfo � (!) ���������� (!) ���������. (ActionSaver ��� ������� �����. � ��������, JournalInfo ����� ��� ��)
                cngChar.KillCharacter(!playerData.isPlayer1);
            }
            else 
            {
                gameMenu.showLoading(false);
            }
        }
        else
        {
            if (playerData.gametype == 3)
            {
                cngChar.cngCharBtnSet(false);
                // ������� ����� ���������� ��������� � ������� ���
            }
        }
    }

    public override void Disconnected(BoltConnection connection)
    {
        if (BoltNetwork.IsServer)
        {
            if (playerData.gametype == 3)
            {
                Vector3 newpos = new Vector3(6f, 0, -9f);
                // ������� ���������� ��������� �� ���������������� ������� ������ � ������� ���������
                
                cngChar.CreateCharacter(!playerData.isPlayer1, newpos);

                cngChar.cngCharBtnSet(true);
            }
            else 
            {
                gameMenu.showLoading(true);
                //gameMenu.toMainMenu();
            }
        }
        else
        {
            // ����� �������������: ��������� "������� ������" ������ �� JournalInfo � (!) ���������� (!) ���������.

        }
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        SceneManager.LoadScene("MainMenu");
    }



}