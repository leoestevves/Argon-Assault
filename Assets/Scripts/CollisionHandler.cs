using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;


    void OnTriggerEnter(Collider other)
    {        
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashVFX.Play(); //Liga a explosão
        GetComponent<MeshRenderer>().enabled = false;   // Desabilita a imagem da nave
        GetComponent<BoxCollider>().enabled = false;    // Desabilita o colisor da nave
        GetComponent<PlayerControls>().enabled = false; // Desabilita o controle da nave
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
