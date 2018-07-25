using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMusic : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;
    private AudioSource m_AudioSource;

    public AudioClip m_WinClip;
    public AudioClip m_LooseClip;

    private void Awake()
    {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(PlayerManager.Instance.m_Win)
        {
            m_AudioSource.PlayOneShot(m_WinClip);
        }
        else
        {
            m_AudioSource.PlayOneShot(m_LooseClip);
        }
    }
}
