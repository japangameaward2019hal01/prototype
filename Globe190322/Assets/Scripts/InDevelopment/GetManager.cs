using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetManager : MonoBehaviour {

    [SerializeField]
    private RotationManager rotationManager = null;

	// Use this for initialization
	void Start () {
        if (!rotationManager) rotationManager = GameObject.Find("Manager").GetComponent<RotationManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
