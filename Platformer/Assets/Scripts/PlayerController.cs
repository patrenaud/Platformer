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

    private void Start()
    {
        m_Visual = GetComponent<SpriteRenderer>();
        PlayerManager.Instance.m_Player = this.gameObject;
    }

    public void Update()
    {
        // To make Character Move Horizontal
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
            if (gameObject.layer == LayerMask.NameToLayer("Sonic")) // If Sonic stops moving or turns around, his power stops
            {
                m_Animator.SetBool("Spin", false);
            }
        }

        // Makes the character Jump
        if (Input.GetKeyDown(KeyCode.W) && m_CanJump)
        {
            m_RigidBody.AddForce(transform.up * m_JumpForce);
        }

        // Space is to use Special Power. one for each Character
        if (Input.GetKeyDown(KeyCode.Space) && m_CanSpin)
        {
            // Tails can only use his power in the air
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

            // Shadow can only use his power on the ground
            if (gameObject.layer == LayerMask.NameToLayer("Shadow") && m_CanJump)
            {
                m_CanSpin = false;
                m_Animator.SetBool("Spin", true);
                m_Speed *= 2;
                StartCoroutine(PowerCooldown());
            }
        }
    }

    // Powers can be used every 2 seconds or so
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

    // For CheckPoint purposes
    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if(aOther.gameObject.layer == LayerMask.NameToLayer("CheckPoint1"))
        {
            PlayerManager.Instance.m_CheckPoint1Done = true;
            Debug.Log("CHeckPoint 1 Reached");
        }
        if (aOther.gameObject.layer == LayerMask.NameToLayer("CheckPoint2"))
        {
            PlayerManager.Instance.m_CheckPoint2Done = true;
            Debug.Log("CHeckPoint 2 Reached");
        }
    }

    // For Jump purposes
    private void OnTriggerStay2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_CanJump = true;
        }
    }

    // For Jump purposes
    private void OnTriggerExit2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_CanJump = false;
        }
    }

    public void ScaleDown()
    {
        gameObject.transform.localScale /= 1.5f;
    }

    public void ScaleUp()
    {
        gameObject.transform.localScale *= 1.5f;
    }
}
