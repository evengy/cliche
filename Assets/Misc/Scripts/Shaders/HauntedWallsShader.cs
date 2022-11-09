using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedWallsShader : MonoBehaviour
{
    [SerializeField] float scale1 = 20f;
    [SerializeField] float scale2 = 20f;
    [SerializeField] float intensity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetFloat("_Noise_Scale_1", Random.Range(10,scale1));
        gameObject.GetComponent<Renderer>().material.SetFloat("_Noise_Scale_2", Random.Range(10,scale2));
        gameObject.GetComponent<Renderer>().material.SetFloat("_Noise_Intensity", Random.Range(0.01f,intensity));
        Debug.Log(this.gameObject.GetComponent<Renderer>().material.GetFloat("_Noise_Scale_1"));
        Debug.Log(this.gameObject.GetComponent<Renderer>().material.GetFloat("_Noise_Scale_2"));
        Debug.Log(this.gameObject.GetComponent<Renderer>().material.GetFloat("_Noise_Intensity"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
