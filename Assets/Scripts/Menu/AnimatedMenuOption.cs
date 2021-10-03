using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class AnimatedMenuOption : MonoBehaviour
{
    public AnimationCurve VertexCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.25f, 2.0f),
        new Keyframe(0.5f, 0), new Keyframe(0.75f, 2.0f), new Keyframe(1, 0f));

    public float CurveScale = 1.0f;

    private Coroutine coroutine;
    private TMP_Text m_TextComponent;
    public void Start()
    {
        m_TextComponent = transform.GetComponent<TMP_Text>();
    }

    public void StartAnimate()
    {
        coroutine = StartCoroutine(AnimateWave());
    }

    public void StopAnimate()
    {
        /*if (m_TextComponent != null)
        {
            
        }*/
        StopAllCoroutines();
        ResetGeometry();
    }

    public void OnEnable()
    {
        ResetGeometry();
    }

    private void UpdateMesh(Vector3[] _vertex, int index)
    {
        m_TextComponent.mesh.vertices = _vertex;
        m_TextComponent.mesh.uv = m_TextComponent.textInfo.meshInfo[index].uvs0;
        m_TextComponent.mesh.uv2 = m_TextComponent.textInfo.meshInfo[index].uvs2;
        m_TextComponent.mesh.colors32 = m_TextComponent.textInfo.meshInfo[index].colors32;
    }
    
    void ResetGeometry()
    {
        if (m_TextComponent == null) return;
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var newVertexPositions = textInfo.meshInfo[i].vertices;

            // Upload the mesh with the revised information
            UpdateMesh(newVertexPositions, i);
        }

        m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        m_TextComponent.ForceMeshUpdate(); // Generate the mesh and populate the textInfo with data we can use and manipulate.
    }

    IEnumerator AnimateWave()
    {
        VertexCurve.preWrapMode = WrapMode.Loop;
        VertexCurve.postWrapMode = WrapMode.Loop;

        Vector3[] newVertexPositions;
        //Matrix4x4 matrix;

        int loopCount = 0;

        while (true)
        {

            m_TextComponent.renderMode = TextRenderFlags.DontRender; // Instructing TextMesh Pro not to upload the mesh as we will be modifying it.
            //m_TextComponent.ForceMeshUpdate(); // Generate the mesh and populate the textInfo with data we can use and manipulate.
            
            ResetGeometry();
            TMP_TextInfo textInfo = m_TextComponent.textInfo;
            int characterCount = textInfo.characterCount;

            newVertexPositions = (Vector3[]) textInfo.meshInfo[0].vertices.Clone();

            for (int i = 0; i < characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                float offsetY = VertexCurve.Evaluate((float)i / characterCount + loopCount / 50f) * CurveScale; // Random.Range(-0.25f, 0.25f);                    

                newVertexPositions[vertexIndex + 0].y += offsetY;
                newVertexPositions[vertexIndex + 1].y += offsetY;
                newVertexPositions[vertexIndex + 2].y += offsetY;
                newVertexPositions[vertexIndex + 3].y += offsetY;
            }

            loopCount += 1;

            // Upload the mesh with the revised information
            //m_TextComponent.mesh.vertices = newVertexPositions;
            //m_TextComponent.mesh.uv = m_TextComponent.textInfo.meshInfo[0].uvs0;
            //m_TextComponent.mesh.uv2 = m_TextComponent.textInfo.meshInfo[0].uvs2;
            //m_TextComponent.mesh.colors32 = m_TextComponent.textInfo.meshInfo[0].colors32;

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = newVertexPositions;//textInfo.meshInfo[i].vertices;
                m_TextComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return new WaitForSeconds(0.025f);
        }
    }
}