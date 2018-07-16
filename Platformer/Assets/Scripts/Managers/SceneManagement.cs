using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : DontDestroyOnLoad
{
    [SerializeField] private GameObject m_LoadingScreen;
    [SerializeField] private GameObject m_MenuScreen;
    public int m_CharacterIndex;    

    private static SceneManagement m_Instance;
    public static SceneManagement Instance
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
        m_LoadingScreen.SetActive(false);
        m_MenuScreen.SetActive(true);
    }


    private void StartLoading()
    {
        m_LoadingScreen.SetActive(true);
        m_MenuScreen.SetActive(false);

    }

    private void OnLoadingDone(Scene i_Scene, LoadSceneMode i_Mode)
    {
        SceneManager.sceneLoaded -= OnLoadingDone;
        m_LoadingScreen.SetActive(false);
    }

    public void ChangeLevel(string i_Scene)
    {
        StartLoading();

        StartCoroutine(LoadingTimer(i_Scene));

        SceneManager.sceneLoaded += OnLoadingDone;
    }

    private IEnumerator LoadingTimer(string i_Scene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(i_Scene);
    }


    public void LoadAsSonic()
    {
        SceneManagement.Instance.ChangeLevel("Game");
        m_CharacterIndex = 1;
    }

    public void LoadAsTails()
    {
        SceneManagement.Instance.ChangeLevel("Game");
        m_CharacterIndex = 2;
    }

    public void LoadAsShadow()
    {
        SceneManagement.Instance.ChangeLevel("Game");
        m_CharacterIndex = 3;
    }    
}
