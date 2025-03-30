using UnityEngine;

public class PlayerBController : Player
{
    void Update()
    {
        Movement(Input.GetKey(KeyCode.LeftArrow), Input.GetKey(KeyCode.RightArrow));
    }
}
