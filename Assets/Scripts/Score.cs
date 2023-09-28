using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    [SerializeField] private Canvas _finalScore;
    [SerializeField] private TMP_Text _finalScoreText;
    PlayerScript _script;
    [SerializeField] private AudioSource source;

    private void Start()
    {
        score = 0;
        _finalScore.enabled = false;

        if (scoreText != null)
        {
            UpdateScoreText();
        }
        _script = FindAnyObjectByType<PlayerScript>();
        source.Pause();
    }

    public void AddScore(int addscore)
    {
        score += addscore;
        if (scoreText != null)
        {
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + score.ToString("000");
    }

    private void Update()
    {
        if (_script==null || _script.isDead == true)
        {
            StartCoroutine(DisplayFinalScore());
        }
    }

    IEnumerator DisplayFinalScore()
    {
        yield return new WaitForSeconds(1f);
        _finalScore.enabled = true;
        _finalScoreText.text = "Final Score\n\n" + score.ToString("000");
        source.Play();
    }
}
