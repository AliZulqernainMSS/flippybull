using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
	[SerializeField]
	private Image fillerImage;
	[SerializeField, HideInInspector]
	private float _value;
	[SerializeField, HideInInspector]
	private float _minValue = 0;
	[SerializeField, HideInInspector]
	private float _maxValue = 1;
	
	public float Value
	{
		get
		{
			return _value;
		}

		set
		{
			_value = value;
			_value = Mathf.Clamp (_value, _minValue, _maxValue);
			OnValueChanged ();
		}
	}

	public float MinValue
	{
		get
		{
			return _minValue;
		}
	}

	public float MaxValue
	{
		get
		{
			return _maxValue;
		}

		set
		{
			_maxValue = value;
			UpdateLayout ();
		}
	}

	private void OnValueChanged ()
	{
		UpdateLayout ();
	}

	private void UpdateLayout ()
	{
		if (fillerImage == null) 
		{
			return;
		}
		float percentage = _value / _maxValue;
		fillerImage.fillAmount = percentage;
	} 

	#if UNITY_EDITOR
	void Update()
	{
		UpdateLayout ();
	}
	#endif
}
