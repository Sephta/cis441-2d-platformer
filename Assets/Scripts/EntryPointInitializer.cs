using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPointInitializer : MonoBehaviour
{
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
}
