using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogersController : MonoBehaviour
{
    public Animator animator;
    private int _state;
    public Rigidbody _rb;
    private Vector3 movement;
    private PlayerController controller;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<PlayerController>();
        Debug.Log(_rb);
    }


    private void Update()
    {
        movement = controller.GetDesiredMovement();
        if (movement.sqrMagnitude > 0.2){
            _state = 1;
        }
        else{
            _state = 0;
        }
        animator.SetInteger("State", _state);
    }

}
