using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper : MonoBehaviour
{
    private int correctAns = 0;
    private int quesSeen = 0;

    public int GetCorrectAns()
    {
        return correctAns;
    }
    public void SetCorrectAns()
    {
        correctAns++;
    }
    public int GetQuesSeen()
    {
        return quesSeen;
    }
    public void SetQuesSeen()
    {
        quesSeen++;
    }
    public int CalculatePercentage()
    {
        return Mathf.RoundToInt((correctAns / (float)quesSeen) * 100);
    }
}
