using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quesText;
    [SerializeField] QuestionSO question;
    void Start()
    {
        quesText.text = question.GetQuestions();
        
    }

  
}
