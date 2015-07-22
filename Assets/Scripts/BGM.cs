using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {

    private AudioClip _bgm; 
	// Use this for initialization
	void Start () {

        _bgm = Resources.Load("Audio/bgm_piano", typeof(AudioClip)) as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
