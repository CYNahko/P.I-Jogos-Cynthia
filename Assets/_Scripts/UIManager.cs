using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    private GameManager gameManager;
    private bool jaParou = false; // Garante que o pause só aconteça uma vez

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        // Garante que o jogo comece rodando (importante ao reiniciar!)
        Time.timeScale = 1f; 
    }

    void Update()
    {
        // Verificamos se deu Game Over e se ainda não "pausamos" o mundo
        if (!jaParou && gameManager != null && gameManager.IsGameOver())
        {
            AtivarGameOver();
        }
    }

    void AtivarGameOver()
    {
        jaParou = true;
        endGamePanel.SetActive(true);
        
        // CONGELA O JOGO: Para física, Spawners, Movimento e Timers
        Time.timeScale = 0f; 
        
        // DICA: Se quiser esconder o Player ou monstros, você pode fazer:
        GameObject.FindWithTag("Player").SetActive(false);
        GameObject.FindWithTag("Inimigo").SetActive(false);
        GameObject.FindWithTag("Coletavel").SetActive(false);
        GameObject.FindWithTag("Parede").SetActive(false);
        GameObject.FindWithTag("Texto").SetActive(false);
    }
}
