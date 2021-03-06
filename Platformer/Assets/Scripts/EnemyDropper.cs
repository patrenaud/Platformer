﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropper : MonoBehaviour
{
    public Rigidbody2D m_RigidBody;
    public float m_Speed;

    private float m_CurrentTime = 0;
    private float m_Time = 15f;
    private Vector2 m_Dir = new Vector2();
    private Animator m_Animator;
    private bool m_IsGrounded = false;
    private bool m_RightChosen = true;
    private bool m_LeftChosen = true;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void InitSpeed(float aSpeed, Vector2 aDirection)
    {
        m_Speed = aSpeed;
        m_Dir = aDirection;
    }

    private void FixedUpdate()
    {
        if (m_IsGrounded)
        {
            // When Enemy has landed, he chooses a side to walk depending on player Position
            if (PlayerManager.Instance.m_Player.transform.position.x < transform.position.x && m_RightChosen)
            {
                m_Dir = -transform.right.normalized;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                m_LeftChosen = false;
            }
            else if (PlayerManager.Instance.m_Player.transform.position.x > transform.position.x && m_LeftChosen)
            {
                m_Dir = transform.right.normalized;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                m_RightChosen = false;
            }
        }

        // Destroys after 15 secs
        m_RigidBody.velocity = m_Dir * m_Speed;
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= m_Time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D aOther)
    {
        // When the enemy collides with a player, the player takes damage       
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Sonic") && aOther.GetComponent<PlayerController>().m_CanSpin == true ||
            aOther.gameObject.layer == LayerMask.NameToLayer("Tails") ||
            aOther.gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            PlayerManager.Instance.TakeDamage();
            Destroy(gameObject);
        }
        else if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_IsGrounded = true;
            m_Animator.SetBool("Ground", true);
            m_RigidBody.gravityScale = 1;
        }
        // Only exception is when Sonic gets hit while he is spinning (invincible spin!)
        else if (aOther.gameObject.layer == LayerMask.NameToLayer("Sonic"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_IsGrounded = false;
            m_Animator.SetBool("Ground", false);
            m_Dir = -transform.up.normalized;
        }
    }
}
