using UnityEngine;
using UnityEditor;  
public class RenameChildren
{
    [MenuItem("Tools/Rename Right Platforms")]
    static void RenameRightSideChildren()
    {
        GameObject parent = GameObject.Find("Platform_Left");

        if (parent == null)
        {
            UnityEngine.Debug.LogWarning("Platform_Right not found in the scene.");
            return;
        }

        int index = 1;
        foreach (Transform child in parent.transform)
        {
            child.name = "left_" + index;
            index++;
        }

        UnityEngine.Debug.Log("Children of Platform_Right have been renamed.");
    }
}
