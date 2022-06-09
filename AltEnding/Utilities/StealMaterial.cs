using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class StealMaterial : MonoBehaviour
{
    public static Dictionary<string, Material> allMaterials;
    public string[] materialNames = null;

    public static List<string> debugListOfNames;

    public Material[] foundMaterials;

    void Start()
    {
        if (allMaterials == null) 
        {
            allMaterials = new Dictionary<string, Material>();
            foreach (var renderer in Resources.FindObjectsOfTypeAll<Renderer>())
            {
                foreach(var material in renderer.sharedMaterials)
                {
                    if (material == null) continue;
                    allMaterials[material.name] = material;
                }
            }
            
            foreach (var renderer in Resources.FindObjectsOfTypeAll<TessellatedRenderer>()) // surprisingly does not inherit from Renderer
            {
                foreach(var material in renderer.sharedMaterials)
                {
                    if (material == null) continue;
                    allMaterials[material.name] = material;
                }
            }

            debugListOfNames = new List<string>(allMaterials.Keys);
            debugListOfNames.Sort();
        }

        if (materialNames == null || materialNames.Length == 0) return;
        var myRenderer = GetComponent<Renderer>();
        // myRenderer.sharedMaterials = new Material[materialNames.Length];
        foundMaterials = new Material[materialNames.Length];
        for(int i = 0; i < materialNames.Length; i++) foundMaterials[i] = allMaterials[materialNames[i]];
         myRenderer.sharedMaterials = foundMaterials;
    }
}
