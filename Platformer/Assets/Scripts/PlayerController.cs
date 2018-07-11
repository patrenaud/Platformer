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
    private bool m_CanSpin = true;
    private bool m_CanJump = true;

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
            m_Animator.SetBool("Spin", false);
        }


        if (Input.GetKeyDown(KeyCode.W) && m_CanJump)
        {
            m_RigidBody.AddForce(transform.up * m_JumpForce);
            //m_Animator.SetBool("Spin", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_CanSpin)
        {
            m_CanSpin = false;
            m_Animator.SetBool("Spin", true);
            StartCoroutine(PowerCooldown());
        }
    }

    private IEnumerator PowerCooldown()
    {
        yield return new WaitForSeconds(2f);
        m_Animator.SetBool("Spin", false);
        m_CanSpin = true;
    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_Speed;
        m_MoveDir.y = m_RigidBody.velocity.y;
        m_RigidBody.velocity = m_MoveDir;
    }

    private void OnTriggerEnter(Collider aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_CanJump = true;
        }
    }

    private void OnTriggerExit(Collider aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_CanJump = false;
        }
    }
}
