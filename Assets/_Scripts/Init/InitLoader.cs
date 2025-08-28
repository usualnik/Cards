using UnityEngine;
using UnityEngine.SceneManagement;

public class InitLoader : MonoBehaviour
{
   private const int MAIN_MENU_BUILD_INDEX = 1;

    void Start()
    {
        PlayerData.Instance.OnDataLoaded += PlayerData_OnDataLoaded;
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnDataLoaded -= PlayerData_OnDataLoaded;

    }

    private void PlayerData_OnDataLoaded()
    {
        SceneManager.LoadScene(MAIN_MENU_BUILD_INDEX);
    }
}
