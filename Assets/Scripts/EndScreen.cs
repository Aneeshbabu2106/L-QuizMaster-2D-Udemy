using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    Scorekeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<Scorekeeper>();
    }
    public void FinalScore()
    {
        scoreText.text = "Congratulations!\n You Score " + scoreKeeper.CalculatePercentage() + "%";
    }
}
