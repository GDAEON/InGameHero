using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private float updateRate;
    void Start()
    {
        StartCoroutine(UpdateVFXGraph());
    }

    IEnumerator UpdateVFXGraph()
    {
        while (gameObject.activeSelf)
        {
            Mesh mesh = new Mesh();
            skinnedMesh.BakeMesh(mesh);
            visualEffect.SetMesh("Mesh", mesh);
        }

        yield return new WaitForSeconds(updateRate);
    }
}
