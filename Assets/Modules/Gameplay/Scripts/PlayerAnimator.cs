using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour 
{
	public Animator m_Animator;

	private readonly int k_JumpAnimation = Animator.StringToHash("Jump");
	private readonly int k_LandAnimation = Animator.StringToHash("Land");

	public void PlayLandAnimation()
	{
		m_Animator.Play (k_LandAnimation);
	}

	public void PlayJumpAnimation()
	{
		m_Animator.Play (k_JumpAnimation);
	}

}
