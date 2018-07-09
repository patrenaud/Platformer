using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    public Rigidbody2D m_RigidBody;
    public float m_JumpForce = 250f;

    private Vector2 m_MoveDir = new Vector2();
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private float m_Speed = 10f;
    private SpriteRenderer m_Visual;
    private float m_InputX;

    private void Awake()
    {
        m_Visual = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        m_InputX = Input.GetAxisRaw("Horizontal");
		m_Animator.SetInteger("Speed", (int)m_InputX);
        if (m_InputX > 0)
        {
            m_Visual.flipX = false;
        }
        else if (m_InputX < 0)
        {
            m_Visual.flipX = true;
        }


        if (Input.GetKey(KeyCode.D))
        {
            // va a droite
            m_MoveDir = transform.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // va a gauche
            m_MoveDir = -transform.right;
        }
        else
        {
            // va nul part
            m_MoveDir = Vector2.zero;
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            m_RigidBody.AddForce(transform.up * m_JumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_Speed;
        m_MoveDir.y = m_RigidBody.velocity.y;
        m_RigidBody.velocity = m_MoveDir;
    }
}
