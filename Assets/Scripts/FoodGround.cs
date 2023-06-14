using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGround : MonoBehaviour
{
    [SerializeField] private GameObject EffectonS;

    private void Start()
    {
        StartCoroutine(WaitForImage());
    }

    IEnumerator WaitForImage()
    {
        yield return new WaitForSeconds(.2f);
        EffectonS.SetActive(true);
        yield return new WaitForSeconds(.4f);
        EffectonS.SetActive(false);
    }
}
