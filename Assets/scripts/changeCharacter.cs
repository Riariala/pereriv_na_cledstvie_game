using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCharacter : MonoBehaviour
{
    [SerializeField] private PlayerController Player1_script;
    [SerializeField] private PlayerController Player2_script;
    [SerializeField] private FixedJoystick _jystick;
    [SerializeField] private PlayerData player_data;
    //private bool ispl1;

    // Start is called before the first frame update
    void Start()
    {
        Player1_script.ChangeJoystick(_jystick);
        player_data.isPlayer1 = true;
    }

    public void Change_chars(){
        if (player_data.isPlayer1){
            player_data.isPlayer1 = false;
            Player2_script.ChangeJoystick(_jystick);
            Player1_script.ChangeJoystick(null);
        }
        else
        {
            player_data.isPlayer1 = true;
            Player1_script.ChangeJoystick(_jystick);
            Player2_script.ChangeJoystick(null);
        }
    }
}
