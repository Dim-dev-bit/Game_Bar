using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuNavigation : MonoBehaviour
{
    public void ChangeScenes(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }
}
