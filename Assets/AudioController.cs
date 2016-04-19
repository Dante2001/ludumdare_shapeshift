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
        onMajor = false;
        bgmMajor.mute = true;
        curPitch = 1f;
    }

    void Update()
    {
        if (bgmMajor.time >= 84f || bgmMinor.time >= 84f)
        {
            bgmMajor.time = 0;
            bgmMinor.time = 0;
        }
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
    // not being used anymore
    public void SpeedUp()
    {
        curPitch += speedModifier;
        bgmMajor.pitch = curPitch;
        bgmMinor.pitch = curPitch;
    }
    public void SpeedDown(float speed)
    {
        curPitch -= speed;
        bgmMajor.pitch = curPitch;
        bgmMinor.pitch = curPitch;
    }

    public void SetSpeedMultiplier(float mult)
    {
        curPitch = 1f * mult;
        //curPitch = curPitch - (curPitch % 0.2f);
        bgmMajor.pitch = curPitch;
        bgmMinor.pitch = curPitch;
    }

    public void DeaccelerateOnGameover()
    {
        StartCoroutine(DeaccelerateOverTime(0.1f));
    }

    IEnumerator DeaccelerateOverTime(float acc)
    {
        while (true)
        {
            if (curPitch > 0.5f)
                SpeedDown(acc);
            yield return new WaitForEndOfFrame();
        }
    }
}
