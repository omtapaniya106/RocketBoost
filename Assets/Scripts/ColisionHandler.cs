using System;
using System.Runtime.Serialization;
using Unity.Loading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LoadingTime = 2f;
    [SerializeField] AudioClip OnSuccess;
    [SerializeField] AudioClip OnCrash;
    [SerializeField] ParticleSystem SuccessPartical;
    [SerializeField] ParticleSystem CrashPartical;

    AudioSource audioSource;

    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!isControllable)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly" :
                Debug.Log("Everything Looking Good..!");
                break;    
            case "Finish" :
                Endlevel();
                break;
            default :
                    StartCrashSequence(); 
                break;
        }
    }

    void Endlevel()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(OnSuccess);
        SuccessPartical.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel",LoadingTime);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(OnCrash);
        CrashPartical.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel" , LoadingTime);
    }

    void NextLevel()
        {
            int currentScene =  SceneManager.GetActiveScene().buildIndex;
            int NextLevel = currentScene + 1;
            if(NextLevel == SceneManager.sceneCountInBuildSettings)
            {
                NextLevel = 0;
            }
                SceneManager.LoadScene(NextLevel);
        }

    void ReloadLevel()
        {
            int currentScene =  SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
}