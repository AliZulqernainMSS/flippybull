using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum AnimationType
{
    LeftToRight,
    RightToLeft,
    TopToBottom,
    BottomToTop
}

public class cAnimation : Singleton<cAnimation>
{
    #region Variables
    public delegate void CallBackDelegate();
    #endregion

    #region HoverAnimation

    public void PlayHoverAnimation(Transform transform, float animationTime, float bottomPosition, float topPosition, bool isLooping)
    {
        PlayHoverAnimation(transform, animationTime, bottomPosition, topPosition, isLooping, null);
    }

    public void PlayHoverAnimation(Transform transform, float animationTime, float bottomPosition,float topPosition, bool isLooping,CallBackDelegate animationCompleteCallBack)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(topPosition, animationTime))
                .Append(transform.DOLocalMoveY(bottomPosition, animationTime))
                .AppendCallback(() =>
                {
                    if (isLooping)
                    {
                PlayHoverAnimation(transform,animationTime,bottomPosition,topPosition,isLooping,animationCompleteCallBack);
                    }
                    else
                    {
                        if (animationCompleteCallBack != null)
                        {
                            animationCompleteCallBack();
                        }
                    }
                });
    }
    #endregion

    #region RotateAnimation
    public void PlayRotateAnimation(Transform transform, float animationTime, Vector3 endAngle)
    {
        PlayRotateAnimation(transform, animationTime, endAngle, null);
    }

    public void PlayRotateAnimation(Transform transform, float animationTime, Vector3 endAngle, CallBackDelegate animationCompleteCallBack)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(endAngle,animationTime))
                .AppendCallback(() =>
                {
                    if (animationCompleteCallBack != null)
                    {
                        animationCompleteCallBack();
                    }
                });
    }
    #endregion

    #region PunchAnimation
	public void PlayPunchAnimation(Transform transform)
	{
		PlayPunchAnimation(transform, 0.15f, 0.075f, null);
	}

	public void PlayPunchAnimation(Transform transform, CallBackDelegate animationCompleteCallBack)
	{
		PlayPunchAnimation(transform, 0.15f, 0.075f, animationCompleteCallBack);
	}

    public void PlayPunchAnimation(Transform transform, float animationTime)
    {
        PlayPunchAnimation(transform,animationTime,.075f, null);
    }

    public void PlayPunchAnimation(Transform transform, float punchSize, float animationTime)
    {
        PlayPunchAnimation(transform, animationTime,punchSize, null);
    }

    public void PlayPunchAnimation(Transform transform, float animationTime, CallBackDelegate animationCompleteCallBack)
    {
        PlayPunchAnimation(transform,animationTime,0.075f,animationCompleteCallBack);
    }

    public void PlayPunchAnimation(Transform transform,float animationTime, float punchSize, CallBackDelegate animationCompleteCallBack)
    {
        // Sequence sequence = DOTween.Sequence();
        // sequence.Append(transform.DOPunchScale(new Vector3(punchSize, punchSize,0f), animationTime, 2, 1f))
        //         .AppendCallback(() =>
        //         {
        //             if (animationCompleteCallBack != null)
        //             {
        //                 animationCompleteCallBack();
        //             }
        //         });
        transform.DOKill(true);
        transform.DOPunchScale(new Vector3(punchSize, punchSize,0f), animationTime, 2, 1f).OnComplete(()=>
        {
            if (animationCompleteCallBack != null)
            {
                animationCompleteCallBack();
            }
        });
    }

    public void PlayScaleAnimation(Transform transform,float endValue, float animationTime)
    {
        PlayScaleAnimation(transform,endValue, animationTime, null);
    }

    public void PlayScaleAnimation(Transform transform, float endValue, float animationTime, CallBackDelegate animationCompleteCallBack)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(endValue, animationTime))
                .AppendCallback(() =>
                {
                    if (animationCompleteCallBack != null)
                    {
                        animationCompleteCallBack();
                    }
                });
    }
    #endregion

    #region Animation
    public void PlayAnimation(AnimationType type, Transform transform, float animationTime)
    {
        PlayAnimation(type, transform, animationTime, -1111f, -1111f, null);
    }

    public void PlayAnimation(AnimationType type, Transform transform, float animationTime,CallBackDelegate animationCompleteCallBack)
    {
        PlayAnimation(type,transform,animationTime,-1111f,-1111f,animationCompleteCallBack);
    }

    public void PlayAnimation(AnimationType type, Transform transform, float animationTime, float from, float to)
    {
        PlayAnimation(type,transform,animationTime,from,to,null);
    }

    public void PlayAnimation(AnimationType type, Transform transform, float animationTime, float from, float to,CallBackDelegate animationCompleteCallBack)
    {
        if(to == -1111)
        {
            to = 0f;
        }

        Sequence sequence = DOTween.Sequence();

        if(from != -1111)
        {
            if(type == AnimationType.LeftToRight || type == AnimationType.RightToLeft)
            {
                sequence.Append(transform.DOLocalMoveX(from,0.01f));
            }
            else
            {
                sequence.Append(transform.DOLocalMoveY(from, 0.01f));   
            }

        }
        if (type == AnimationType.LeftToRight || type == AnimationType.RightToLeft)
        {
            sequence.Append(transform.DOLocalMoveX(to, animationTime));
        }
        else
        {
            sequence.Append(transform.DOLocalMoveY(to,animationTime));
        }
        sequence.AppendCallback(() =>
                {
                    if (animationCompleteCallBack != null)
                    {
                        animationCompleteCallBack();
                    }
                })
                .Play();
    }
    #endregion

    public void PlayAnimation(AnimationType type, RectTransform transform, float animationTime, float from, float to, CallBackDelegate animationCompleteCallBack)
    {
        if (to == -1111)
        {
            to = 0f;
        }

        Sequence sequence = DOTween.Sequence();

        if (from != -1111)
        {
            if (type == AnimationType.LeftToRight || type == AnimationType.RightToLeft)
            {
                sequence.Append(transform.DOAnchorPosX(from, 0.01f));
            }
            else
            {
                sequence.Append(transform.DOAnchorPosY(from, 0.01f));
            }

        }
        if (type == AnimationType.LeftToRight || type == AnimationType.RightToLeft)
        {
            sequence.Append(transform.DOAnchorPosX(to, animationTime));
        }
        else
        {
            sequence.Append(transform.DOAnchorPosY(to, animationTime));
        }
        sequence.AppendCallback(() =>
        {
            if (animationCompleteCallBack != null)
            {
                animationCompleteCallBack();
            }
        })
                .Play();
    }


    #region FadeAnimation
    public void PlayFadeAnimation(Image image, float animationTime)
    {
        PlayFadeAnimation(image,animationTime,-1f,-1f,null);
    }

    public void PlayFadeAnimation(Image image, float animationTime, CallBackDelegate animationCompleteCallBack)
    {
        PlayFadeAnimation(image, animationTime, -1f, -1f, animationCompleteCallBack);
    }

    public void PlayFadeAnimation(Image image, float animationTime, float from, float to)
    {
        PlayFadeAnimation(image, animationTime, from, to, null);
    }

    public void PlayFadeAnimation(Image image, float animationTime, float from, float to, CallBackDelegate animationCompleteCallBack)
    {
        if (to == -1)
        {
            to = 0.5f;
        }
        Sequence sequence = DOTween.Sequence();

        if (from != -1)
        {
            sequence.Append(image.DOFade(from, 0.01f));
        }
        sequence.Append(image.DOFade(to, animationTime));

        sequence.AppendCallback(() =>
        {
            if (animationCompleteCallBack != null)
            {
                animationCompleteCallBack();
            }
        })
        .Play();
    }
    #endregion

    #region ShakeAnimation
    
	public void PlayShakeAnimation( Transform transform, float intensity, float animationTime, float delay = 0f, CallBackDelegate animationCompleteCallBack = null)
    {
        PlayShakeAnimation(transform, intensity, 25, animationTime, delay, animationCompleteCallBack);
    }

    public void PlayShakeAnimation(Transform transform, float intensity, int vibrato, float animationTime, float delay = 0f, CallBackDelegate animationCompleteCallBack = null)
    {
        transform.DOKill(true);
        transform.DOShakePosition(animationTime, intensity, vibrato, 90, false, false).SetDelay(delay)
            .OnComplete(() =>
            {
                if (animationCompleteCallBack != null)
                {
                    animationCompleteCallBack();
                }
            });
    }

    #endregion

    #region NumberTextAnimation

    public void PlayNumberTextAnimation(Text text, double initialValue, double finalValue, float duration = 0.5f)
    {
        DOTween.To(() => initialValue, x => initialValue = x, finalValue, 0.5f).OnUpdate(() =>
        {
				text.text = initialValue.ToString();
        });
    }

    #endregion

	#region FadeAnimation
	public void PlayScaleBobAnimation (RectTransform rectTransform, float durration, Vector3 bobScale, int loops)
	{
		PlayScaleBobAnimation (rectTransform.transform, durration, bobScale, loops);
	}

	public void PlayScaleBobAnimation (Transform objectTransform, float durration, Vector3 bobScale, int loops)
	{
		Vector3 initialScale = objectTransform.localScale;
		Sequence sequence = DOTween.Sequence ();
		sequence.Append(objectTransform.DOScale (bobScale, durration/2));
		sequence.Append(objectTransform.DOScale (initialScale, durration/2));
		sequence.SetLoops (loops);
	}
	#endregion

	#region Notification Animation

	public void PlayNotificationAnimation (Transform notification)
	{
		float finalPosition = notification.transform.localPosition.y;
		notification.localPosition -= Vector3.up * 1100f;
		notification.DOLocalMoveY (finalPosition, 1f);
	}

	#endregion

	#region Floater Animation

	private Sequence floaterAnimationSequence;

	public void PlayFloaterAnimation (Transform floater, TweenCallback callBack)
	{
		if (floaterAnimationSequence != null) 
		{
			floaterAnimationSequence.Kill (true);
		}

		Vector3 floaterScale = Vector3.one;
		floaterScale.x = 0;
		floater.localScale = floaterScale;

		floaterAnimationSequence = DOTween.Sequence ();
		floaterAnimationSequence.Append(floater.DOScaleX (1, 0.3f).SetEase (Ease.OutBack))
			.AppendInterval(1)
			.Append(floater.DOScale (Vector3.one * 0.1f, 0.1f))
			.AppendCallback(callBack);
	}

	public void PlayFloaterAnimation (Transform floater, Vector3 targetScale, TweenCallback callBack = null)
	{
		Vector3 floaterScale = Vector3.one;
		floaterScale.x = 0;
		floater.localScale = floaterScale;

		Sequence sequence = DOTween.Sequence ();
		sequence.Append(floater.DOScaleX (1, 0.3f).SetEase (Ease.OutBack))
			.AppendInterval(1)
			.Append(floater.DOScale (targetScale, 0.1f))
			.AppendCallback(callBack);
	}

	#endregion


	#region Tutorial Animation

	public void PlayTutorialArrowAnimation (RectTransform tutorialArrow)
	{
		Vector2 anchoredPosition = tutorialArrow.anchoredPosition;
		Vector2 scale = tutorialArrow.localScale;

		tutorialArrow.anchoredPosition = new Vector2(0, 800);
		tutorialArrow.localScale = scale * 10f;

		Sequence seq = DOTween.Sequence();
		seq.Append(tutorialArrow.DOLocalMoveY(anchoredPosition.y, 0.55f).SetEase(Ease.InQuad));
		seq.Join(tutorialArrow.DOScale(scale, 0.55f).SetEase(Ease.InQuad));
		seq.AppendCallback(
			()=>{
				tutorialArrow.DOAnchorPosY(anchoredPosition.y + 30f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);
			}
		);
	}

	#endregion

}
