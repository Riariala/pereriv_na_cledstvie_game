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

    private AudioSource audioSource;
    private int prevState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        _state = 0;
        if (playerData.gametype != 0)
        {
            if (entity.IsOwner)
            {
                movement = controller.GetDesiredMovement();
                if (movement.sqrMagnitude > 0.2)
                {
                    _state = 1;
                }
                else
                {
                    audioSource.Play();
                }
            }
            else 
            {
                if (state.isMoving)
                {
                    //state.Animator.SetInteger("State", 1);
                    _state = 1;

                }
                else
                {
                    //state.Animator.SetInteger("State", 0);
                    audioSource.Play();
                }
            }
        }
        else
        {
            movement = controller.GetDesiredMovement();
            if (movement.sqrMagnitude > 0.2)
            {
                _state = 1;
            }
            else 
            {
                audioSource.Play();
            }
        }
        if (prevState != _state)
        {
            animator.SetInteger("State", _state);
            prevState = _state;
        }
    }

}
