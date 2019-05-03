using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleTweener : BaseTweener
{
	public Ease movementEase = Ease.Linear;
	public Vector3 preanimationScale = Vector3.zero;
	public Vector3[] scales;

	private Vector3 originalScale = Vector3.zero;
	private Sequence sequence;

	#region implemented abstract members of BaseTweener
	public override void SetUp ()
	{
		originalScale = transform.localScale;
		transform.localScale = preanimationScale;
	}
	public override void PlayTween (float speed = 1.0f)
	{
		duration /= speed;
		float timePerTween = duration / ((scales.Length > 0)? scales.Length : 1);
		sequence = DOTween.Sequence ();
		if (delay > 0) 
		{
			sequence.AppendInterval (delay);
		}
		foreach (var scale in scales) 
		{
			sequence.Append (transform.DOScale(scale, timePerTween).SetEase(movementEase));
		}
		sequence.Append (transform.DOScale(originalScale, timePerTween).SetEase(movementEase));
		sequence.SetLoops (loops, loopType);
		sequence.Play ();
	}
	public override void KillTween ()
	{
		if(sequence != null)
		{
			sequence.Kill (true);
		}
        transform.localScale = originalScale;
    }

    public override void Goto(float seconds, bool play)
    {
        sequence.Goto(seconds, play);
    }
	#endregion
	
}
