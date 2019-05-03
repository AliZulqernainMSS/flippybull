using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationTweener : BaseTweener
{
    public Vector3 initialRotation;
    public Vector3[] eulerAngles;
	private Sequence sequence;
    public Ease ease = Ease.Linear;
	private Vector3 originalRotation;

	#region implemented abstract members of BaseTweener
	public override void SetUp ()
	{
        originalRotation = transform.localEulerAngles;
        transform.localEulerAngles = initialRotation;
	}
	public override void PlayTween (float speed = 1.0f)
	{
		duration /= speed;
		float timePerTween = duration / ((eulerAngles.Length > 0) ? eulerAngles.Length : 1);
		sequence = DOTween.Sequence ();
		if (delay > 0) 
		{
			sequence.AppendInterval (delay);
		}
		foreach (var rotation in eulerAngles) 
		{
            sequence.Append (transform.DOLocalRotate(transform.eulerAngles + rotation, timePerTween).SetEase(ease));
		}
        sequence.Append (transform.DOLocalRotate(originalRotation, timePerTween).SetEase(ease));
		sequence.SetLoops (loops, loopType);
		sequence.Play ();
	}
	public override void KillTween ()
	{
		if(sequence != null)
		{
			sequence.Kill (true);
		}

        transform.localEulerAngles = originalRotation;
    }

    public override void Goto(float seconds, bool play)
    {
        sequence.Goto(seconds, play);
    }
	#endregion
	
}
