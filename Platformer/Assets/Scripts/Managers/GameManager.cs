using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DontDestroyOnLoad
{
    private static GameManager m_Instance;
    public static GameManager Instance
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
    }

    private void LoadMainSonic()
    {

    }

    private void LoadMainTails()
    {

    }

    private void LoadMainShadow()
    {
        
    }
}
