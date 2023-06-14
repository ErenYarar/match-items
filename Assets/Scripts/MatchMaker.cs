using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMaker : MonoBehaviour
{
    public List<GameObject> PlacedObject = new List<GameObject>();
    public GameObject PointA;
    public Animator anim;
    private IEnumerator coroutine;
    public GameObject winPanel;
    public GameObject confetti_PS;
    //
    [SerializeField] private AudioClip _trueObjectMatchClip;
    [SerializeField] private AudioClip _falseObjectMatchClip;
    [SerializeField] private AudioClip _winPanelOpenedClip;

    private void OnTriggerEnter(Collider other)
    {
        if (PlacedObject.Count == 0)
        {
            other.gameObject.transform.position = PointA.transform.position;
            other.gameObject.transform.rotation = PointA.transform.rotation;
            PlacedObject.Add(other.gameObject);
        }
        else if (other.gameObject.transform.name.Contains(PlacedObject[0].name) == true)
        {
            SoundManager.Instance.PlaySound(_trueObjectMatchClip);
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            PlacedObject[0].gameObject.GetComponent<MeshCollider>().enabled = false;
            coroutine = WaitAndPrint(0.25f);
            StartCoroutine(coroutine);
            AlignObjects(other.gameObject, PlacedObject[0].gameObject);
            Destroy(other.gameObject, 1f);
            Destroy(PlacedObject[0].gameObject, 1f);
            PlacedObject.Clear();
            if (VibrationsManager.Instance.OnValueChanged) // Araştır
            {
                Handheld.Vibrate();
            }
        }
        else
        {
            SoundManager.Instance.PlaySound(_falseObjectMatchClip);
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 1) * 120 * Time.deltaTime;
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        anim.SetBool("isFinishActive", true);
        GoldManager.instance.GoldObj.SetActive(true);
        GoldManager.instance.GoldAnim.SetBool("isMoved", true);
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("isFinishActive", false);
        yield return new WaitForSeconds(.75f);
        FoodTimer();
        GoldManager.instance.AddGold(1);
        GoldManager.instance.GoldObj.SetActive(false);
        GoldManager.instance.GoldAnim.SetBool("isMoved", false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlacedObject.Contains(other.gameObject))
        {
            PlacedObject.Remove(other.gameObject);
        }
    }

    public void AlignObjects(GameObject obj1, GameObject obj2)
    {
        // objelerin pozisyonlarını ayrı değişkenlere atayalım
        Vector3 pos1 = obj1.transform.position;
        Vector3 pos2 = obj2.transform.position;

        // objelerin pozisyonlarını karşılaştıralım ve aynı pozisyona getirelim
        if (pos1 != pos2)
        {
            obj1.transform.position = pos2;
        }
    }

    private void FoodTimer()
    {
        if (GameObject.FindGameObjectsWithTag("Food").Length == 0)
        {
            StartCoroutine(WaitForWin());
        }
    }

    IEnumerator WaitForWin()
    {
        SoundManager.Instance.PlaySound(_winPanelOpenedClip);
        confetti_PS.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        winPanel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        confetti_PS.SetActive(false);
    }
}
