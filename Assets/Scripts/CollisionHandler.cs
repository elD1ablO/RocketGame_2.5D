using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1.5f;

    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;

    AudioSource audioSource;

    bool isTransitioning = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {        
        if (isTransitioning) return;

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
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);              
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("ReloadLevel", delay);
        isTransitioning = true;
    }

    void StartSuccess()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);              
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("LoadNextLevel", delay);
        isTransitioning = true;
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
