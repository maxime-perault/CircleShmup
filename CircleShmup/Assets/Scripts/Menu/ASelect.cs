using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class ASelect : MonoBehaviour
{
    protected GameObject music;

    protected void Start()
    {
        music = GameObject.Find("MusicPlayer");
    }

    protected IEnumerator LoadYourAsyncScene(string name)
    {
        string path = "Scenes/"; path += name;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(path);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public virtual void Select(int tmp_button) { }
}
