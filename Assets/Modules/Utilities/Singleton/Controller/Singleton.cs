using UnityEngine;
using UnityEngine.Networking;
// <summary>
// Be aware this will not prevent a non singleton constructor
//   such as `T myT = new T();`
// To prevent that, add `protected T () {}` to your singleton class.
// 
// As a note, this is made as MonoBehaviour because we need Coroutines.
// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    
    private static object _lock = new object();
    
    public static T Instance
    {
        get
		{
            lock(_lock)
			{
                if (_instance == null)
				{
                    // check if there is at least one instance in the scene 
                    _instance = (T) FindObjectOfType(typeof(T));
                    // if there is one, also check if there are multiple types which
                    // is not allowed
                    if ( _instance != null && FindObjectsOfType(typeof(T)).Length > 1 )
                    {
						Debug.LogException (new UnityException("More than one Singleton Found!"));
                        return null;
                    }
                    
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject
                        {
                            hideFlags = HideFlags.HideInHierarchy,
                            name = "(singleton) " + typeof(T)
                        };
                        _instance = singleton.AddComponent<T>();
						_instance.Initialize ();
                    } 
					else
					{
						Debug.LogWarning ("Singleton already found!");
					}
				}
                return _instance;
            }
        }
    }

	public virtual void Initialize()
	{
		
	}
}

public interface ISingleton
{
	void Open ();
	void Close ();
}