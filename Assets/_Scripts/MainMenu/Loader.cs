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
        {19 , "Assets/Scenes/Game/Level 19.unity"},
        {20 , "Assets/Scenes/Game/Level 20.unity"},
        {21 , "Assets/Scenes/Game/Level 21.unity"},
        {22 , "Assets/Scenes/Game/Level 22.unity"},
        {23 , "Assets/Scenes/Game/Level 23.unity"},
        {24 , "Assets/Scenes/Game/Level 24.unity"},
        {25 , "Assets/Scenes/Game/Level 25.unity"},
        {26 , "Assets/Scenes/Game/Level 26.unity"},
        {27 , "Assets/Scenes/Game/Level 27.unity"},
        {28 , "Assets/Scenes/Game/Level 28.unity"},
        {29 , "Assets/Scenes/Game/Level 29.unity"},
        {30 , "Assets/Scenes/Game/Level 30.unity"},
        {31 , "Assets/Scenes/Game/Level 31.unity"},
        {32 , "Assets/Scenes/Game/Level 32.unity"},
        {33 , "Assets/Scenes/Game/Level 33.unity"},
        {34 , "Assets/Scenes/Game/Level 34.unity"},
        {35 , "Assets/Scenes/Game/Level 35.unity"},
        {36 , "Assets/Scenes/Game/Level 36.unity"},
        {37 , "Assets/Scenes/Game/Level 37.unity"}      

    };

    private const int LOADING_SCREEN_WINDOW_INDEX = 5;
   public void LoadGameScene()
   {
        var nextSceneIndex = PlayerData.Instance.GetCurrentLevel();

        if (PlayerData.Instance.GetHearts() > 0 
            && _adressableLevelsPaths.TryGetValue(nextSceneIndex, out string path))
        {
            WindowManager.Instance.OpenWindow(LOADING_SCREEN_WINDOW_INDEX);
            LoadLevelWithAdressables(path);
        }           
   }

    private void LoadLevelWithAdressables(string path)
    {
        Addressables.LoadSceneAsync(path,
          LoadSceneMode.Single);
    }
}
