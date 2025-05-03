using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float parallaxSpeed = 2f;
    private float spriteWidth;
    private Transform[] parts;

    private void Start()
    {
        parts = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            parts[i] = transform.GetChild(i);
        }

        SpriteRenderer sr = parts[0].GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }

    private void Update()
    {
        transform.position += Vector3.left * parallaxSpeed * Time.deltaTime;

        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].position.x < -spriteWidth)
            {
                // Move to the right of the farthest sibling
                float rightMostX = GetRightmostX();
                parts[i].position = new Vector3(rightMostX + spriteWidth, parts[i].position.y, parts[i].position.z);
            }
        }
    }

    private float GetRightmostX()
    {
        float maxX = parts[0].position.x;
        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].position.x > maxX)
                maxX = parts[i].position.x;
        }
        return maxX;
    }
}
