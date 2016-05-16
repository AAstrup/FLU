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
        pSystem.Emit(1);
    }
}
[System.Serializable]
public enum particleSystemType { LeftWall,MidWall }