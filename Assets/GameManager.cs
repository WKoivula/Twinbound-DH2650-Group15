using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform playerA;
    [SerializeField]
    private Transform playerB;

    private bool hasSwapped = false;
    private float swapInterval = 5f;
    private float timer = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TriggerSwap()
    {
        SwapPositions();
        hasSwapped = true;
    }

    private void Update()
    {
        if (hasSwapped)
        {
            timer += Time.deltaTime;

            if (timer >= swapInterval)
            {
                SwapPositions();
                timer = 0f;
            }
        }
    }

    private void SwapPositions()
    {
        Vector3 tempPos = playerA.position;
        playerA.position = playerB.position;
        playerB.position = tempPos;
    }
}
