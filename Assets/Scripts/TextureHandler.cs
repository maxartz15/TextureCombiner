using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextureCombiner
{
	[RequireComponent(typeof(TextureCombiner))]
	public abstract class TextureHandler : MonoBehaviour
	{
	    protected TextureCombiner m_textureCombiner = null;
	
	    private void Awake()
	    {
	        m_textureCombiner = this.GetComponent<TextureCombiner>();
	    }
	
	    protected virtual void OnEnable()
	    {
	        m_textureCombiner.m_loadDialogAction += Load;
	        m_textureCombiner.m_saveDialogAction += Save;
	        m_textureCombiner.m_saveTextureAction += SaveTexture;
	    }
	
	    protected virtual void OnDisable()
	    {
	        m_textureCombiner.m_loadDialogAction -= Load;
	        m_textureCombiner.m_saveDialogAction -= Save;
	        m_textureCombiner.m_saveTextureAction -= SaveTexture;
	    }
	
	    protected virtual void Load()
	    {
	    }
	
	    protected virtual void Save(string name)
	    {
	        m_textureCombiner.SaveTextureBytes(name);
	    }
	
	    protected virtual void SaveTexture(string path, byte[] bytes)
	    {   
	    }
	}
}