using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D) mesh.materials[0].GetTexture(shaderIdName);
        }

        public void ChangeTexture()
        {
            if (mesh != null && texture != null)
            {
                mesh.materials[0].SetTexture(shaderIdName, texture);
            }
        }
        
        public void ChangeTexture(ClothSetup clothSetup)
        {
            if (mesh != null && clothSetup != null)
            {
                mesh.materials[0].SetTexture(shaderIdName, clothSetup.texture);
            }
        }

        public void ResetTexture()
        {
            if (mesh != null && _defaultTexture != null)
            {
                mesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
            }
        }
    }
}
