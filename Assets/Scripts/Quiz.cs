using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI quesText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    bool isComplete = false;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnsweredEarly = false;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timers")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    Scorekeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<Scorekeeper>();
        currentQuestion = questions[0];
        progressBar.value = 0;
        progressBar.maxValue = questions.Count;
        //DisplayQuestion();
    }
    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQue)
        {
            
            DisplayQuestion();
            hasAnsweredEarly = false;
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

        if (questions.Count > 0)
        {
            
            GetRandomQuestions();
            quesText.text = currentQuestion.GetQuestions();
            for (int i = 0; i < answerButton.Length; i++)
            {
                TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = currentQuestion.GetAnswers(i);
            }
            
            SetButtonState(true);
            SetDefaultImage();
            progressBar.value++;
            scoreKeeper.SetQuesSeen();
        }
    }

    private void GetRandomQuestions()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
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
        scoreText.text = "Score: " + scoreKeeper.CalculatePercentage() + "%";
        if(progressBar.value == progressBar.maxValue )
        {
            isComplete = true;
        }
    }

    void DisplayAnswers(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswer())
        {
            quesText.text = "Correct!";
            buttonImage = answerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.SetCorrectAns();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswer();
            string correctAnswer = currentQuestion.GetAnswers(correctAnswerIndex);
            quesText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }
}
