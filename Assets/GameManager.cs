using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform playerA;
    [SerializeField]
    private Transform playerB;

    private bool hasSwapped = false;
    private float swapDuration = 10f;
    private float timer = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TriggerSwap()
    {
        SwapPositions();
        timer = swapDuration;
        hasSwapped = true;
    }

    private void Update()
    {
        if (hasSwapped)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                SwapBack();
                hasSwapped = false;
            }
        }
    }

    private void SwapPositions()
    {
        Vector3 tempPos = playerA.position;
        playerA.position = playerB.position;
        playerB.position = tempPos;
    }
    private void SwapBack()
    {
        Vector3 tempPosition = playerB.position;
        playerB.position = playerA.position;
        playerA.position = tempPosition;
    }
}
