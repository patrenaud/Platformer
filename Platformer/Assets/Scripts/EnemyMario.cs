using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMario : MonoBehaviour
{
    public Rigidbody2D m_RigidBody;
    public float m_Speed = 5;

    private Vector3 m_Dir;
    //private Vector3 m_ScaleDown = new Vector3(0, 20);
    private float m_Time;

    private void Start()
    {
        m_Dir = -transform.right;

    }

    private void Update()
    {
        m_Time += Time.deltaTime;

        if (m_Time >= 4f)
        {
            StartCoroutine(SwitchSide());
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            m_Time = 0;
        }
    }

    private IEnumerator SwitchSide()
    {
        yield return new WaitForSeconds(0.01f);
        m_Dir = -m_Dir;
    }


    private void FixedUpdate()
    {
        m_RigidBody.velocity = m_Dir * m_Speed;
    }

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        // When the enemy collides with a player, the player takes damage       
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Sonic") ||
            aOther.gameObject.layer == LayerMask.NameToLayer("Tails") ||
            aOther.gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            PlayerManager.Instance.TakeDamage();
        }
    }
}
