using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    AudioSource audioSource;
    public AudioClip rolloverSound;
    public AudioClip clickSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.clip = rolloverSound;
        audioSource.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.clip = clickSound;
        audioSource.Play();

        if(gameObject.name == "Newgame")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SpaceEscape");
        }
    }
}
