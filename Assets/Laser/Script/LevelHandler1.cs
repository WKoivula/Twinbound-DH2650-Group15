using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler1 : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private float fadeConstant = 2.0f;

    private bool player1InZone = false;
    private bool player2InZone = false;

    private void Start()
    {
        StartCoroutine(DoFadeIn());
    }

    public void SetPlayerInZone(int playerIndex, bool entered)
    {
        if (playerIndex == 1) player1InZone = entered;
        if (playerIndex == 2) player2InZone = entered;

        if (player1InZone && player2InZone)
        {
            StartCoroutine(DoFadeOut(1));
        }
    }

    IEnumerator DoFadeIn()
    {
        float alpha = blackScreen.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeConstant;
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator DoFadeOut(int sceneOffset)
    {
        float alpha = blackScreen.color.a;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeConstant;
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneOffset);
    }
}