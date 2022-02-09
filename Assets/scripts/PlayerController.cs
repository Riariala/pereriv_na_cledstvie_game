using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody)), typeof(BoxCollider)))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private FixedJoystick _joystick;
    //[SerializeField] private Animator _animator;

    [SerializeField] private float speed =  10f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // void Update()
    // {
        
    // }

    void FixedUpdate()
    {
        if (!(_joystick is null))
        {
            _rb.velocity = new Vector3(_joystick.Horizontal * speed, _rb.velocity.y, _joystick.Vertical * speed);
        }
    }

    public void ChangeJoystick(FixedJoystick newJstk)
    {
        _joystick = newJstk;
    }
}
