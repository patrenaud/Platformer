using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : DontDestroyOnLoad
{
    [HideInInspector]
    public GameObject m_Player;
    [SerializeField]
    private GameObject m_SonicPrefab; // index 1
    [SerializeField]
    private GameObject m_TailsPrefab; // index 2
    [SerializeField]
    private GameObject m_ShadowPrefab; // index 3
    [SerializeField]
    private Transform m_SpawnPos;
    private Color m_DefaultColor;
    private float m_Timer;
    private float m_TimerTick;
    private Coroutine m_Coroutine;

    public int m_HP = 2;
    public int m_Life = 3;

    private static PlayerManager m_Instance;
    public static PlayerManager Instance
    {
        get { return m_Instance; }
    }

    protected override void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(this);
        }
        base.Awake();

        // Instantiates the player depending on the CharacterIndex from the button choice from SceneManagement
        if (SceneManagement.Instance.m_CharacterIndex == 1) // index 1 = Sonic
        {
            m_Player = Instantiate(m_SonicPrefab, m_SpawnPos.position, Quaternion.identity);
        }
        else if (SceneManagement.Instance.m_CharacterIndex == 2) // index 2 = Tails
        {
            m_Player = Instantiate(m_TailsPrefab, m_SpawnPos.position, Quaternion.identity);
        }
        else if (SceneManagement.Instance.m_CharacterIndex == 3) // index 3 = Shadow
        {
            m_Player = Instantiate(m_ShadowPrefab, m_SpawnPos.position, Quaternion.identity);
        }
    }

    private void Start()
    {
        m_DefaultColor = m_Player.GetComponent<Renderer>().material.color;
        m_Timer = 0;
        m_TimerTick = 1;
    }

    public void TakeDamage()
    {
        DamageFeedback();

        if (m_HP == 0)
        {
            // Load game at checkpoint
            // LooseLife()
        }
    }

    private void DamageFeedback()
    {
        if (m_Coroutine == null)
        {
            m_HP -= 1;
            m_TimerTick = 1;
            m_Coroutine = StartCoroutine(ColorFeedBack());
        }
    }

    private IEnumerator ColorFeedBack()
    {
        while (m_TimerTick >= 0)
        {
            m_Timer += Time.deltaTime * 2;
            m_Player.GetComponent<Renderer>().material.color = Color.red;
            if (m_Timer >= m_TimerTick)
            {
                m_Player.GetComponent<Renderer>().material.color = m_DefaultColor;
                yield return new WaitForSeconds(m_TimerTick/5);
                m_Timer = 0;
                if (m_TimerTick <= 0.5)
                {
                    m_TimerTick -= 0.05f;
                }
                else
                {
                    m_TimerTick -= 0.1f;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    public void LooseLife()
    {
        m_Life -= 1;

        if (m_Life == 0)
        {
            // GameOver
        }
    }
}
