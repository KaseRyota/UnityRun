using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class invincibleTimesManager : MonoBehaviour {

    public Text invincibleText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        invincibleText.text = ("無敵残り回数:" + PlayerScript.invincibleTimes);   
    }
}
