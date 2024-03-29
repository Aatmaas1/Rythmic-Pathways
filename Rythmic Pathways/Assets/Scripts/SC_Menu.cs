using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Méthode pour démarrer une nouvelle partie
    public void StartGame()
    {
        // Charger la scène du jeu
        SceneManager.LoadScene("Ricky2");
    }

    // Méthode pour quitter le jeu
    public void QuitGame()
    {
        // Quitter l'application (fonctionne uniquement dans un build)
        Application.Quit();
    }
}
