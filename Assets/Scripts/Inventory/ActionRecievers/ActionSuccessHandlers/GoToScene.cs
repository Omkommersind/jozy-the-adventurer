using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour, IActionSuccessHandler
{
    public string DestinationScene;

    public void OnSuccess()
    {
        SceneManager.LoadScene(DestinationScene);
    }
}
