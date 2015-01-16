using UnityEngine;
using System.Collections;

public class WeightDestroyable : Destroyable
{
    public int weight = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    protected override void onDestroy()
    {
        //base.onDestroy();
        //Debug.Log(tag + " will be destroyed");
        Camera.main.GetComponent<CameraShake>().shakeFor(1.0f);
        GameObject emitter = Instantiate(Resources.Load("Prefabs/Wall_destruction")) as GameObject;
        emitter.transform.position = transform.position;
        ParticleSystem emitterSystem = emitter.GetComponent<ParticleSystem>();
        Destroy(emitter, emitterSystem.duration + emitterSystem.startLifetime);
    }
}
