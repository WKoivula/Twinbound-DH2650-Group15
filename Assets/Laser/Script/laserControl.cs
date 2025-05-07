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
        
        if (!lr.enabled) lr.enabled = true; // ✅ 手动打开激光
        FireLaser(emitter.position, Vector3.down);

        Vector3 origin = emitter.position;
        Vector3 direction = Vector3.down;

        FireLaser(origin, direction); // 👈 使用基类激光发射逻辑
    }
}
