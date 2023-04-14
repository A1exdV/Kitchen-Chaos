using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using JetBrains.Annotations;
using Player;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set;}
    
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private Camera _camera;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerInteractions.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        _camera = Camera.main;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        var trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        var baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup,PlayerInteractions.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop,cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        var deliveryCounter = sender as GameObject;
        PlaySound(audioClipRefsSO.deliveryFail,deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        var deliveryCounter = sender as GameObject;
        PlaySound(audioClipRefsSO.deliverySuccess,deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
            PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipRefsSO.footstep[Random.Range(0,audioClipRefsSO.footstep.Length)],position,volume);
    }
}
