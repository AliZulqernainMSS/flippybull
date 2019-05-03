using UnityEngine;
using System.Collections;
using System.Text;

public class cPlayerPrefs
{
	#region PrivateConstant
	private const int INT_KEY = kPlayerPrefs.INT_KEY + kPlayerPrefs.INT_KEY_LAST;
	private const int FLOAT_KEY = kPlayerPrefs.FLOAT_KEY + kPlayerPrefs.FLOAT_KEY_LAST;
    private const int DOUBLE_KEY = kPlayerPrefs.DOUBLE_KEY + kPlayerPrefs.DOUBLE_KEY_LAST;
	private const int STRING_KEY = kPlayerPrefs.STRING_KEY + kPlayerPrefs.STRING_KEY_LAST;
	private const int BOOL_KEY = kPlayerPrefs.BOOL_KEY + kPlayerPrefs.BOOL_KEY_LAST;
	#endregion

	#region SavePrefs
	public void WriteToDisk()
	{
		PlayerPrefs.Save();
	}
	#endregion

	#region IntegerEncryptDecryption
	public void SetInt(string key , int value)
	{
		PlayerPrefs.SetString (EncryptDecrypt(key,INT_KEY),EncryptDecrypt(value.ToString(),INT_KEY));
	}

	public int GetInt(string key , int defaultValue)
	{
		string value = string.Empty;
		int val = defaultValue;
		try{
			value = PlayerPrefs.GetString (EncryptDecrypt(key,INT_KEY),EncryptDecrypt(defaultValue.ToString(),INT_KEY));
			value = EncryptDecrypt (value, INT_KEY);
			val = int.Parse (value);
		}
		catch(UnityException ex)
		{
			Debug.LogError (ex.StackTrace);
			return defaultValue;

		}
		return val;
	}
	#endregion

    #region FloatEncryptDecryption
    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetString(EncryptDecrypt(key, FLOAT_KEY), EncryptDecrypt(value.ToString(), FLOAT_KEY));
    }

    public float GetFloat(string key, float defaultValue)
    {
        string value = EncryptDecrypt(defaultValue.ToString(), FLOAT_KEY);
        float val = defaultValue;
        try
        {
            value = PlayerPrefs.GetString(EncryptDecrypt(key, FLOAT_KEY), EncryptDecrypt(defaultValue.ToString(), FLOAT_KEY));
            value = EncryptDecrypt(value, FLOAT_KEY);
            val = float.Parse(value);
        }
        catch (UnityException ex)
        {
            Debug.LogError(ex.StackTrace);
            return defaultValue;

        }
        return val;
    }
    #endregion

	#region DoubleEncryptDecryption
    public void SetDouble(string key , double value)
	{
        PlayerPrefs.SetString (EncryptDecrypt(key,DOUBLE_KEY),EncryptDecrypt(value.ToString(),DOUBLE_KEY));
	}

    public double GetDouble(string key , double defaultValue)
	{
        string value = EncryptDecrypt (defaultValue.ToString (), DOUBLE_KEY);
        double val = defaultValue;
		try
		{
            value = PlayerPrefs.GetString (EncryptDecrypt(key,DOUBLE_KEY),EncryptDecrypt(defaultValue.ToString(),DOUBLE_KEY));
            value = EncryptDecrypt (value, DOUBLE_KEY);
            val = double.Parse (value);
		}
		catch(UnityException ex)
		{
			Debug.LogError (ex.StackTrace);
            return defaultValue;

		}
		return val;
	}
	#endregion

	#region StringEncryptDecryption
	public void SetString(string key , string value)
	{
		PlayerPrefs.SetString (EncryptDecrypt(key,STRING_KEY),EncryptDecrypt(value,STRING_KEY));
	}

	public string GetString(string key , string defaultValue)
	{
		string value = PlayerPrefs.GetString (EncryptDecrypt(key,STRING_KEY),EncryptDecrypt(defaultValue,STRING_KEY));
		value = EncryptDecrypt (value, STRING_KEY);
		return value;
	}
	#endregion

	#region BoolEncryptDecryption
	public void SetBool(string key , bool value)
	{
		PlayerPrefs.SetString (EncryptDecrypt(key,BOOL_KEY),EncryptDecrypt(value.ToString(),BOOL_KEY));
	}

	public bool GetBool(string key , bool defaultValue)
	{

		string value = string.Empty;
		bool val = defaultValue;
		try
		{
			value = PlayerPrefs.GetString (EncryptDecrypt(key,BOOL_KEY),EncryptDecrypt(defaultValue.ToString(),BOOL_KEY));
			value = EncryptDecrypt (value, BOOL_KEY);
			val = bool.Parse (value);
		}
		catch(UnityException ex)
		{
			Debug.LogError (ex.StackTrace);
			return defaultValue;
		}

		return val;

	}
	#endregion

	#region EncryptDecrypt
	private string EncryptDecrypt(string text, int key)
	{
		StringBuilder inSb = new StringBuilder(text);
		if(string.IsNullOrEmpty(text))
		{
			return text;
		}
		StringBuilder outSb = new StringBuilder(text.Length);
		char c;
		for (int i = 0; i < text.Length; i++)
		{
			c = inSb[i];
			c = (char)(c ^ key);
			outSb.Append(c);
		}
		return outSb.ToString();
	}
	#endregion

}