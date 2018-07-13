using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarioBulletCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_EnemyBulletPrefab;
    [SerializeField]
    private List<Transform> m_BulletSpawnPoints = new List<Transform>();
    [SerializeField]
    private float m_CurrentTime;
    private float m_Speed = 8;

    private void Update()
    {
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= 5f)
        {
            EnemyMarioBullet script = Instantiate(m_EnemyBulletPrefab, m_BulletSpawnPoints[Random.Range(0, m_BulletSpawnPoints.Count)].position, Quaternion.identity).GetComponent<EnemyMarioBullet>();
            script.InitSpeed(m_Speed, -transform.right.normalized);
            m_CurrentTime = 0;
        }        
    }
}