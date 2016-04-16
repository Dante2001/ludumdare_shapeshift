using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{

    public AudioSource bgmMajor;
    public AudioSource bgmMinor;

    public float speedModifier;

    private bool onMajor;
    private float curPitch;
    private bool isStarted = false;

    // Use this for initialization
    void Start()
    {
        onMajor = true;
        bgmMinor.mute = true;
        curPitch = 1f;
    }

    public void PlayAudio()
    {
        if (!isStarted)
        {
            bgmMajor.Play();
            bgmMinor.Play();
        }
    }

    public void SwapAudio()
    {
        if (onMajor)
        {
            bgmMinor.mute = false;
            bgmMajor.mute = true;
        }
        else
        {
            bgmMajor.mute = false;
            bgmMinor.mute = true;
        }
        onMajor = !onMajor;
    }

    public void SpeedUp()
    {
        curPitch += speedModifier;
        bgmMajor.pitch = curPitch;
        bgmMinor.pitch = curPitch;
    }

    public void SpeedDown()
    {
        curPitch -= speedModifier;
        bgmMajor.pitch = curPitch;
        bgmMinor.pitch = curPitch;
    }
}
