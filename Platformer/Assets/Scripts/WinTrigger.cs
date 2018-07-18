using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Sonic") ||
            aOther.gameObject.layer == LayerMask.NameToLayer("Tails") ||
            aOther.gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            PlayerManager.Instance.m_Win = true;
            SceneManagement.Instance.ChangeLevel("Results");
        }
    }
}
