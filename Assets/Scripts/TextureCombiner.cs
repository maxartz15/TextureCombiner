using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System;
using SFB;

namespace TextureCombiner
{
    public class TextureCombiner : MonoBehaviour
    {
        [SerializeField]
        private TextureSlot m_textureSlotPrefab = null;
        [SerializeField]
        private RectTransform m_parent = null;

        [Space]

        [SerializeField]
        private Button m_openButton = null;
        [SerializeField]
        private Button m_combineButton = null;
        [SerializeField]
        private Button m_saveButton = null;

        [Space]

        [SerializeField]
        private Shader m_combineShader = null;
        [SerializeField]
        private RawImage m_imageOutput = null;

        private Texture2D m_source = null;
        private RenderTexture m_dest = null;

        private SizeOptions m_sizeOptions = null;
        private AddOptions m_addOptions = null;
        private CombineOptions m_combineOptions = null;
        private SaveOptions m_saveOptions = null;

        public Action m_loadDialogAction = null;
        public Action<string> m_saveDialogAction = null;
        public Action<string, byte[]> m_saveTextureAction = null;

        private void OnEnable()
        {
            m_openButton.onClick.AddListener(Load);
            m_combineButton.onClick.AddListener(CombineTextures);
            m_saveButton.onClick.AddListener(Save);

            m_sizeOptions = GetComponentInChildren<SizeOptions>();
            m_addOptions = GetComponentInChildren<AddOptions>();
            m_combineOptions = GetComponentInChildren<CombineOptions>();
            m_saveOptions = GetComponentInChildren<SaveOptions>();

            m_source = new Texture2D(2, 2);
            m_source.Apply();
        }

        private void OnDisable()
        {
            m_openButton.onClick.RemoveListener(Load);
            m_combineButton.onClick.RemoveListener(CombineTextures);
            m_saveButton.onClick.RemoveListener(Save);
        }

        private void Update()
        {
            if(m_combineOptions.GetAutoCombine())
            {
                CombineTextures();
            }
        }

        private void CombineTextures()
        {
            Material mat = new Material(m_combineShader);

            // Set material textures.
            foreach (TextureSlot ts in GetComponentsInChildren<TextureSlot>())
            {
                Vector4 sourceChannel = ts.GetSourceChannels();
                TextureSlot.Channels targetChannel = ts.GetTargetChannels();

                if (targetChannel.r)
                {
                    mat.SetVector("_Mask0", sourceChannel);
                    mat.SetTexture("_Tex0", ts.texture);
                }

                if (targetChannel.g)
                {
                    mat.SetVector("_Mask1", sourceChannel);
                    mat.SetTexture("_Tex1", ts.texture);
                }

                if (targetChannel.b)
                {
                    mat.SetVector("_Mask2", sourceChannel);
                    mat.SetTexture("_Tex2", ts.texture);
                }

                if (targetChannel.a)
                {
                    mat.SetVector("_Mask3", sourceChannel);
                    mat.SetTexture("_Tex3", ts.texture);
                }
            }

            mat.SetVector("_Add", m_addOptions.GetAddValues());

            if (m_dest)
            {
                if (RenderTexture.active == m_dest)
                {
                    RenderTexture.active = null;
                }
                m_dest.Release();
                m_dest = null;
            }

            m_dest = new RenderTexture(m_sizeOptions.width, m_sizeOptions.height, 0);
            m_dest.Create();

            Graphics.Blit(m_source, m_dest, mat);

            m_imageOutput.texture = m_dest;
        }

        public void Load()
        {
            m_loadDialogAction.Invoke();
        }

        public void LoadTextureSlot(Texture2D texture2D)
        {
            TextureSlot ts = Instantiate(m_textureSlotPrefab, m_parent);
            ts.Init(texture2D);
        }

        public void Save()
        {
            m_saveDialogAction.Invoke(m_saveOptions.GetOutputName());
        }

        public void SaveTextureBytes(string path)
        {
            if (m_dest)
            {
                // Get texture from renderTexture.
                RenderTexture curRT = RenderTexture.active;
                RenderTexture.active = m_dest;

                Texture2D tex = new Texture2D(m_dest.width, m_dest.height);
                tex.hideFlags = HideFlags.HideAndDontSave;
                tex.ReadPixels(new Rect(0, 0, m_dest.width, m_dest.height), 0, 0);
                tex.Apply();

                RenderTexture.active = curRT;

                // Convert texture to bytes.
                byte[] bytes;
                bytes = tex.EncodeToPNG();

                Destroy(tex);

                m_saveTextureAction.Invoke(path, bytes);
            }
        }
    }
}