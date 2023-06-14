using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHelper : MonoBehaviour
{
    private static HandHelper instance;

    public GameObject _handHelper; 

    public static HandHelper Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
}
