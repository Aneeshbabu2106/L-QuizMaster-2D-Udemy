using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen  endscreen;

    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endscreen = FindObjectOfType<EndScreen>();
        quiz.gameObject.SetActive(true);
        endscreen.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endscreen.gameObject.SetActive(true);
            endscreen.FinalScore();
        }  
    }
    public void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
