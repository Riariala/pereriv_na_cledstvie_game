using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 51)]
public class PlayerData : ScriptableObject
{
    public bool isPlayer1;
    public GameObject char_player;
    public int dialogId;
    public bool isBusy;
    public List<bool> isGameOver; //0 - ������� (Player1)
    public List<bool> isGameJustStarted;
    public string gameCode;
    public int gametype; //0 - ���������, 1 - ������������ ��������, 2 - ���������� ������������ ��������, 3 - ��������� ��������, 4 - ������

    public void ChangeCharacter(GameObject newplayer)
    {
        isPlayer1 = !isPlayer1;
        char_player = newplayer;
    }

    public void ChangeCharacter()
    {
        isPlayer1 = !isPlayer1;
    }

    public void SetCharacter(bool _isPlayer1, GameObject newplayer)
    {
        isPlayer1 = _isPlayer1;
        char_player = newplayer;
    }
    public void SetCharacter(bool _isPlayer1)
    {
        isPlayer1 = _isPlayer1;
    }

    public void SetObjectCharacter(GameObject newplayer)
    {
        char_player = newplayer;
    }
}
