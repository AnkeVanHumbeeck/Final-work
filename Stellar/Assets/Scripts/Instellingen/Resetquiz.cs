using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetQuiz : MonoBehaviour
{
    void Start()
    {
        if (QuizStateManager.Instance != null)
        {
            QuizStateManager.Instance.ResetQuiz();
        }
    }

}
