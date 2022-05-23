using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParticle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float duration;

    private void OnEnable()
    {
        StartParticleSystem();
    }

    public void StartParticleSystem()
    {
        StartCoroutine(ParticleDuration());
        particleSystem.Play();
    }

    IEnumerator ParticleDuration()
    {
        float time = 0f;
        while(time < duration)
        {

            time += Time.deltaTime;


            yield return null;

        }

        particleSystem.Pause();
        gameObject.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();


    }

}
