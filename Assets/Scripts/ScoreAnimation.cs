using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScoreAnimation : MonoBehaviour
{
    public Text Score;
    int score = default;
    int scoreFormatNumbersCount = 13;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int tetrisCount) 
    {
        switch (tetrisCount)
        {
            case 0:
                break;
            case 1:
                score += 100;
                break;
            case 2:
                score += 300;
                break;
            case 3:
                score += 700;
                break;
            case 4:
                score += 1500;
                break;
            default:
                Debug.Log(tetrisCount);
                break;
        }
        
        StartScoreAnimation(score);
    }

    public string ScoreStandartFormat(int score)
    {
        return SetNumberFormat(score) + "\r\n";
    }

    public string SetNumberFormat(int score) 
    {
        string stringScore = score.ToString();
        for (int i = stringScore.Length; i <= scoreFormatNumbersCount; i++)
        {
            stringScore = "0" + stringScore;
        }
        return stringScore;
    }
    void StartScoreAnimation(int score, float timeDuration = 0.8f)
    {
        var TopPosition = new Vector2(0, -60);
        var BottomPosition = new Vector2(0, 60);
        Score.rectTransform.DOAnchorPos(TopPosition, 1);
        Tween GoTopPosition(float time = 0)
        {
            return Score.rectTransform.DOAnchorPos(TopPosition, time);
        }
        Tween GoBottomPosition(float time = 0) 
        {
            return Score.rectTransform.DOAnchorPos(BottomPosition, time);
        }
        var animationText = Score.text + SetNumberFormat(score);

        GoTopPosition();
        Score.text = animationText;
        Tween animation = GoBottomPosition(timeDuration);
        animation.OnComplete(() => 
        { 
            Score.text = ScoreStandartFormat(score);
            GoTopPosition(0);
        });

    }
}
