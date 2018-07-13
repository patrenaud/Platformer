using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D m_RigidBody;
    public float m_JumpForce = 250f;
    [HideInInspector]
    public bool m_CanSpin = true;

    private Vector2 m_MoveDir = new Vector2();
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private float m_Speed = 10f;
    private SpriteRenderer m_Visual;
    private float m_InputX;    
    private bool m_CanJump = true;

    private float m_Hp;


    private void Start()
    {
        m_Visual = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        m_InputX = Input.GetAxisRaw("Horizontal");

        if (m_InputX > 0)
        {
            m_MoveDir = transform.right;
            m_Visual.flipX = false;
            m_Animator.SetBool("Run", true);
        }
        else if (m_InputX < 0)
        {
            m_MoveDir = -transform.right;
            m_Visual.flipX = true;
            m_Animator.SetBool("Run", true);
        }
        else
        {
            m_MoveDir = Vector2.zero;
            m_Animator.SetBool("Run", false);
            if (gameObject.layer == LayerMask.NameToLayer("Sonic"))
            {
                m_Animator.SetBool("Spin", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && m_CanJump)
        {
            m_RigidBody.AddForce(transform.up * m_JumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_CanSpin)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Tails") && !m_CanJump)
            {
                m_CanSpin = false;
                m_Animator.SetBool("Spin", true);
                m_RigidBody.gravityScale = 0.2f;
                StartCoroutine(PowerCooldown());
            }

            if (gameObject.layer == LayerMask.NameToLayer("Sonic"))
            {
                m_CanSpin = false;
                m_Animator.SetBool("Spin", true);
                StartCoroutine(PowerCooldown());
            }

            if (gameObject.layer == LayerMask.NameToLayer("Shadow") && m_CanJump)
            {
                m_CanSpin = false;
                m_Animator.SetBool("Spin", true);
                m_Speed *= 2;
                StartCoroutine(PowerCooldown());
            }
        }
    }

    private IEnumerator PowerCooldown()
    {
        yield return new WaitForSeconds(2f);

        if (gameObject.layer == LayerMask.NameToLayer("Tails"))
        {
            m_Animator.SetBool("Spin", false);
            m_CanSpin = true;
            m_RigidBody.gravityScale = 1;
        }
        if (gameObject.layer == LayerMask.NameToLayer("Sonic"))
        {
            m_Animator.SetBool("Spin", false);
            m_CanSpin = true;
        }
        if (gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            m_Speed /= 2;
            m_Animator.SetBool("Spin", false);
            m_CanSpin = true;
        }

    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_Speed;
        m_MoveDir.y = m_RigidBody.velocity.y;
        m_RigidBody.velocity = m_MoveDir;
    }

    private void OnTriggerStay2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_CanJump = true;
        }


    }

    private void OnTriggerExit2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_CanJump = false;
        }
    }
}
