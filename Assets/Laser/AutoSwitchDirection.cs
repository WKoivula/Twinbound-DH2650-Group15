using UnityEngine;

public class AutoSwitchDirectionV2 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float firstMoveTime = 1f;
    public float normalMoveTime = 2f;

    private float timer = 0f;
    private int direction = -1;
    private bool firstMoveDone = false;

    void Update()
    {
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
        timer += Time.deltaTime;

        if (!firstMoveDone && timer >= firstMoveTime)
        {
            direction *= -1;
            firstMoveDone = true;
            timer = 0f;
        }
        else if (firstMoveDone && timer >= normalMoveTime)
        {
            direction *= -1;
            timer = 0f;
        }
    }
}
