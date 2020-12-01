using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Vector3> positions;
    public float speed, timeChange;

    private float startTime, journeyDistance, distCovered, fractionOfJourney;
    private Vector3 startPos, endPos;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartPlatforms();
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                startPos = transform.localPosition;
                endPos = positions[i];
                journeyDistance = Vector3.Distance(startPos, endPos);
                startTime = Time.time;
                fractionOfJourney = 0;
                while (fractionOfJourney <= 1)
                {
                    distCovered = (Time.time - startTime) * speed;
                    fractionOfJourney = distCovered / journeyDistance;
                    transform.localPosition = Vector3.Lerp(startPos, endPos, fractionOfJourney);
                    yield return new WaitForSeconds(timeChange * Time.timeScale);
                }
            }
        }
    }

    public void StartPlatforms()
    {
        StartCoroutine(MovePlatform());
    }


}
