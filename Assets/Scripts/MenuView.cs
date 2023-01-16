using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public event EventHandler PlayClicked;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Play()
        => OnPlayClicked(EventArgs.Empty);

    private void OnPlayClicked(EventArgs eventArgs)
    {
        var handler = PlayClicked;
        handler?.Invoke(this, eventArgs);

        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
