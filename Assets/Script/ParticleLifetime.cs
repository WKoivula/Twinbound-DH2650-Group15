using System.Collections;
using UnityEngine;

public class ParticleLifetime : MonoBehaviour
{
    private ParticleSystem particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        StartCoroutine(destroyParticleObject(particles.main.duration));
    }

    IEnumerator destroyParticleObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this);
    }
}
