using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class ScoreDisplayScript : MonoBehaviour
{
    public int sceneIndex;

    int score;

    public Text textToChange;

    public VideoPlayer dogPlayer;
    public VideoPlayer swordPlayer;
    public VideoPlayer crownPlayer;
    public List<VideoPlayer> listOfVideos;
    private int chosenEndingVideo;


    public AudioClip endingSound;
    AudioSource endingSoundSource;
    int audioCounter = 0;


    void Awake() {

    }

    // Start is called before the first frame update
    void Start()
    {
        score = PlayheadMover.score;
        Debug.Log("score is: " + score);
        ChangeText();

        endingSoundSource = GetComponent<AudioSource>();
        audioCounter = 0;

        PlayEndingSound();

        listOfVideos = new List<VideoPlayer>();
        listOfVideos.Add(dogPlayer);
        listOfVideos.Add(swordPlayer);
        listOfVideos.Add(crownPlayer);

        float index = Random.Range(0.0f, 3.0f);
        int indexInt = (int)index;

        //chosenEndingVideo = listOfVideos[indexInt];

        listOfVideos[indexInt].Play();

        StartCoroutine(Wait());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait() {
        //yield return new WaitUntil(() => outcome == 1);
        //videoBackground.Play();
        //wellDoneText.SetActive(true);
        yield return new WaitForSeconds(8.5f);
        SceneManager.LoadScene(sceneIndex);
    }

    void PlayEndingSound() {
        if (endingSound != null)
        {
                endingSoundSource.PlayOneShot(endingSound, 0.7F);
        }
    }

    void ChangeText() {
        textToChange.text = ""+score+"";
    }

}
