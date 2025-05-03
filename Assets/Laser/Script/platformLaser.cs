using UnityEngine;

public class PlatformCharge : MonoBehaviour
{
    public NozzleLaserController nozzleLaser;
    public Transform nozzleTransform; 
    private bool isActive = false;

    public void SetLaserState(bool on, float laserEmitterX)
    {
        if (nozzleLaser == null || nozzleTransform == null) return;

        if (on)
        {
            if (!isActive)
            {
                nozzleLaser.StartFiring();
                isActive = true;
            }

           
            Vector3 pos = nozzleTransform.position;
            pos.x = laserEmitterX;
            nozzleTransform.position = pos;
        }
        else
        {
            if (isActive)
            {
                nozzleLaser.StopFiring();
                isActive = false;
            }
        }
    }
}
