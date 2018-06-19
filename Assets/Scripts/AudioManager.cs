using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager audioManager;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource BackGroundMusic;
    [SerializeField]
    AudioClip[] clip;

    private void Awake()
    {
        audioManager = this;
    }
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerVoice(string voiceName)
    {
        switch (voiceName)
        {
            case "Coin":
                audioSource.clip = clip[0];
                break;
            case "Jump":
                audioSource.clip = clip[1];
                Debug.Log("bb");
                break;
            case "EatMushroom":
                audioSource.clip = clip[2];
                break;
            case "Win":
                audioSource.clip = clip[3];
                BackGroundMusic.Stop();
                break;
            case "Gameover":
                audioSource.clip = clip[4];
                BackGroundMusic.Stop();
                break;
            case "Deth":
                audioSource.clip = clip[5];
                break;
            case "kill":
                audioSource.clip = clip[6];
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
