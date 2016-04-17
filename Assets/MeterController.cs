using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeterController : MonoBehaviour {

    public Slider sanityMeter;
    public Slider fullnessMeter;
    public Slider healthMeter;

    public float sanityConstantDrain;
    public float fullnessConstantDrain;
    public float healthConstantDrain;

    public float sanityBurstDrain;
    public float sanityRegeneration;
    public float fullnessRegeneration;

    public float healthNoSanityDrain;
    public float healthNoFullnessDrain;

    private bool noSanityHealthDrain = false;
    private bool noFullnessHealthDrain = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckHealth();
        DrainMeters();
	}

    void CheckHealth()
    {
        if (!noSanityHealthDrain && sanityMeter.value <= 0)
        {
            healthConstantDrain += healthNoSanityDrain;
            noSanityHealthDrain = true;
        }
        else if (noSanityHealthDrain && sanityMeter.value > 0)
        {
            healthConstantDrain -= healthNoSanityDrain;
            noSanityHealthDrain = false;
        }

        if (!noFullnessHealthDrain && fullnessMeter.value <= 0)
        {
            healthConstantDrain += healthNoFullnessDrain;
            noSanityHealthDrain = true;
        }
        else if (noFullnessHealthDrain && fullnessMeter.value > 0)
        {
            healthConstantDrain -= healthNoFullnessDrain;
            noSanityHealthDrain = false;
        }
    }

    void DrainMeters ()
    {
        sanityMeter.value -= sanityConstantDrain * Time.deltaTime;
        fullnessMeter.value -= fullnessConstantDrain * Time.deltaTime;
        healthMeter.value -= healthConstantDrain * Time.deltaTime;
    }

    // well this function was basically useless >_________>
    IEnumerator ResetDrain(string meter, float resetAmount, float afterTime = 1.0f)
    {
        yield return new WaitForSeconds(afterTime);
        if (meter == "sanity")
            sanityConstantDrain -= resetAmount;
        else if (meter == "fullness")
            fullnessConstantDrain -= resetAmount;
        else if (meter == "health")
            healthConstantDrain -= resetAmount;
    }

    public void SanityDrainFaster()
    {
        sanityConstantDrain += sanityBurstDrain;
        StartCoroutine(ResetDrain("sanity", sanityBurstDrain));
    }

    public void SanityUp()
    {
        sanityMeter.value += sanityRegeneration;
    }

    public void FullnessUp()
    {
        fullnessMeter.value += fullnessRegeneration;
    }

    public bool NoHealth()
    {
        if (healthMeter.value <= 0)
            return true;
        return false;
    }

    public float SanityPlayerSpeedMultiplier()
    {
        return 1f + (sanityMeter.maxValue - sanityMeter.value) / (sanityMeter.maxValue / 4f);
    }

    public float SanityAudioSpeedMultiplier()
    {
        return 1f + (sanityMeter.maxValue - sanityMeter.value) / (sanityMeter.maxValue * 2);
    }

}
