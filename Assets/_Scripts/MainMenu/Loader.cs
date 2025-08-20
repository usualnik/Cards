using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
   public void LoadGameScene()
   {
        var nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextSceneBuildIndex);
   }
}
