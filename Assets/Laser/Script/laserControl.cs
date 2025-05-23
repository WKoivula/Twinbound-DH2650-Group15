using UnityEngine;

public class LaserHitScan : BaseLaserController
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (emitter == null) return;
        
        if (!lr.enabled) lr.enabled = true; 
        FireLaser(emitter.position, Vector3.down);

        Vector3 origin = emitter.position;
        Vector3 direction = Vector3.down;

        FireLaser(origin, direction);
    }
}
