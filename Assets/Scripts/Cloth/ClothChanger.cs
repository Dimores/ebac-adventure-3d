using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public List<SkinnedMeshRenderer> mesh;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private List<Texture> _defaultTextures = new List<Texture>();

        private void Awake()
        {
            _defaultTextures.Clear();

            foreach (var renderer in mesh)
            {
                if (renderer != null &&
                    renderer.materials.Length > 0)
                {
                    _defaultTextures.Add(renderer.materials[0].GetTexture(shaderIdName));
                }
                else
                {
                    _defaultTextures.Add(null);
                }
            }
        }

        public void ChangeTexture()
        {
            if (texture == null)
                return;

            foreach (var renderer in mesh)
            {
                if (renderer != null &&
                    renderer.materials.Length > 0)
                {
                    renderer.materials[0].SetTexture(shaderIdName, texture);
                }
            }
        }

        public void ChangeTexture(ClothSetup clothSetup)
        {
            if (clothSetup == null || clothSetup.texture == null)
                return;

            foreach (var renderer in mesh)
            {
                if (renderer != null &&
                    renderer.materials.Length > 0)
                {
                    renderer.materials[0].SetTexture(shaderIdName, clothSetup.texture);
                }
            }
        }

        public void ResetTexture()
        {
            for (int i = 0; i < mesh.Count; i++)
            {
                if (mesh[i] != null &&
                    mesh[i].materials.Length > 0)
                {
                    mesh[i].materials[0].SetTexture(shaderIdName, _defaultTextures[i]);
                }
            }
        }
    }
}