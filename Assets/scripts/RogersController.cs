using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogersController : MonoBehaviour
{
    public Animator animator;
    private int _state;
    public PlayerController playerController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerController>();
        Debug.Log(playerController);
    }


    private void Update()
    {
        if (playerController._joystick.Horizontal != 0 || playerController._joystick.Vertical != 0){
            _state = 1;
        }
        else{
            _state = 0;
        }
        animator.SetInteger("State", _state);
    }
}
