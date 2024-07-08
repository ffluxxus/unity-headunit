using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private ModalWindow modalWindowPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public ModalWindow CreateModal()
    {
        ModalWindow modalWindow = Instantiate(modalWindowPrefab, transform);

        modalWindow.gameObject.SetActive(true);

        return modalWindow;
    }
}