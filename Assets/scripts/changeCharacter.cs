using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCharacter : MonoBehaviour
{
    [SerializeField] private PlayerController Player1_script;
    [SerializeField] private PlayerController Player2_script;
    [SerializeField] private FixedJoystick _jystick;
    //private PlayerController Player1_script;
    //private PlayerController Player2_script;
    private bool ispl1;

    // Start is called before the first frame update
    void Start()
    {
        //Player1_script = Player1.GetComponent<PlayerController>();
        //Player2_script = Player2.GetComponent<PlayerController>();
        Player1_script.ChangeJoystick(_jystick);
        Debug.Log(Player2_script);
        //Player1_script._joystick = _jystick;
        ispl1 = true;
        Debug.Log("ae");
    }

    public void Change_chars(){
        Debug.Log("krya");
        if (ispl1 == true){
            Debug.Log("1111");
            ispl1 = false;
            Debug.Log(Player2_script);
            Player2_script.ChangeJoystick(_jystick);
            Player1_script.ChangeJoystick(null);
            //Player1_script = Player1.GetComponent<PlayerController>();
            //Player2_script = Player2.GetComponent<PlayerController>();
            //Player2_script._joystick = _jystick;
            //Player1_script._joystick = null;
            //ispl1 = false;
        }
        else
        {
            Debug.Log("2222");
            ispl1 = true;
            Player1_script.ChangeJoystick(_jystick);
            Player2_script.ChangeJoystick(null);
            //Player1_script = Player1.GetComponent<PlayerController>();
            //Player2_script = Player2.GetComponent<PlayerController>();
            //Player1_script._joystick = _jystick;
            //Player2_script._joystick = null;
            
        }
    }


}
