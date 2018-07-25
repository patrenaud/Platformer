using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WinImage;
    [SerializeField]
    private GameObject m_LooseImage;
    [SerializeField]
    private Button m_LoadButton;

    private void Start()
    {
        m_WinImage.SetActive(true);
        m_LooseImage.SetActive(true);
        if (PlayerManager.Instance.m_Win)
        {
            m_LooseImage.SetActive(false);
            m_LoadButton.interactable = false;
        }
        else
        {
            if(PlayerManager.Instance.m_Life == 0)
            {
                m_LoadButton.interactable = false;
                m_LoadButton.gameObject.SetActive(false);

                PlayerManager.Instance.m_Lives[0].SetActive(false);
            }            
            m_WinImage.SetActive(false);
        }
    }

    public void LoadGame()
    {
        SceneManagement.Instance.ChangeLevel("Game");
        PlayerManager.Instance.SetLife();
        PlayerManager.Instance.SetPlayer();
    }
   
}
