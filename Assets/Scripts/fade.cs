using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour {

    public GameObject PanelTransition;
    public Image Fume;
    public Color[] ColorTransition;
    public float Step;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeIn()
    {
        PanelTransition.SetActive(true);
        StartCoroutine("FadeInCoRoutine");
    }

    public void FadeOut()
    {
        PanelTransition.SetActive(true);
        StartCoroutine("FadeOutCoRoutine");
    }

    IEnumerator FadeInCoRoutine()
    {
        for(float i=0; i < 1; i += Step)
        {
            Fume.color = Color.Lerp(ColorTransition[0], ColorTransition[1], i);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOutCoRoutine()
    {
        for (float i = 0; i < 1; i += Step)
        {
            Fume.color = Color.Lerp(ColorTransition[1], ColorTransition[0], i);
            yield return new WaitForEndOfFrame();
        }
        PanelTransition.SetActive(false);
    }
}
