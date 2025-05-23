using UnityEngine;

public class GasRising : MonoBehaviour
{
    public float speed = 0.5f;
    public float maxScaleY = 50f;

    private float startY;
    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
        startY = transform.localPosition.y;
    }

    void Update()
    {
        if (transform.localScale.y < maxScaleY)
        {
            transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
            float offset = (transform.localScale.y - startScale.y) * 0.5f;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                startY + offset,
                transform.localPosition.z
            );
        }
    }
}
