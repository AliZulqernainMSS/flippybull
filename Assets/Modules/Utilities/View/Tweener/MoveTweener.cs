using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTweener : BaseTweener
{
	public Vector3 initialPosition;
	public Vector3[] positions;
	private Sequence sequence;
	public Ease movementEase = Ease.Linear;
	private Vector3 originalPosition;

	#region implemented abstract members of BaseTweener
	public override void SetUp ()
	{
		originalPosition = transform.localPosition;
        transform.localPosition = initialPosition;
	}
	public override void PlayTween (float speed = 1.0f)
	{
		duration /= speed;
		float timePerTween = duration / ((positions.Length > 0) ? positions.Length : 1);
		sequence = DOTween.Sequence ();
		if (delay > 0) 
		{
			sequence.AppendInterval (delay);
		}
		foreach (var position in positions) 
		{
            sequence.Append (transform.DOLocalMove(originalPosition + position, timePerTween).SetEase(movementEase));
		}
        sequence.Append (transform.DOLocalMove(initialPosition, timePerTween).SetEase(movementEase));
		sequence.SetLoops (loops, loopType);
		sequence.Play ();
	}
	public override void KillTween ()
	{
		if(sequence != null)
		{
			sequence.Kill (true);
		}
        transform.localPosition = originalPosition;
    }

    public override void Goto(float seconds, bool play = false)
    {
        sequence.Goto(seconds, play);
    }
	#endregion
	
}
