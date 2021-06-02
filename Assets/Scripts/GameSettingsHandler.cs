using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsHandler : MonoBehaviour
{
// #region Singleton Instance Logic
//     private static bool _isInstanceActive = true;
//     private static object _instanceLock = new object();
    
//     private static GameSettingsHandler _settingsInstance;

//     public static GameSettingsHandler _inst
//     {
//         get
//         {
//             if (!_isInstanceActive)
//             {
//                 #if UNITY_EDITOR
//                 Debug.Log("[Singleton] Instance + '" + typeof(GameSettingsHandler) + "' is no longer active. Returning NULL");
//                 #endif

//                 return null;
//             }

//             lock (_instanceLock)
//             {
//                 if (_settingsInstance == null)
//                 {
//                     _settingsInstance = FindObjectOfType<GameSettingsHandler>();

//                     if (_settingsInstance == null)
//                     {
//                         var newInst = new GameObject();
//                         _settingsInstance = newInst.AddComponent<GameSettingsHandler>();
//                         newInst.name = "~" + typeof(GameSettingsHandler);

//                         _isInstanceActive = true;

//                         DontDestroyOnLoad(newInst);
//                     }
//                 }

//                 return _settingsInstance;
//             }
//         }
//     }

//     private void OnApplicationQuit()
//     {
//         _isInstanceActive = false;
//     }

//     private void OnDestroy()
//     {
//         _isInstanceActive = false;
//     }
// #endregion

    public static GameSettingsHandler _inst;

    [Space]
    [Header("Game Settings")]
    public bool showFPS = true;

    private void Awake()
    {
        // Confirm singleton instance is active
        if (_inst == null)
        {
            _inst = this;
            DontDestroyOnLoad(this);
        }
        else if (_inst != this)
            GameObject.Destroy(this.gameObject);
    }
}
