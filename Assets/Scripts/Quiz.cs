using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI quesText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timers")]
    [SerializeField] Image timerImage;
    Timer timer;
    

    void Start()
    {
        DisplayQuestion();
        timer = FindObjectOfType<Timer>();
    }
    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQue)
        {
            hasAnsweredEarly = false;
            DisplayQuestion();
            timer.loadNextQue = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnswering)
        {
            DisplayAnswers(-1);
            SetButtonState(false);

        }
    }
    void DisplayQuestion()
    {
        quesText.text = question.GetQuestions();
        for (int i = 0; i < answerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswers(i);
        }
        SetButtonState(true);
        SetDefaultImage();
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            answerButton[i].GetComponent<Button>().interactable = state;
        }   
    }
    void SetDefaultImage()
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            answerButton[i].GetComponent<Image>().sprite = defaultSprite;
        }
    }
    public void AnswerEntered(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswers(index);
        SetButtonState(false);
        timer.CancelTimer();
    }
    void DisplayAnswers(int index)
    {
        Image buttonImage;
        if (index == question.GetCorrectAnswer())
        {
            quesText.text = "Correct!";
            buttonImage = answerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIndex = question.GetCorrectAnswer();
            string correctAnswer = question.GetAnswers(correctAnswerIndex);
            quesText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
        }
    }
}
