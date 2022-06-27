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

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

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
                audioSource.Play();
            }
            animator.SetInteger("State", _state);
        }
        else 
        {
            if (playerData.gametype != 0)
            {
                if (state.isMoving)
                {
                    state.Animator.SetInteger("State", 1);
                    animator.SetInteger("State", 1);

                }
                else
                {
                    state.Animator.SetInteger("State", 0);
                    animator.SetInteger("State", 0);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
                    audioSource.Play();
                    //state.Animator.Play("Neutral");
                    //isStillMoving = false;
=======
>>>>>>> 167e38c80135808b09541e26e1924313194e5530
=======
>>>>>>> 167e38c80135808b09541e26e1924313194e5530
=======
>>>>>>> 167e38c80135808b09541e26e1924313194e5530
=======
>>>>>>> 167e38c80135808b09541e26e1924313194e5530

                }
            }
        }

    }

}
