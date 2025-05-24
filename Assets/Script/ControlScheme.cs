using UnityEngine;
using UnityEngine.UI;

public class ControlScheme : MonoBehaviour
{
    [SerializeField] GameObject twinA;
    [SerializeField]  GameObject twinB;
    [SerializeField] private float fadeConstant = 2.0f;

    SpriteRenderer controlsTwinA;
    SpriteRenderer controlsTwinB;

    private float timeToStartFade = 8f;
    private float fadeSpeed = 2f;
    public void Start()
    {
        controlsTwinA = twinA.GetComponent<SpriteRenderer>();
        controlsTwinB = twinB.GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (timeToStartFade > 0)
        {
            timeToStartFade -= Time.deltaTime;
            return;
        }

        if (controlsTwinA.color.a > 0)
        {
            controlsTwinA.color = new Color(controlsTwinA.color.r, controlsTwinA.color.g, controlsTwinA.color.b, controlsTwinA.color.a - (fadeSpeed * Time.deltaTime));
            controlsTwinB.color = new Color(controlsTwinB.color.r, controlsTwinB.color.g, controlsTwinB.color.b, controlsTwinB.color.a - (fadeSpeed * Time.deltaTime));
        }
        
        else if (controlsTwinA.color.a <= 0)
        {
            twinA.SetActive(false);
            twinB.SetActive(false);
        }
    } 
}
