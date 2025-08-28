using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
   public void LoadGameScene()
   {
        if (PlayerData.Instance.GetHearts() > 0)
            SceneManager.LoadScene(PlayerData.Instance.GetCurrentLevel() + 1); // load currrent level with build index offset
   }
}
