using UnityEngine;
using System.Collections;

public class LifetimeParticle : MonoBehaviour
{
    private ParticleSystem particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Play();
        //StartCoroutine(destroyParticleObject(particles.main.duration));
    }

    IEnumerator destroyParticleObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this);
    }
}
