using UnityEngine;
using System.Collections;

[System.Serializable]
public class ParticleSystemController : MonoBehaviour {
    ParticleSystem pSystem;
    public particleSystemType type;

    public void Start()
    {
        pSystem = gameObject.GetComponent<ParticleSystem>();
    }
    public void Emit(int amount)
    {
        print("AMOUTN " + amount);
        if (pSystem == null)
            print("System is null");
        pSystem.Emit(1);
    }
}
[System.Serializable]
public enum particleSystemType { LeftWall,MidWall }