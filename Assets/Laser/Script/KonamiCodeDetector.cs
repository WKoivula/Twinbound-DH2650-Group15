using UnityEngine;

public class KonamiCodeLaserKiller : MonoBehaviour
{
    private KeyCode[] konamiCode = new KeyCode[]
    {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.A, KeyCode.B,
        KeyCode.A, KeyCode.B
    };

    private int currentIndex = 0;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(konamiCode[currentIndex]))
            {
                currentIndex++;
                if (currentIndex >= konamiCode.Length)
                {
                    Debug.Log("Konami Code Activated! Disabling all lasers...");
                    DisableAllLasers();
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex = 0;
            }
        }
    }

    void DisableAllLasers()
    {
        BaseLaserController[] allLasers = FindObjectsOfType<BaseLaserController>();
        foreach (var laser in allLasers)
        {
            laser.gameObject.SetActive(false);
        }
        Debug.Log("All lasers have been disabled.");
    }
}
