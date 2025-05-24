using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private float fadeConstant = 2.0f;
    [SerializeField] private Rigidbody player1;
    [SerializeField] private Rigidbody player2;
    [SerializeField] private float floatSpeed = 0.1f;
    [SerializeField] private CameraFollowTwoPlayers cameraScript;
    [SerializeField] private string startScreenName = "StartScreen";

    private int playersInTrigger = 0;

    private void Awake()
    {
        if (blackScreen == null)
        {
            Canvas blackScreenCanvas = FindFirstObjectByType<Canvas>();
            if (blackScreenCanvas != null)
            {
                blackScreen = blackScreenCanvas.GetComponentInChildren<Image>();
                levelText = blackScreenCanvas.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }

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
            cameraScript.shouldFollow = false;
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
            player1.AddForce(new Vector3(0, 1, 0) * 10000f * floatSpeed * Time.deltaTime);
            player2.AddForce(new Vector3(0, 1, 0) * 10000f * floatSpeed * Time.deltaTime);
            alpha += Time.deltaTime * fadeConstant;
            Color bColor = blackScreen.color;
            Color color = new Color(bColor.r, bColor.g, bColor.b, alpha);
            blackScreen.color = color;
            yield return null;
        }
        alpha = levelText.color.a;
        while (levelText.color.a < 1)
        {
            alpha += Time.deltaTime * fadeConstant;
            Color bColor = levelText.color;
            Color color = new Color(bColor.r, bColor.g, bColor.b, alpha);
            levelText.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while (levelText.color.a > 0)
        {
            alpha -= Time.deltaTime * fadeConstant;
            Color bColor = levelText.color;
            Color color = new Color(bColor.r, bColor.g, bColor.b, alpha);
            levelText.color = color;
            yield return null;
        }
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex == 5)
        {
            SceneManager.LoadScene(startScreenName);
        }
        else
        {
            SceneManager.LoadScene(currentIndex + scene);
        }
        yield return null;
    }
}
