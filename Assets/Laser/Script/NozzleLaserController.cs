using UnityEngine;

/// <summary>

/// </summary>
public class NozzleLaserController : BaseLaserController
{
    // 当前是否正在发射激光
    private bool isFiring = false;

    // 初始化组件（调用基类初始化）
    protected override void Start()
    {
        base.Start();
    }

    // 每帧更新，只有当激光开启时才进行射线检测与渲染
    void Update()
    {
        if (!isFiring) return;        // 未开启激光则跳过

        if (emitter == null) return;  // 没有发射器也无法发射

        // 发射激光的起点与方向（默认向下）
        Vector3 origin = emitter.position;
        Vector3 direction = Vector3.down;

        // 调用基类方法执行射线检测 + 渲染 + 平台响应
        FireLaser(origin, direction);
    }

    /// <summary>
    /// 启动激光，打开 LineRenderer 和特效
    /// </summary>
    public void StartFiring()
    {
        isFiring = true;
        // 启用 LineRenderer 显示激光
        if (lr != null) lr.enabled = true;

        // 激活起点/终点特效
        if (startVFX != null) startVFX.SetActive(true);
        if (endVFX != null) endVFX.SetActive(true);
    }

    /// <summary>
    /// 停止激光，关闭渲染与命中平台效果
    /// </summary>
    public void StopFiring()
    {
        isFiring = false;

        // 调用基类关闭激光、特效、平台状态
        StopLaser();
    }
}
