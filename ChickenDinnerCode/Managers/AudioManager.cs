using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
   public Sound[] musicSounds,sfxSounds;
   public string currentClipName;
   public AudioSource musicSource,sfxSource;
   private void Awake() 
   {
     if (Instance==null)
     {
        Instance=this;
        DontDestroyOnLoad(gameObject);

     }
     else
     {
        Destroy(gameObject);
     }
   }
   public void RefreshSoundsVolume()
   {
      musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
      sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume");
   }
   public void PlayMusic(string name)
   {
       Sound s=Array.Find(musicSounds,x=>x.name==name);
       if (s==null)
       {
            Debug.Log("ses yok");
       }
       else
       {
          currentClipName = name;
          musicSource.clip=s.clip;
          musicSource.Play();
       }
   }
   public void PlaySFX(string name)
   {
       Sound s=Array.Find(sfxSounds,x=>x.name==name);
       if (s==null)
       {
            Debug.Log("ses yok");
       }
       else
       {
         sfxSource.PlayOneShot(s.clip);
       }
   }
  
}

[System.Serializable]
public class Sound 
{
    
    public string name;
    public AudioClip clip;
}