using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerData player_data;
    private Transform target;

    void Start()
    {
        target = player_data.char_player.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        transform.LookAt(target.position + Vector3.up);
    }

    public void ChangeTarget()
    {
        target = player_data.char_player.GetComponent<Transform>();
    }
}
