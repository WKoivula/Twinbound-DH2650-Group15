using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private float fadeConstant = 2.0f;
    [SerializeField] private Rigidbody player1;
    [SerializeField] private Rigidbody player2;

    private int playersInTrigger = 0;
    private void Start()
    {
        StartCoroutine(DoFadeIn());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            playersInTrigger++;
        }
        if (playersInTrigger >= 2)
        {
            StartCoroutine(DoFadeOut(1));
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            playersInTrigger--;
        }
    }

    IEnumerator DoFadeIn()
    {
        Debug.Log("Starting");
        float alpha = blackScreen.color.a;
        while (blackScreen.color.a > 0)
        {
            alpha -= Time.deltaTime * fadeConstant;
            Color bColor = blackScreen.color;
            Color color = new Color(bColor.r, bColor.g, bColor.b, alpha);
            blackScreen.color = color;
            yield return null;
        }
        yield return null;
    }

    public IEnumerator DoFadeOut(int scene)
    {
        float alpha = blackScreen.color.a;
        while (blackScreen.color.a < 1)
        {
            alpha += Time.deltaTime * fadeConstant;
            Color bColor = blackScreen.color;
            Color color = new Color(bColor.r, bColor.g, bColor.b, alpha);
            blackScreen.color = color;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scene);
        yield return null;
    }
}
