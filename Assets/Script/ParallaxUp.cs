using UnityEngine;

public class LoopingBackgroundUp : MonoBehaviour
{
    public float parallaxSpeed = 2f;
    private float spriteHe�ght;
    private Transform[] parts;

    private void Start()
    {
        parts = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            parts[i] = transform.GetChild(i);
        }

        SpriteRenderer sr = parts[0].GetComponent<SpriteRenderer>();
        spriteHe�ght = sr.bounds.size.y;
    }

    private void Update()
    {
        transform.position -= Vector3.up * parallaxSpeed * Time.deltaTime;

        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].position.y < -spriteHe�ght)
            {
                // Move to the right of the farthest sibling
                float highestY = GetHighestY();
                parts[i].position = new Vector3(parts[i].position.x, highestY + spriteHe�ght, parts[i].position.z);
            }
        }
    }

    private float GetHighestY()
    {
        float maxY = parts[0].position.y;
        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].position.y > maxY)
                maxY = parts[i].position.y;
        }
        return maxY;
    }
}
