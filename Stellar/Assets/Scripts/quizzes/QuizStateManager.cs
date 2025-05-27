using UnityEngine;

public class QuizStateManager : MonoBehaviour
{
    public static QuizStateManager Instance;

    public bool[] heeftGeantwoord;
    public int[] gekozenAntwoorden;

    public int aantalJuisteAntwoorden = 0;

    public void ResetQuiz()
    {
        for (int i = 0; i < heeftGeantwoord.Length; i++)
        {
            heeftGeantwoord[i] = false;
            gekozenAntwoorden[i] = -1;
        }
        aantalJuisteAntwoorden = 0; 
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        int aantalVragen = 10;
        heeftGeantwoord = new bool[aantalVragen];
        gekozenAntwoorden = new int[aantalVragen];
    }

    

}
