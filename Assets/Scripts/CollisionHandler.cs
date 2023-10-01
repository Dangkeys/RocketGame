using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float crashVolume = 0.5f;
    [SerializeField] [Range(0f, 1f)] float successVolume = 0.5f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] float levelLoadDelay = 1f;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();
        else if(Input.GetKeyDown(KeyCode.C))
            collisionDisabled = !collisionDisabled; // toggle collisionDisabled
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is Friendly");
                break;
            case "Finish":
                StartSuccesSeaquence();
                break;
            default:
                StartCrashSeaquence();
                break;
        }

    }

    private void StartSuccesSeaquence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success, successVolume);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSeaquence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash, crashVolume);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", levelLoadDelay);
    }
    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
