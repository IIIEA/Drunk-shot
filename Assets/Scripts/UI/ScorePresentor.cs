using TMPro;
using UnityEngine;

public class ScorePresentor : MonoBehaviour
{
    [SerializeField] private LootCollector _lootCollector;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _stars;

    private void OnEnable()
    {
        _lootCollector.StarsCountChanged += OnStarCountChanged;
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _lootCollector.StarsCountChanged -= OnStarCountChanged;
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score, DunkType type, int points)
    {
        _score.text = score.ToString();
    }

    private void OnStarCountChanged(int count)
    {
        _stars.text = count.ToString();
    }
}
