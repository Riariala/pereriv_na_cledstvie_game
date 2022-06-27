using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class RogersController : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    public Animator animator;
    private int _state;
    public Rigidbody _rb;
    private Vector3 movement;
    private PlayerController controller;
    public PlayerData playerData;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<PlayerController>();
    }


    private void Update()
    {
        //if (playerData.gametype != 0)
        //{
        //    if (state.isMoving)
        //    {
        //        //if (!isStillMoving)
        //        //{
        //        state.Animator.SetInteger("State", 1);
        //        playerAnimator.SetInteger("State", 1);
        //        //state.Animator.Play("Walk");
        //        //isStillMoving = true;
        //        //}

        //    }
        //    else
        //    {
        //        state.Animator.SetInteger("State", 0);
        //        playerAnimator.SetInteger("State", 0);
        //        //state.Animator.Play("Neutral");
        //        //isStillMoving = false;

        //    }
        //}

        if (entity.IsOwner)
        {
            movement = controller.GetDesiredMovement();
            Debug.Log("movement.sqrMagnitude " + movement.sqrMagnitude);
            if (movement.sqrMagnitude > 0.2)
            {
                _state = 1;
            }
            else
            {
                _state = 0;
            }
            animator.SetInteger("State", _state);
        }
        else 
        {
            if (playerData.gametype != 0)
            {
                if (state.isMoving)
                {
                    //if (!isStillMoving)
                    //{
                    state.Animator.SetInteger("State", 1);
                    animator.SetInteger("State", 1);
                    //state.Animator.Play("Walk");
                    //isStillMoving = true;
                    //}

                }
                else
                {
                    state.Animator.SetInteger("State", 0);
                    animator.SetInteger("State", 0);
                    //state.Animator.Play("Neutral");
                    //isStillMoving = false;

                }
            }
        }

        //movement = controller.GetDesiredMovement();
        //Debug.Log("movement.sqrMagnitude " + movement.sqrMagnitude);
        //if (movement.sqrMagnitude > 0.2){
        //    _state = 1;
        //}
        //else{
        //    _state = 0;
        //}
        //animator.SetInteger("State", _state);
    }

}
