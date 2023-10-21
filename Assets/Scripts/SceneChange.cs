using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	public void Exit()
	{
		Application.Quit();
	}

	public void Reset()
	{
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);		
	}
}
