using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class SbSttrTester : MonoBehaviour {

    public SkyboxSetter ss;

    // Testing Stuff
    [SerializeField]
    private Texture imageA;
    [SerializeField]
    private Texture imageB;
    [SerializeField]
    private VideoClip videoC;
    [SerializeField]
    private VideoClip videoD;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetKeyDown("a") ){
            Debug.Log("A");
            ss.SetSkybox(imageA);
        } else if( Input.GetKeyDown("b") ) {
            Debug.Log("B");
            ss.SetSkybox(imageB);
        } else if (Input.GetKeyDown("c") ) {
            Debug.Log("C");
            ss.SetSkybox(videoC);
        } else if (Input.GetKeyDown("d") ) {
            Debug.Log("D");
            ss.SetSkybox(videoD);
        }
    }
}
