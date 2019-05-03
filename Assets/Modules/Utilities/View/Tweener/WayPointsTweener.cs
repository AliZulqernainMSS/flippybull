using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class WayPointsTweener : BaseTweener
{
	public Ease movementEase = Ease.Linear;
	public int startIndex = 0;
	public Transform[] waypoints;

	private Sequence sequence;

	#region implemented abstract members of BaseTweener
	public override void SetUp ()
	{
		
	}
	public override void PlayTween (float speed = 1.0f)
	{
		duration /= speed;
		float timePerTween = duration / ((waypoints.Length > 0)? waypoints.Length : 1);
		sequence = DOTween.Sequence ();
		if (delay > 0) 
		{
			sequence.AppendInterval (delay);
		}
		sequence.Append(transform.DOPath(Array.ConvertAll(waypoints, (x)=> x.position).ToArray(), timePerTween).SetLookAt(0.01f).SetEase(movementEase));
		sequence.SetEase(movementEase);
		sequence.SetLoops (loops, loopType);
		sequence.Play ();
	}
	public override void KillTween ()
	{
		if(sequence != null)
		{
			sequence.Kill (true);
		}
    }

    public override void Goto(float seconds, bool play)
    {
        sequence.Goto(seconds, play);
    }
	#endregion
}
