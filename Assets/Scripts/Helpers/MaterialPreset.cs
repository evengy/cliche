using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class MaterialPreset : MonoBehaviour
    {
        [SerializeField] Material preset;
        void Start()
        {
            GetComponent<Renderer>().material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            GetComponent<Renderer>().material.CopyPropertiesFromMaterial(preset);
        }
    }
}