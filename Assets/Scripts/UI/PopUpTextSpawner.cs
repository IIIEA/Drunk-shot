using System.Collections;
using UnityEngine;

public class PopUpTextSpawner : MonoBehaviour
{
    [SerializeField] private PopUpText _popUpTextPrefab;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private float _spawnDelay;

    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_spawnDelay);    
    }

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score, DunkType type, int points)
    {
        string[] text = new string[3];

        switch (type)
        {
            case DunkType.Perfect:
                text = new string[3] { DunkType.Perfect.ToString(), "+" + points.ToString(), DunkType.Null.ToString() };
                break;
            case DunkType.Bounce:
                text = new string[3] { DunkType.Bounce.ToString(), "+" + points.ToString(), DunkType.Null.ToString() };
                break;
            case DunkType.Both:
                text = new string[3] { DunkType.Perfect.ToString(), DunkType.Bounce.ToString(), "+" + points.ToString() };
                break;
            case DunkType.Null:
                text = new string[3] { "+" + points.ToString(), DunkType.Null.ToString(), DunkType.Null.ToString() };
                break;
        }

        StartCoroutine(SpawnText(text));
    }

    public IEnumerator SpawnText(string[] text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == DunkType.Null.ToString())
                yield break;

            SpawnPopUpText(text[i]);

            yield return _delay;
        }
    }

    private void SpawnPopUpText(string text)
    {
        var popUpText = Instantiate(_popUpTextPrefab, _scoreCounter.transform.position, Quaternion.identity);
        popUpText.Init(text);
    }
}
