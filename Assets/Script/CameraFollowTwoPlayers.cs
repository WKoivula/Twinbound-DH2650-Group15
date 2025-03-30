using UnityEngine;

public class CameraFollowTwoPlayers : MonoBehaviour
{
    public Transform playerA;
    public Transform playerB;
    public Vector3 offset = new Vector3(0, 0, -10f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (playerA == null || playerB == null) return;

        Vector3 midpoint = (playerA.position + playerB.position) / 2f;
        Vector3 desiredPosition = midpoint + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
