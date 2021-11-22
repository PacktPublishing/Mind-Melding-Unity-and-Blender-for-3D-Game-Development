using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dais : MonoBehaviour
{
    public GameObject finalFX;
    GameObject marine;



  

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        finalFX.gameObject.SetActive(true);

        var player = other.gameObject;
        var scaleTo = new Vector3(.1f, .1f, .1f);
        StartCoroutine(ScaleOverSeconds(player, scaleTo, 1.0f));

    }

    public IEnumerator ScaleOverSeconds(GameObject objectToScale, Vector3 scaleTo, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;

        while(elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToScale.transform.position = scaleTo;

        Marine marine = objectToScale.gameObject.GetComponent<Marine>();
        marine.DoDying();
    }
}
