using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource gameMusic;

    [Space(10), Header("Sound Effect")]
    [SerializeField] List<AudioSource> channels = new List<AudioSource>();
    [SerializeField] AudioClip sfx1;
    [SerializeField] List<AudioClip> sfxList;
    [SerializeField] int index;
    public int _channel;
    [Space(10), Header("Music")]
    [SerializeField] AudioClip backgroundMusic;

    [SerializeField] GameObject audioChild;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        _channel = 0;
    }
    
    // DESIGN PATTERN

    // - SINGLETON
    // - OBJECT POOLING

    void Start()
    {
        //PlaySound(index);
        //PlaySfx(2,.4f);
        //PlaySfx(Type_Sfx.Jump, .7f);
        //yield return new WaitForSeconds(2);
        //PlayBackGroundMusic(backgroundMusic, .8f);

        LazyPooling.Instance.CreatePool(audioChild,10);
        AudioSource child = LazyPooling.Instance.getObj<AudioSource>(audioChild);
        
    }

    public void PlayBackGroundMusic(AudioClip clip, float volume)
    {
        gameMusic.clip = clip;
        gameMusic.loop = true;
        gameMusic.volume = volume;
        gameMusic.Play();
    }
    //void PlaySound(int index)
    //{
    //    sfxSource.clip = sfxList[index];
    //    sfxSource.volume = .6f;
    //    sfxSource.Play();
    //}
    public void PlaySfx(int index, float volume)
    {
        channels[_channel].PlayOneShot(sfxList[index], volume);
        _channel++;
        if (_channel > channels.Count) _channel = 0;
    }
    public void PlaySfx(Type_Sfx typeSfx, float volume)
    {
        channels[_channel].PlayOneShot(sfxList[(int)typeSfx], volume);
        _channel++;
        if (_channel > channels.Count) _channel = 0;
    }
}

public enum Type_Sfx
{
    Jump, Hit, Attack, CollectCoin,
}
