using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;


/// <summary>
/// Loads the Initialization Scene so that needed system dependencies can be accessed from any other scene in the editor
/// </summary>
public class InitManager : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Scene Data")]
	public List<SceneData> scenesToLoad = new List<SceneData>();


    void Awake()
    {
		LoadSceneListAdditive();
    }

	private void LoadSceneListAdditive()
	{
		foreach (var scene in scenesToLoad)
		{
			if (SceneManager.GetActiveScene().name == scene.sceneName)
				return;
			if (SceneManager.GetSceneByName(scene.sceneName).isLoaded)
				continue;
			
			SceneManager.LoadSceneAsync(scene.sceneName, LoadSceneMode.Additive);
		}
	}
#endif
}
