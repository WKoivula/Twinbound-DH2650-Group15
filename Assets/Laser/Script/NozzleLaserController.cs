using UnityEngine;

public class NozzleLaserController : BaseLaserController
{
    private bool isFiring = false;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!isFiring) return;
        if (emitter == null) return;

        Vector3 origin = emitter.position;
        Vector3 direction = Vector3.down;

        FireLaser(origin, direction);
    }

    public void StartFiring()
    {
        isFiring = true;
        if (lr != null) lr.enabled = true;
        if (startVFX != null) startVFX.SetActive(true);
        if (endVFX != null) endVFX.SetActive(true);
    }

    public void StopFiring()
    {
        isFiring = false;
        StopLaser();
    }
}
