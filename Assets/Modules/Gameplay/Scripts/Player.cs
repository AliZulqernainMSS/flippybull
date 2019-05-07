using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
	public PlayerAnimator m_Animator;
	public Collider m_PlayerCollider;
	public ParticleSystem m_DeathParticle;
	[SerializeField]
	private float m_JumpPower = 5f;
	[SerializeField]
	private float m_JumpSpeed = 6f;
	private float m_HalfScreenWidth;
	private float m_FlipOffset = 15f;
	private int m_CurrentOffset = 0;
	private float m_LastBackFlipTime = -1;
	public Vector3 m_LastPosition;

	private void Start() 
	{
		m_HalfScreenWidth = Screen.width * 0.5f;
		m_FlipOffset = GameConstants.k_CellOffset;
	}

	private void OnEnable()
	{
		m_PlayerCollider.AddOnTiggerEnterListener (TriggerDetected);
	}

	private void OnDisable()
	{
		m_PlayerCollider.RemoveOnTiggerEnterListener (TriggerDetected);
	}
	
	void Update () 
	{
#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            DoFlip(1);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DoFlip(-1);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
            DoBackFlip();
		}
#else
		if(Input.GetMouseButtonDown(0))
		{
			float x = Input.mousePosition.x;
			if(x > m_HalfScreenWidth)
			{
                DoFlip(1);
			}
			else
			{
                DoFlip(-1);
			}
		}
#endif
    }

	#region Movement

	public void DoBackFlip()
	{
		if ((Time.realtimeSinceStartup - m_LastBackFlipTime) < 1)
		{
			return;
		}
		m_LastBackFlipTime = Time.realtimeSinceStartup;
		transform.DOKill(true);
		m_LastPosition = transform.position;
		m_Animator.PlayJumpAnimation();
        Vector3 position = transform.position;
        position.z -= m_FlipOffset;
		transform.DOJump(position, m_JumpPower, 1, 1f / m_JumpSpeed);
	}

	public void DoFlip(int flip)
	{
		transform.DOKill(true);
		m_LastPosition = transform.position;
		m_Animator.PlayJumpAnimation();
        m_CurrentOffset += flip;
        Vector3 position = transform.position;
        position.z += m_FlipOffset;
        position.x = m_CurrentOffset * m_FlipOffset;
		transform.DOLocalRotate (Vector3.up * flip * 45f, 0.1f).OnComplete(()=>{
			transform.DOLocalRotate(Vector3.zero, 0.1f).SetDelay(0.05f);
		});
		transform.DOJump(position, m_JumpPower, 1, 1f / m_JumpSpeed).SetEase(Ease.Linear).SetDelay(0.03f)
		.OnComplete(() =>
		{
            if (Mathf.Abs(m_CurrentOffset) > 1)
			{
				m_CurrentOffset = (int)Mathf.Sign(m_CurrentOffset) * -1;
                position = transform.position;
                position.x = m_CurrentOffset * m_FlipOffset;
				transform.position = new Vector3(position.x + (m_CurrentOffset * m_FlipOffset), position.y, position.z);
				m_LastPosition = transform.position;
				transform.DOJump(position, m_JumpPower, 1, 1f / m_JumpSpeed).SetEase(Ease.Linear);
			}
			else
			{
				m_Animator.PlayLandAnimation();
			}
		});
	}
	#endregion

	public void Die()
	{
		this.enabled = false;
		m_Animator.gameObject.SetActive (false);
		m_DeathParticle.Play ();
	}

	public void LevelComplete()
	{
		if (this.enabled == false) 
		{
			return;
		}
		this.enabled = false;
		m_Animator.transform.localEulerAngles = Vector3.up * 180f;
		Vector3 position = transform.position;
		position.x = 0;
		transform.position = position;
		transform.DOJump (position, m_JumpPower, 1, 1f / m_JumpSpeed)
			.SetDelay (0.3f)
			.SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Restart);
		GameManager.Instance.LevelComplete ();
	}

	private void TriggerDetected(Collider collider)
	{
		if (collider.CompareTag ("Trap")) 
		{
			Die ();
			GameManager.Instance.PlayerDied ();
		}
		else if (collider.CompareTag ("Finish")) 
		{
			LevelComplete ();
		}
	}

}
