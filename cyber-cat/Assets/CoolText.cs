using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoolText : MonoBehaviour
{
    public TMP_Text textComponent;

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charinfo = textInfo.characterInfo[i];

            if (!charinfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charinfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charinfo.vertexIndex + j];
                verts[charinfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}

