using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KinematicObject : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Vector3 velocity;
    public float nowSpeed;

    protected virtual void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        rigid.velocity = new Vector3(velocity.x * nowSpeed, velocity.y);
    }

    public void MoveTo(Vector2 _velocity)
    {
        velocity = _velocity;
    }
}
