using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        // Lock levels based on player progress
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 2);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelsUnlocked)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void OpenLevelSelection()
    {
        startPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }

    public void BackToStart()
    {
        levelSelectionPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void SelectLevel(int levelIndex)
    {
        // Load the selected level
        SceneManager.LoadScene("Level_" + levelIndex);
    }
}
