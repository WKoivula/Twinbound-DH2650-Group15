using UnityEngine;

public class NozzleLaserController : MonoBehaviour
{
    public Transform nozzleOrigin;
    public float maxDistance = 10f;
    public LayerMask hitMask;

    public GameObject startVFX; 
    public GameObject endVFX;   

    private bool isFiring = false;
    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;

        if (startVFX != null) startVFX.SetActive(false);
        if (endVFX != null) endVFX.SetActive(false);
    }

    void Update()
    {
        if (!isFiring) return;

        Vector3 origin = nozzleOrigin.position;
        Vector3 direction = Vector3.down;

        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(origin, direction, out hit, maxDistance, hitMask))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = origin + direction * maxDistance;
        }

        lr.SetPosition(0, origin);
        lr.SetPosition(1, endPoint);

        if (startVFX != null)
        {
            startVFX.SetActive(true);
            startVFX.transform.position = origin;
        }

        if (endVFX != null)
        {
            endVFX.SetActive(true);
            endVFX.transform.position = endPoint;
        }
    }

    public void StartFiring()
    {
        isFiring = true;
        lr.enabled = true;

        if (startVFX != null) startVFX.SetActive(true);
        if (endVFX != null) endVFX.SetActive(true);
    }

    public void StopFiring()
    {
        isFiring = false;
        lr.enabled = false;

        if (startVFX != null) startVFX.SetActive(false);
        if (endVFX != null) endVFX.SetActive(false);
    }
}
