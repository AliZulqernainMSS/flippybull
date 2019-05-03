using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class ColorTweener : BaseTweener 
{
	public Color preAnimationColor;
	public Color[] targetColors;
	private Image targetImage;
	private Sequence sequence;

	public override void SetUp ()
	{
		targetImage = GetComponent<Image> ();
		targetImage.color = preAnimationColor;
	}

	public override void PlayTween (float speed = 1.0f)
	{
		duration /= speed;
		float timePerTween = duration / targetColors.Length;
		sequence = DOTween.Sequence ();
		foreach (var color in targetColors) 
		{
			sequence.Append (targetImage.DOColor(color, timePerTween));
		}
		if (delay > 0) 
		{
			sequence.AppendInterval (delay);
		}
		sequence.SetLoops (loops, loopType);
		sequence.Play ();
	}

	public override void KillTween ()
	{
		if(sequence != null)
		{
			sequence.Kill (true);
		}
        targetImage.color = preAnimationColor;
	}

    public override void Goto(float seconds, bool play)
    {
        sequence.Goto(seconds, play);
    }
}
