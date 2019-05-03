using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System;
using Random = System.Random;
using System.Security.Cryptography;

public static class ExtensionMethods
{
    private static Random randomNumberGenerator = new Random();

    public static void Invoke(this MonoBehaviour me, Action theDelegate, float time)
    {
        me.StartCoroutine(ExecuteAfterTime(theDelegate, time));
    }

    private static IEnumerator ExecuteAfterTime(Action theDelegate, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (theDelegate != null)
        {
            theDelegate();
        }
    }

    public static RectTransform ResetRectTranformOffset(this GameObject target)
    {
        return ResetRectTranformOffset(target.GetComponent<RectTransform>());
    }

    public static RectTransform ResetRectTranformOffset(this Transform target)
    {
        return ResetRectTranformOffset(target.GetComponent<RectTransform>());
    }

    public static RectTransform ResetRectTranformOffset(this RectTransform target)
    {
        target.offsetMin = new Vector2(0, 0);
        target.offsetMax = new Vector2(0, 0);
        return target;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCurrentMethodName(this MonoBehaviour myObject, int frameLayer = 1)
    {
        var st = new StackTrace();
        var sf = st.GetFrame(frameLayer);

        return sf.GetMethod().Name;
    }

    public static void LogMethodCall(this MonoBehaviour myObject)
    {
        UnityEngine.Debug.Log(myObject.GetType().Name + " : " + myObject.GetCurrentMethodName(2), myObject);
    }

    public static Vector2 RandomPoint(this Rect rect)
    {
        return RandomPointInRect(rect.center, rect.size);
    }

    public static Vector2 RandomPoint(this RectTransform rectTransform)
    {
        return RandomPointInRect(rectTransform.rect.center, rectTransform.rect.size);
    }

    public static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
            (UnityEngine.Random.value - 0.5f) * size.x,
            (UnityEngine.Random.value - 0.5f) * size.y,
            (UnityEngine.Random.value - 0.5f) * size.z
        );
    }

    public static Vector3 RandomPointInBoxWithinRange(Vector3 minSize, Vector3 thickeness)
    {
        float xAxis = Mathf.Sign(UnityEngine.Random.value - 0.5f) * UnityEngine.Random.value;
        float zAxis = (1f - Mathf.Abs(xAxis)) * Mathf.Sign(UnityEngine.Random.value - 0.5f);

        float finalXAxis = xAxis + zAxis;
        float finalZAxis = zAxis - xAxis;

        //Clamp over upper side
        finalZAxis = Mathf.Abs(finalZAxis);

        finalXAxis *= minSize.x + UnityEngine.Random.Range(0, thickeness.x);
        finalZAxis *= minSize.z + UnityEngine.Random.Range(0, thickeness.z);

        return new Vector3(finalXAxis, 0, finalZAxis);
    }

    private static Vector2 RandomPointInRect(Vector2 center, Vector2 size)
    {
        return center + new Vector2(
            (UnityEngine.Random.value - 0.5f) * size.x,
            (UnityEngine.Random.value - 0.5f) * size.y
        );
    }

    public static T GetValue<T>(this Dictionary<string, T> dictionary, string key)
    {
        T value;
        if (dictionary.TryGetValue(key, out value))
        {
            return value;
        }
        return default(T);
    }

    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0) return min;
        else if (val.CompareTo(max) > 0) return max;
        else return val;
    }

    public static float RandomSign()
    {
        return Mathf.Pow(-1, UnityEngine.Random.Range(0, 2));
    }

    public static int GetRandomProbabilityIndex(this float[] ProbabilityArray)
    {
        if (ProbabilityArray.Length <= 0)
        {
            return -1;
        }
        int result = 0;
        float random = UnityEngine.Random.Range(0f, 100.1f);
        float probSum = ProbabilityArray[0];
        for (int i = 0; i < ProbabilityArray.Length; i++)
        {
            if (random <= probSum)
            {
                result = i;
                break;
            }
            else
            {
                probSum += ProbabilityArray[i];
            }
        }
        return result;
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = randomNumberGenerator.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void PureShuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void SetAlpha(this TextMesh textMesh, float alpha)
    {
        Color color = textMesh.color;
        color.a = alpha;
        textMesh.color = color;
    }

    public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
    {
        vector.x = Mathf.Clamp(vector.x, min.x, max.x);
        vector.y = Mathf.Clamp(vector.y, min.y, max.y);
        vector.z = Mathf.Clamp(vector.z, min.z, max.z);
        return vector;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if(component != null)
        {
            return component;
        }
        component = gameObject.AddComponent<T>();
        return component;
    }

    public static Vector3 ResetHeight(this Vector3 position, float value = 0)
    {
        position.y = value;
        return position;
    }

}
