using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_STANDALONE_WIN
using B83.Win32;
#endif
#if UNITY_STANDALONE
using SFB;
#endif

namespace TextureCombiner
{
	public class TextureHandlerWin : TextureHandler
	{
	#if UNITY_STANDALONE
	    protected override void OnEnable()
	    {
	#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
	        UnityDragAndDropHook.InstallHook();
	        UnityDragAndDropHook.OnDroppedFiles += LoadDropPaths;
	#endif
	
	        base.OnEnable();
	    }
	
	    protected override void OnDisable()
	    {
	#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
	        UnityDragAndDropHook.OnDroppedFiles -= LoadDropPaths;
	        UnityDragAndDropHook.UninstallHook();
	#endif
	
	        base.OnDisable();
	    }
	
	    protected override void Load()
	    {
	        // Open file with filter
	        var extensions = new[]
	        {
	            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
	        };
	
	        StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", extensions, true, LoadPaths);
	    }
	
	#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
	    private void LoadDropPaths(List<string> paths, POINT aPos)
	    {
	        List<string> outPaths = new List<string>();
	
	        foreach (string p in paths)
	        {
	            var fi = new System.IO.FileInfo(p);
	            var ext = fi.Extension.ToLower();
	            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
	            {
	                outPaths.Add(p);
	            }
	        }
	
	        if(outPaths.Count > 0)
	        {
	            LoadPaths(outPaths.ToArray());
	        }
	    }
	#endif
	
	    private void LoadPaths(string[] paths)
	    {
	        if(paths.Length != 0)
	        {
	            LoadTextures(paths);
	        }
	    }
	
	    private void LoadTextures(string[] paths)
	    {
	        if(paths.Length != 0)
	        {
	            foreach (string p in paths)
	            {
	                Debug.Log(p);
	
	                // Load Image.
	                var bytes = System.IO.File.ReadAllBytes(p);
	                Texture2D texture = new Texture2D(2, 2);
	                texture.hideFlags = HideFlags.HideAndDontSave;
	                texture.LoadImage(bytes);
	                texture.Apply();
	
	                m_textureCombiner.LoadTextureSlot(texture);
	            }
	        }
	    }
	
	    protected override void Save(string name)
	    {
	        // Open file with filter
	        var extensions = new[]
	        {
	                new ExtensionFilter("Image Files", "png")
	        };
	
	        StandaloneFileBrowser.SaveFilePanelAsync("Save file", "", name, extensions, base.Save);
	    }
	
	    protected override void SaveTexture(string path, byte[] bytes)
	    {
	        System.IO.File.WriteAllBytes(path, bytes);
	    }
	#endif
	}
}