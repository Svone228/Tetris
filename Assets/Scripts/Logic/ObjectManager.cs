using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public ScoreAnimation Score;
    public MainScript Main;
    public NextFigureScript NextFigureScript;
    static ScoreAnimation score;
    static MainScript main;
    static NextFigureScript nextFigureScript;
    private void Awake()
    {
        main = Main;
        score = Score;
        nextFigureScript = NextFigureScript;
    }
    // Start is called before the first frame update
    public static MainScript GetMainScript() 
    {
        return main;
    }
    public static ScoreAnimation GetScoreAnimation()
    {
        return score;
    }
    public static NextFigureScript GetNextFigureScript() 
    {
        return nextFigureScript;
    }
}
