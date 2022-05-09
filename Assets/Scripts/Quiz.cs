using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quesText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButton;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctAnswerSprite;
    int correctAnswerIndex;

    void Start()
    {
        DisplayQuestion();
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
        Image buttonImage;
        if(index == question.GetCorrectAnswer())
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
        SetButtonState(false);
    }
}
