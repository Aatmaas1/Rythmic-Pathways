using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // M�thode pour d�marrer une nouvelle partie
    public void StartGame()
    {
        // Charger la sc�ne du jeu
        SceneManager.LoadScene("Ricky2");
    }

    // M�thode pour quitter le jeu
    public void QuitGame()
    {
        // Quitter l'application (fonctionne uniquement dans un build)
        Application.Quit();
    }
}
