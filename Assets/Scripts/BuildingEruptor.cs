using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingEruptor : MonoBehaviour
{
    public GameObject[] buildingSections; // Assign your building sections here
    public float totalAnimationTime = 2.0f; // Total time to animate the entire complex
    private float delayBetweenBatches;

    void Start()
    {
        foreach (GameObject obj in buildingSections)
        {
            obj.SetActive(false);
        }

        delayBetweenBatches = totalAnimationTime / buildingSections.Length;
        StartCoroutine(AnimateBuildingEruption());
    }

    IEnumerator AnimateBuildingEruption()
    {
        yield return new WaitForSeconds(0.5f);

        List<GameObject> remainingSections = new List<GameObject>(buildingSections);
        while (remainingSections.Count > 0)
        {
            int numberOfSectionsToActivate = Random.Range(1, Mathf.Min(5, remainingSections.Count) + 1);
            List<GameObject> currentBatch = new List<GameObject>();

            for (int i = 0; i < numberOfSectionsToActivate; i++)
            {
                int index = Random.Range(0, remainingSections.Count);
                GameObject section = remainingSections[index];
                remainingSections.RemoveAt(index);
                currentBatch.Add(section);
                section.SetActive(true);
                StartCoroutine(AnimateSection(section));
            }

            yield return new WaitForSeconds(delayBetweenBatches);
        }
    }

    IEnumerator AnimateSection(GameObject section)
    {
        Vector3 originalScale = section.transform.localScale;
        section.transform.localScale = Vector3.zero;

        float timer = 0;
        while (timer < delayBetweenBatches)
        {
            float progress = timer / delayBetweenBatches;
            section.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        section.transform.localScale = originalScale;
    }
}