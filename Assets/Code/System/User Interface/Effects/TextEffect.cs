using UnityEngine;
using TMPro;

namespace Memoria
{
    public class TextEffect : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake() => text = GetComponent<TMP_Text>();

        private void Update()
        {
            text.ForceMeshUpdate();
            var info = text.textInfo;

            for (int i = 0; i < info.characterCount; i++)
            {
                var charInfo = info.characterInfo[i];
                if (!charInfo.isVisible) continue;

                var verts = info.meshInfo[charInfo.materialReferenceIndex].vertices;
                for (int j = 0; j < 4; j++)
                {
                    var origin = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 2 + origin.x * 0.01f) * 10, 0);
                }
            }

            for (int i = 0; i < info.meshInfo.Length; i++)
            {
                var mesh = info.meshInfo[i];
                mesh.mesh.vertices = mesh.vertices;
                text.UpdateGeometry(mesh.mesh, i);
            }
        }
    }
}