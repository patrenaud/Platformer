using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringsBehavior : MonoBehaviour
{
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Sonic") ||
           aOther.gameObject.layer == LayerMask.NameToLayer("Tails") ||
           aOther.gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            m_Animator.SetBool("Restart", false);
            m_Animator.SetTrigger("Active");            
            StartCoroutine(EndCollision());
        }
    }

    private IEnumerator EndCollision()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        m_Animator.SetBool("Restart", true);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
