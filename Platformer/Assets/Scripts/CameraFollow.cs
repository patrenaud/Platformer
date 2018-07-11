using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour 
{

[SerializeField]
private Transform m_ToFollow;
[SerializeField]
private float m_FollowSpeed = 3f;
[SerializeField]
private float m_Offset = 2f;

private Vector3 m_MovePos = new Vector3();

	private void Start () 
	{
		m_MovePos = transform.position;
	}
	

	private void LateUpdate () 
	{
		m_MovePos.z = -10f;
		m_MovePos.x = Mathf.Lerp(transform.position.x, m_ToFollow.position.x + m_Offset, m_FollowSpeed * Time.deltaTime);
		transform.position = m_MovePos;
	}
}
