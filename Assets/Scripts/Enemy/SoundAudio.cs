using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAudio : MonoBehaviour
{
    public static SoundAudio _intance;
    public GameObject SoundPrefab;
    //Moss Giant -------------------------
    [Header("Sound Audio Moss Giant")]
    public AudioClip _footStepMoss;
    private AudioSource footMossSound;
    public AudioClip _attackMoss;
    private AudioSource attackMossSound;
    public AudioClip _deathMoss;
    private AudioSource deathMossSound;
    public AudioClip _hitMoss;
    private AudioSource hitMossSound;
    //Skeletion---------------
    [Header("Sound Audio Skeletion")]
    public AudioClip _footStepSkeletion;
    private AudioSource footSkeletionSound;
    public AudioClip _attackSkeletion;
    private AudioSource attackSkeletionSound;
    public AudioClip _deathSkeletion;
    private AudioSource deathSkeletionSound;
    public AudioClip _hitSkeletion;
    private AudioSource hitSkeletionSound;
    //Spider ----------------------
    [Header("Sound Audio Spider")]
    public AudioClip _footStepSpider;
    private AudioSource footSpiderSound;
    public AudioClip _attackSpider;
    private AudioSource attackSpiderSound;
    public AudioClip _deathSpider;
    private AudioSource deathSpiderSound;
    public AudioClip _idleSpider;
    private AudioSource idleSpiderSound;

    private void Awake()
    {
        _intance = this;
    }
    //------Moss Giant --------
    public void PlaySound(AudioClip clip, float volume,bool isOFF)
    {
        if(clip == this._footStepMoss)
        {
            Play(clip, ref footMossSound, volume);
            return;
        }
        if (clip == this._attackMoss)
        {
            Play(clip, ref attackMossSound, volume);
            return;
        }
        if (clip == this._deathMoss)
        {
            Play(clip, ref deathMossSound, volume);
            return;
        }
        if (clip == this._hitMoss)
        {
            Play(clip, ref hitMossSound, volume);
            return;
        }
    }
    public void StopSound(AudioClip clip)
    {
        if(clip == this._attackMoss)
        {
            attackMossSound?.Stop();
            return;
        }
        if (clip == this._deathMoss)
        {
            deathMossSound?.Stop();
            return;
        }
        if (clip == this._footStepMoss)
        {
            footMossSound?.Stop();
            return;
        }
        if (clip == this._hitMoss)
        {
            hitMossSound?.Stop();
            return;
        }
    }
    private void Play(AudioClip clip, ref AudioSource audioSource,
        float volume, bool isLoopback = false)
    {
        if(audioSource != null && audioSource.isPlaying)
        {
            return;
        }
        audioSource = Instantiate(_intance.SoundPrefab).GetComponent<AudioSource>();

        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.loop = isLoopback;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    //-----Skeletion-----------
    public void PlaySoundSkeletion(AudioClip clip, float volume)
    {
        if (clip == this._footStepSkeletion)
        {
            Play(clip, ref footSkeletionSound, volume);
            return;
        }
        if (clip == this._attackSkeletion)
        {
            Play(clip, ref attackSkeletionSound, volume);
            return;
        }
        if (clip == this._deathSkeletion)
        {
            Play(clip, ref deathSkeletionSound, volume);
            return;
        }
        if (clip == this._hitSkeletion)
        {
            Play(clip, ref hitSkeletionSound, volume);
            return;
        }
    }
    public void StopSoundSkeletion(AudioClip clip)
    {
        if (clip == this._attackSkeletion)
        {
            attackSkeletionSound?.Stop();
            return;
        }
        if (clip == this._deathSkeletion)
        {
            deathSkeletionSound?.Stop();
            return;
        }
        if (clip == this._footStepSkeletion)
        {
            footSkeletionSound?.Stop();
            return;
        }
        if (clip == this._hitSkeletion)
        {
            hitSkeletionSound?.Stop();
            return;
        }
    }
    //-----Spider-----------
    public void PlaySoundSpider(AudioClip clip, float volume)
    {
        if (clip == this._footStepSpider)
        {
            Play(clip, ref footSpiderSound, volume);
            return;
        }
        if (clip == this._attackSpider)
        {
            Play(clip, ref attackSpiderSound, volume);
            return;
        }
        if (clip == this._deathSpider)
        {
            Play(clip, ref deathSpiderSound, volume);
            return;
        }
        if (clip == this._idleSpider)
        {
            Play(clip, ref idleSpiderSound, volume);
            return;
        }
    }
    public void StopSoundSpider(AudioClip clip)
    {
        if (clip == this._attackSpider)
        {
            attackSpiderSound?.Stop();
            return;
        }
        if (clip == this._deathSpider)
        {
            deathSpiderSound?.Stop();
            return;
        }
        if (clip == this._footStepSpider)
        {
            footSpiderSound?.Stop();
            return;
        }
        if (clip == this._idleSpider)
        {
            idleSpiderSound?.Stop();
            return;
        }
    }

}
