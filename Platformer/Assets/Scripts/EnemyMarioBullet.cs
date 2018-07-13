using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarioBullet : MonoBehaviour
{
    public Rigidbody2D m_RigidBody;
    public float m_Speed;

    private float m_CurrentTime = 0;
    private float m_Time = 5f;
    private Vector2 m_Dir = new Vector2();

    public void InitSpeed(float aSpeed, Vector2 aDirection)
    {
        m_Speed = aSpeed;
        m_Dir = aDirection;
    }

    private void FixedUpdate()
    {
        m_RigidBody.velocity = m_Dir * m_Speed;
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= m_Time)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D aOther)
    {
        // When the enemy collides with a player, the player takes damage        
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Sonic") && aOther.GetComponent<PlayerController>().m_CanSpin == true ||
           aOther.gameObject.layer == LayerMask.NameToLayer("Tails") ||
           aOther.gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            PlayerManager.Instance.TakeDamage();
            Destroy(gameObject);
            Debug.Log(PlayerManager.Instance.m_HP);
        }
        // Only exception is when Sonic gets hit while he is spinning (invincible spin!)
        else if (aOther.gameObject.layer == LayerMask.NameToLayer("Sonic"))
        {
            Destroy(gameObject);
        }
    }
}
