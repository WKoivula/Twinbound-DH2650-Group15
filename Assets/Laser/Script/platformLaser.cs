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

            // ✅ 始终对齐喷嘴位置（即使已经是激活状态）
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
