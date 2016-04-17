using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartBubbles : MonoBehaviour {

    public GameObject[] hearts;
    private int currentHeartCount;

	// Use this for initialization
	void Start () {
	    currentHeartCount = hearts.Length;
	}

    public void LoseHeart()
    {
        if (currentHeartCount > 0)
        {
            Image tempHeart;
            tempHeart = hearts[currentHeartCount - 1].transform.GetChild(1).GetComponentInChildren<Image>();
            tempHeart.enabled = false;
            currentHeartCount -= 1;
        }
    }

    public void RegainHeart()
    {
        if (currentHeartCount < hearts.Length)
        {
            Image tempHeart;
            tempHeart = hearts[currentHeartCount - 1].transform.GetChild(1).GetComponentInChildren<Image>(true);
            tempHeart.enabled = true;
            currentHeartCount += 1;
        }
    }

}

