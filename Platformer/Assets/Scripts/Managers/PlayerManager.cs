using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : DontDestroyOnLoad
{
    [HideInInspector]
    public GameObject m_Player;
    [SerializeField]
    private List<GameObject> m_Lives = new List<GameObject>();
    [SerializeField]
    private GameObject m_SonicPrefab; // index 1
    [SerializeField]
    private GameObject m_TailsPrefab; // index 2
    [SerializeField]
    private GameObject m_ShadowPrefab; // index 3
    [SerializeField]
    private Transform m_SpawnPos1;
    [SerializeField]
    private Transform m_CheckPoint1;
    [SerializeField]
    private Transform m_CheckPoint2;
    private Color m_DefaultColor;
    private float m_Timer;
    private float m_TimerTick;

    public Coroutine m_Coroutine;
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
            m_Player = Instantiate(m_SonicPrefab, m_SpawnPos1.position, Quaternion.identity);
        }
        else if (SceneManagement.Instance.m_CharacterIndex == 2) // index 2 = Tails
        {
            m_Player = Instantiate(m_TailsPrefab, m_SpawnPos1.position, Quaternion.identity);
        }
        else if (SceneManagement.Instance.m_CharacterIndex == 3) // index 3 = Shadow
        {
            m_Player = Instantiate(m_ShadowPrefab, m_SpawnPos1.position, Quaternion.identity);
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
        // Ceci permet au joueur de ne pas se faire toucher lorsqu'il clignote en rouge et que la Coroutine est active
        if (m_Coroutine == null)
        {
            m_HP -= 1;
            DamageFeedback();

            if (m_HP == 1)
            {
                m_Player.GetComponent<PlayerController>().ScaleDown();
            }

            // Si le joueur arrive à 0 de HP, Sa nouvelle position de spawn dépendera des checkpoints activés.
            if (m_HP == 0)
            {
                if (m_Player.GetComponent<PlayerController>().m_CheckPoint2Done)
                {
                    ReplacePlayer(m_CheckPoint2.transform);

                }
                else if (m_Player.GetComponent<PlayerController>().m_CheckPoint1Done)
                {
                    ReplacePlayer(m_CheckPoint1.transform);
                }
                else
                {
                    ReplacePlayer(m_SpawnPos1.transform);
                }
                LooseLife();
            }
        }
    }

    // Si le joueur n'est pas déjà touché, le dommage s'effectuera
    private void DamageFeedback()
    {
        if (m_Coroutine == null)
        {
            m_TimerTick = 1;
            m_Coroutine = StartCoroutine(ColorFeedBack());
        }
    }

    // Lorsque le joueur est touché et perd de la vie, un clognotant rouge se crée et protège le joueur des autres dommages.
    private IEnumerator ColorFeedBack()
    {

        while (m_TimerTick >= 0)
        {
            if (m_Player != null)
            {
                m_Timer += Time.deltaTime * 2;
                m_Player.GetComponent<Renderer>().material.color = Color.red;
                if (m_Timer >= m_TimerTick)
                {
                    m_Player.GetComponent<Renderer>().material.color = m_DefaultColor;
                    yield return new WaitForSeconds(m_TimerTick / 5);
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
            else
            {
                yield return null;
            }
        }
        m_Coroutine = null;
    }

    // Lorsque le joueur n'a plus de vies, GAMEOVER
    public void LooseLife()
    {
        m_Life -= 1;
        if (m_Life == 2)
        {
            m_Lives[0].SetActive(true);
            m_Lives[1].SetActive(true);
            m_Lives[2].SetActive(false);
        }
        else if (m_Life == 1)
        {
            m_Lives[0].SetActive(true);
            m_Lives[1].SetActive(false);
            m_Lives[2].SetActive(false);
        }

        if (m_Life == 0)
        {
            SceneManagement.Instance.ChangeLevel("Results");
        }
    }

    private void ReplacePlayer(Transform i_Pos)
    {
        m_Player.GetComponent<PlayerController>().ScaleUp();
        m_Player.transform.position = i_Pos.position;
        m_HP = 2;
    }
}
