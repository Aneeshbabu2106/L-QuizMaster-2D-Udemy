using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQue =30f;
    [SerializeField] float timeToShowAns = 10f;
    public bool isAnswering =true;
    float timeValue;
    public bool loadNextQue;
    public float fillFraction;

    void Update()
    {
        UpdateTime();
    }
    public void CancelTimer()
    {
        timeValue = 0;
    }
    void UpdateTime()
    {
        timeValue -= Time.deltaTime;

        if(isAnswering)
        {
            if(timeValue > 0)
            {
                fillFraction = timeValue / timeToCompleteQue;
            }
            else
            {
                isAnswering = false;
                timeValue = timeToShowAns;
            }
        }
        else
        {
            if (timeValue > 0)
            {
                fillFraction = timeValue / timeToShowAns;
            }
            else
            {
                isAnswering = true;
                timeValue = timeToCompleteQue;
                loadNextQue = true;
            }

        }
    }
}
