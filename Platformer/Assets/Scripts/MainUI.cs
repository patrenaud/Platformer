using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private Button m_SButton;
    [SerializeField]
    private Button m_TButton;
    [SerializeField]
    private Button m_ShButton;

    public void CloseUI()
    {
        m_SButton.gameObject.SetActive(false);
        m_TButton.gameObject.SetActive(false);
        m_ShButton.gameObject.SetActive(false);
    }

}
