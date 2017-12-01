using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    public float scrollSpeed;
    MeshRenderer mesh;

    private void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update () {
        float FloatOffset = Time.deltaTime * scrollSpeed;
        Vector2 VecOffset = new Vector2(mesh.material.mainTextureOffset.x + Time.deltaTime * FloatOffset, 0);
        mesh.material.mainTextureOffset = VecOffset;
    }
}
