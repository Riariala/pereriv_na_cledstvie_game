using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCollider : MonoBehaviour
{
    public CapsuleCollider charCol;
    public CapsuleCollider blockerCol;

    void Start()
    {
        Physics.IgnoreCollision(charCol, blockerCol, true);
    }
}
