using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour
{
    public GameObject GameOverPanel; // The Game Over panel to show
    public TMP_Text GameOverText;    // The text showing "Game Over"
    public TMP_Text BestTimeText;    // The text displaying the best time
    public Button ReplayButton;      // The button to replay the game

    void Start()
    {
        // Ensure the panel is hidden initially
        GameOverPanel.SetActive(false);

        // Set up button listener for replay functionality
        ReplayButton.onClick.AddListener(OnReplayButtonClicked);
    }

    public void ShowGameOverPanel(float raceTime)
    {
        // Show the Game Over Panel
        GameOverPanel.SetActive(true);

        // Set "Game Over" text
        GameOverText.text = "Game Over";

        // Display the best time
        string bestTimeDisplay = FormatTime(LapTimeManager.BestLapTime);  // Use the BestLapTime from LapTimeManager
        BestTimeText.text = $"Best Time: {bestTimeDisplay}";
    }

    public void OnReplayButtonClicked()
    {
        // Reload the current scene to replay the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private string FormatTime(float raceTime)
    {
        // Format the time into minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        int milliseconds = Mathf.FloorToInt((raceTime * 100) % 100);

        return $"{minutes:D2}:{seconds:D2}.{milliseconds:D2}";
    }
}
