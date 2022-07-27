using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Game");
        }

        public void Settings()
        {
            
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
    
}
