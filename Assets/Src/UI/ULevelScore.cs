using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ULevelScore : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _levelText;
    private const string SCORE_TEXT_TEMPLATE = "{0} pts";

    void Start()
    {
        _scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        ArkanoidEvent.OnScoreUpdatedEvent += OnScoreUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnScoreUpdatedEvent -= OnScoreUpdated;
    }

    private void OnScoreUpdated(int score, int totalScore)
    {
        _scoreText.text = string.Format(SCORE_TEXT_TEMPLATE, totalScore);
    }
}
