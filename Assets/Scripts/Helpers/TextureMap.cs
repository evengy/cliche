using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class TextureMap : MonoBehaviour
    {
        private MeshFilter meshFilter;

        private Mesh mesh;
        // Start is called before the first frame update
        void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            mesh = meshFilter.mesh;
            Vector2[] uvMap = mesh.uv;
            // Front
            uvMap[0] = new Vector2(0, 0);
            uvMap[1] = new Vector2(1f / 3f, 0);
            uvMap[2] = new Vector2(0, 1f / 3f);
            uvMap[3] = new Vector2(1f / 3f, 1f / 3f);
            // Top
            uvMap[4] = new Vector2(1f / 3f, 1f / 3f);
            uvMap[5] = new Vector2(2f / 3f, 1f / 3f);
            uvMap[8] = new Vector2(1f / 3f, 0);
            uvMap[9] = new Vector2(2f / 3f, 0);
            // Back
            uvMap[6] = new Vector2(1, 0);
            uvMap[7] = new Vector2(2f / 3f, 0);
            uvMap[10] = new Vector2(1, 1f / 3f);
            uvMap[11] = new Vector2(2f / 3f, 1f / 3f);
            // Bottom
            uvMap[12] = new Vector2(0, 1f / 3f);
            uvMap[13] = new Vector2(0, 2f / 3f);
            uvMap[14] = new Vector2(1f / 3f, 2f / 3f);
            uvMap[15] = new Vector2(1f / 3f, 1f / 3f);
            // Left
            uvMap[16] = new Vector2(1f / 3f, 1f / 3f);
            uvMap[17] = new Vector2(1f / 3f, 2f / 3f);
            uvMap[18] = new Vector2(2f / 3f, 2f / 3f);
            uvMap[19] = new Vector2(2f / 3f, 1f / 3f);
            // Right
            uvMap[20] = new Vector2(2f / 3f, 1f / 3f);
            uvMap[21] = new Vector2(2f / 3f, 2f / 3f);
            uvMap[22] = new Vector2(1, 2f / 3f);
            uvMap[23] = new Vector2(1, 1f / 3f);

            mesh.uv = uvMap;
        }
    }
}
