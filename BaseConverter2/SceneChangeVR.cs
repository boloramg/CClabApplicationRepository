using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeVR : MonoBehaviour
{
    public int sceneIndex;

    void OnTriggerEnter(Collider other)
    {

        //sceneIndex = 0;
        SceneManager.LoadScene(sceneIndex);

    }
}

