using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterSwitcher : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] CharacterData knight;
    [SerializeField] CharacterData dragon;

    PlayerController playerController;
    void OnEnable()
    {

        dragon.enabled = false;
        GetComponent<Animator>().runtimeAnimatorController = knight.MyController;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchCharacter()
    {
        if (playerController.character == knight)
        {
            playerController.character = dragon;
            playerController.ApplyCharData();
        }
        else
        {
            playerController.character = knight;
            playerController.ApplyCharData();
        }
    }
}
