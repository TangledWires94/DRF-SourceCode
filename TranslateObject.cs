using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateObject : MonoBehaviour
{
    private float startTime, journeyDistance, distCovered, fractionOfJourney;
    public Vector3 startPosition, endPosition;
    public float speed, timeChange;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartTranslation(bool direction)
    {
        StopAllCoroutines();
        startTime = Time.time;
        if (direction)
        {
            journeyDistance = Vector3.Distance(startPosition, endPosition);
            StartCoroutine(ObjectTranslation(startPosition, endPosition));
        } else
        {
            journeyDistance = Vector3.Distance(endPosition, startPosition);
            StartCoroutine(ObjectTranslation(endPosition, startPosition));
        }
    }

    IEnumerator ObjectTranslation(Vector3 position1, Vector3 position2)
    {
        fractionOfJourney = 0;
        while(fractionOfJourney <= 1)
        {
            distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyDistance;
            gameObject.transform.localPosition = Vector3.Lerp(position1, position2, fractionOfJourney);
            yield return new WaitForSeconds(timeChange * Time.timeScale);
        }
    }
}
