using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class BaseTweener : MonoBehaviour
{
    public bool playOnAwake = true;
	public float duration;
	public int loops;
	public LoopType loopType;
	public float delay = 0;

	public abstract void SetUp();
	public abstract void PlayTween(float speed = 1.0f);
    public abstract void KillTween();
    public abstract void Goto(float seconds, bool play = false);

	void Awake()
	{
		SetUp();
	}

	void OnEnable()
	{
        if(playOnAwake)
        {
            PlayTween();
        }
	}

	void OnDisable()
	{
		KillTween();
	}
}
