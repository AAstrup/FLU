using UnityEngine;
using System.Collections.Generic;

public class ParticleSystemAdmin : MonoBehaviour {
    public static ParticleSystemAdmin instance;
    void Awake() { instance = this; }

    Dictionary<particleSystemType, ParticleSystemController> dict = new Dictionary<particleSystemType, ParticleSystemController>();
    
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var script = gameObject.transform.GetChild(i).GetComponent<ParticleSystemController>();
            if (script == null)
                print("IS NULL!");
            dict.Add(script.type, script);
            print("Type " + script.type.ToString() + " added" );
        }
    }

	public void Emit(int amount,particleSystemType type)
    {
        foreach (KeyValuePair<particleSystemType, ParticleSystemController> entry in dict)
        {
            entry.Value.Emit(amount);
        }
    }
}