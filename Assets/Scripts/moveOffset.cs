using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {

    private Material material;
    public float speedX;
    public float speedY;
    public float increment;
    private float offset;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        offset += increment;
        material.SetTextureOffset("_MainTex", new Vector2(offset * speedX, offset * speedY));
	}
}
