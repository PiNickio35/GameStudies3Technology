using UnityEngine;

namespace _PROJECT.Scripts
{
    public class TerrainManipulation : MonoBehaviour
    {
        private Renderer _materialRenderer;
        [SerializeField] private float offsetSpeed = 0.2f;

        private void Awake()
        {
            if (_materialRenderer == null)
            {
                _materialRenderer = GetComponent<Renderer>();
            }
        }

        private void FixedUpdate()
        {
            if (_materialRenderer != null)
            {
                Vector2 textureOffsetVector = new Vector2(_materialRenderer.material.GetTextureOffset("_BaseColorMap").x + offsetSpeed, _materialRenderer.material.GetTextureOffset("_BaseColorMap").y);
                _materialRenderer.material.SetTextureOffset("_BaseColorMap", textureOffsetVector);
            }
        }
    }
}
