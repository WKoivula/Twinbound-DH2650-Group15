using UnityEngine;

public class PlayerAController : Player
{

    void Update()
    {
        Movement(Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));
    }
}
