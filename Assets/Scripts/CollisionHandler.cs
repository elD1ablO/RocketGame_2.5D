using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1.5f;
    
    void OnCollisionEnter(Collision collision)
    {        
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("standing on a pad");
                break;
            case "Finish":
                print("You win");
                StartSuccess();
                break;
            default:
                print("Looser");
                StartCrash();
                break;
        }
    }

    void StartCrash()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void StartSuccess()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
