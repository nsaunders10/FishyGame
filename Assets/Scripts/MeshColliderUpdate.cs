using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderUpdate : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    MeshCollider col;

    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        col = GetComponent<MeshCollider>();
    }

    
    void Update()
    {
        UpdateCollider();
    }

   

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        col.sharedMesh = null;
        col.sharedMesh = colliderMesh;
    }
}
