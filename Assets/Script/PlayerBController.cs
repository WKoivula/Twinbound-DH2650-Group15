using UnityEngine;

public class PlayerBController : Player
{
    private void Update()
    {
        Movement(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.W);
        base.Update();
    }
}
