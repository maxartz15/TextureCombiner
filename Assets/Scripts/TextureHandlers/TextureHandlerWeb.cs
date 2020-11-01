using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_WEBGL
using System.Text;
using System.Runtime.InteropServices;
#endif

namespace TextureCombiner
{
	public class TextureHandlerWeb : TextureHandler
	{
	#if UNITY_WEBGL
	    protected override void OnEnable()
	    {
	        base.OnEnable();
	    }
	
	    protected override void OnDisable()
	    {
	        base.OnDisable();
	    }
	
	    protected override void Load()
	    {
	        UploadFile(this.gameObject.name, "OnLoadTexturesWWW", ".png, .jpg, .jpeg", true);
	    }
	
	    public void OnLoadTexturesWWW(string path)
	    {
	        string[] paths = path.Split(',');
	        StartCoroutine(LoadTexturesWWW(paths));
	    }
	
	    public IEnumerator LoadTexturesWWW(string[] paths)
	    {
	        if (paths.Length != 0)
	        {
	            foreach (string p in paths)
	            {
	                Debug.Log(p);
	
	                // Load Image.
	                var loader = new WWW(p);
	                yield return loader;
	                m_textureCombiner.LoadTextureSlot(loader.texture);
	            }
	        }
	    }
	
	    public void OnSaveTextureWWW()
	    {
	        Debug.Log("OnSaveTextureWWW");
	    }
	
	    [DllImport("__Internal")]
	    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);
	    [DllImport("__Internal")]
	    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);
	
	    protected override void Save(string name)
	    {
	        base.Save(name);
	    }
	
	    protected override void SaveTexture(string path, byte[] bytes)
	    {
	        DownloadFile(this.gameObject.name, "OnSaveTextureWWW", path, bytes, bytes.Length);
	    }
	#endif
	}
}