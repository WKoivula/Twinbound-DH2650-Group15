using UnityEngine;

public class EndTriggerZone : MonoBehaviour
{
    public enum PlayerID
    {
        Player1 = 1,
        Player2 = 2
    }

    [Header("设置此触发器对应的玩家")]
    [SerializeField] private PlayerID assignedPlayer;

    [Header("LevelHandler 控制器引用")]
    [SerializeField] private LevelHandler1 handler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            handler.SetPlayerInZone((int)assignedPlayer, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            handler.SetPlayerInZone((int)assignedPlayer, false);
        }
    }
}
