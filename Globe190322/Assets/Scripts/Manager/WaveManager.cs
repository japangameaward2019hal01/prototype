using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : RotationManager {

    public RotationManager rotationManager;
    public Material OceanMat;
    private Vector4 Speed;
	// Use this for initialization
	void Start () {
        Speed.z = -3.5f;
	}
	
	// Update is called once per frame
	void Update () {
       Debug.Log(rotationManager.Get_AxisRotation());
        if (rotationManager.Get_AxisRotation())
        {
            if (rotationManager.Get_Rotation() > 0)
            {
                Speed.z = -3.5f;
            }
            else if (rotationManager.Get_Rotation() < 0)
            {
                Speed.z = 3.5f;
            }
        }
        else
        {
            Speed = new Vector4(0, 0, 0, 0);
        }
        OceanMat.SetVector("_FlowSpeed", Speed);
    }
}
