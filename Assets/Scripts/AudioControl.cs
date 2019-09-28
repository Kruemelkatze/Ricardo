 using System.Linq;
 using Hellmade.Sound;
 using UnityEngine;
 
public class AudioControl : MonoBehaviour
{
    public ObjectDictionary SoundClips;
    public ObjectDictionary MusicClips;
 
    public string DefaultMusic;
    public bool IgnoreDuplicateMusic = true;
 
    private static int _currentMusicId;
    private static string _currentMusic;

    private static AudioControl _instance;
    public static AudioControl Instance => _instance;
    
    void OnEnable()
    {
        //This is buggy, see https://github.com/JackM36/Eazy-Sound-Manager/issues/7, use own tracking
        //var unusedGO = SoundManager.gameobject;
        //Debug.Log(unusedGO.name); // Simply for not letting unusedGO being optimized away
        //SoundManager.ignoreDuplicateMusic = true;
    }

    void Awake()
    {
        _instance = GetComponent<AudioControl>();
    }
 
    // Simple functions for demo usage with button OnClick()
    public void PlayDefaultMusic()
    {
        PlayDefaultMusic(EazySoundManager.GlobalMusicVolume);
    }
 
    public void PlayMusic(string key)
    {
        PlayMusic(key, EazySoundManager.GlobalMusicVolume);
    }
 
    public void PlaySound(string key)
    {
        PlaySound(key, EazySoundManager.GlobalSoundsVolume);
    }
 
    public void PlayMusic(AudioClip clip)
    {
        PlayMusic(clip, EazySoundManager.GlobalMusicVolume);
    }
 
    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, EazySoundManager.GlobalSoundsVolume);
    }
 
    // Default theme helper
    public void PlayDefaultMusic(float volume= 1, bool loop = true, bool persist = true)
    {
        PlayMusic(DefaultMusic, volume, loop, persist);
    }
 
    // Basically wrappers for EazySoundManager's method, which fetch the AudioClip from the Dictionary
    public int PlayMusic(string key, float volume= 1, bool loop = true, bool persist = true)
    {
        if (IgnoreDuplicateMusic && _currentMusic == key)
            return _currentMusicId;
 
        Object clip;
        var found = MusicClips.TryGetValue(key, out clip);
        if (found)
        {
            _currentMusic = key;
            _currentMusicId = PlayMusic((AudioClip)clip, volume, loop, persist);
            return _currentMusicId;
        }
 
        return -1;
    }
 
    public int PlayMusic(AudioClip clip, float volume= 1, bool loop = true, bool persist = true)
    {
        return EazySoundManager.PlayMusic(clip, volume, loop, persist);
    }

    public int PlayRandomSound(string key, float volume= 1, Transform sourceTransform = null)
    {
        var clips = SoundClips.Where(kvp => kvp.Key.StartsWith(key)).Select(kvp => (AudioClip) kvp.Value).ToList();
        if (clips.Any())
        {
            var clip = clips[Random.Range(0, clips.Count)];
            return PlaySound(clip, volume, sourceTransform);
        }

        return -1;
    }
 
    public int PlaySound(string key, float volume= 1, Transform sourceTransform = null)
    {
        Object clip;
        var found = SoundClips.TryGetValue(key, out clip);
        if (found)
        {
            return PlaySound((AudioClip)clip, volume, sourceTransform);
        }
 
        return -1;
    }
 
    public int PlaySound(AudioClip clip, float volume= 1, Transform sourceTransform = null)
    {
        return EazySoundManager.PlaySound(clip, volume, false, sourceTransform);
    }
}