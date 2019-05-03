using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References
{
    private Dictionary<string, Object> cacheReferanceDict;

    public References()
    {
        cacheReferanceDict = new Dictionary<string, Object>(); 
        LoadSeed();
    }

    private void LoadSeed()
    {
        ReferencesView refs = Resources.Load<ReferencesView>("References");

        List<Object> references = new List<Object>();
        references.AddRange(refs.common);
        references.AddRange(refs.commonPrefabs);
        references.AddRange(refs.playerAvatarPrefabs);
        references.AddRange(refs.m_NPCPrefabs);
        references.AddRange(refs.m_NPCRagdolls);
        references.AddRange(refs.playerAvatarRagdollPrefabs);
		references.AddRange (refs.m_DestructibleProps);
		references.AddRange (refs.m_PropsPrefabs);
		references.AddRange (refs.m_HurdlesPrefabs);
		references.AddRange (refs.m_MatadorRagdolls);
        references.AddRange(refs.m_RaceRefereePrefabs);
        references.AddRange(refs.m_RaceRefereeRagdolls);

        foreach (var referenceObject in references)
        {
            if (referenceObject != null)
            {
                cacheReferanceDict.Add(referenceObject.name, referenceObject);
            }
        }
    }

    public GameObject LoadPrefab(string key)
    {
        return LoadObject<GameObject>(key);
    }

    public T LoadPrefab<T>(string key) where T : Component
    {
        return (LoadObject(key) as GameObject).GetComponent<T>();
    }

    public T LoadObject<T> (string key) where T : Object
    {
        return LoadObject(key) as T;
    }

    public Object LoadObject(string key)
    {
        if (!cacheReferanceDict.ContainsKey(key))
        {
            cacheReferanceDict[key] = Resources.Load(key);
            Debug.Log("key : "+ key);
        }
        return cacheReferanceDict[key];
    }
}
