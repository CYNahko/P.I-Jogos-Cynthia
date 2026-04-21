using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{

    public void IniciaJogo()
    {
        // MUITO IMPORTANTE: Destrava o tempo antes de carregar a cena
        Time.timeScale = 1f; 
        SceneManager.LoadScene(1);
    }
    
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
