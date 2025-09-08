using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private Dictionary<int, string> _adressableLevelsPaths = new Dictionary<int, string>()
    {
        {1 , "Assets/Scenes/Game/Level 1.unity"},
        {2 , "Assets/Scenes/Game/Level 2.unity"},
        {3 , "Assets/Scenes/Game/Level 3.unity"},
        {4 , "Assets/Scenes/Game/Level 4.unity"},
        {5 , "Assets/Scenes/Game/Level 5.unity"},
        {6 , "Assets/Scenes/Game/Level 6.unity"},
        {7 , "Assets/Scenes/Game/Level 7.unity"},
        {8 , "Assets/Scenes/Game/Level 8.unity"},
        {9 , "Assets/Scenes/Game/Level 9.unity"},
        {10 , "Assets/Scenes/Game/Level 10.unity"},
        {11 , "Assets/Scenes/Game/Level 11.unity"},
        {12 , "Assets/Scenes/Game/Level 12.unity"},
        {13 , "Assets/Scenes/Game/Level 13.unity"},
        {14 , "Assets/Scenes/Game/Level 14.unity"},
        {15 , "Assets/Scenes/Game/Level 15.unity"},
        {16 , "Assets/Scenes/Game/Level 16.unity"},
        {17 , "Assets/Scenes/Game/Level 17.unity"},
        {18 , "Assets/Scenes/Game/Level 18.unity"},
        {19 , "Assets/Scenes/Game/Level 19.unity"}
    };

    [SerializeField] private GameObject _loadingScreen;

   public void LoadGameScene()
   {
        var nextSceneIndex = PlayerData.Instance.GetCurrentLevel();

        if (PlayerData.Instance.GetHearts() > 0 
            && _adressableLevelsPaths.TryGetValue(nextSceneIndex, out string path))
        {
            _loadingScreen.SetActive(true);
            LoadLevelWithAdressables(path);
        }           
   }

    private void LoadLevelWithAdressables(string path)
    {
        Addressables.LoadSceneAsync(path,
          LoadSceneMode.Single);
    }
}
