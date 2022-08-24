using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyLimit
{
    public KeyCode key;
    public int times;


    public KeyLimit(KeyCode _key, int _times)
    {
        key = _key;
        times = _times;
    }
}
