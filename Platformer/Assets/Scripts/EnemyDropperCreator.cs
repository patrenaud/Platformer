using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropperCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject m_EnemyDropperPrefab;
    [SerializeField]
    private List<Transform> m_DropperSpawnPoints = new List<Transform>();
    [SerializeField]
    private float m_CurrentTime;
    private float m_Speed = 2;

    private void Update()
    {
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= 5f)
        {
            EnemyDropper script = Instantiate(m_EnemyDropperPrefab, m_DropperSpawnPoints[Random.Range(0, m_DropperSpawnPoints.Count)].position, Quaternion.identity).GetComponent<EnemyDropper>();
            script.InitSpeed(m_Speed, -transform.up.normalized);
            m_CurrentTime = 0;
        }
    }
}
