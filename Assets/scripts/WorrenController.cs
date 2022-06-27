using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorrenController : MonoBehaviour
{
    public Animator animator;
    private bool generated = false;
    private int _state;
    private System.Random rnd;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rnd = new System.Random();
    }


    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") && !generated)
        {
            _state = rnd.Next(0,10);
            animator.SetInteger("State", _state);
            generated = true;
            //Debug.Log(_state); 
        }      
    }


    public void animation_event()
    {
        generated=false;
    }
}
