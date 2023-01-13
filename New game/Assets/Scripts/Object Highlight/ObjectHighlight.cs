using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform rayShootPoint;
    private float rayRange = 5f;
    private RaycastHit rayHit;
    private bool highlightedObjectInRange;

    private bool rayHasHitted;

    public Highlight highlight;

    void Update()
    {
        highlightedObjectInRange = Physics.Raycast(rayShootPoint.position, rayShootPoint.forward, out rayHit, rayRange, layerMask);

        if (highlightedObjectInRange && highlight == null)
        {
            if (rayHit.transform.TryGetComponent(out highlight))
            {
                highlight.ToggleHighlight(true);
            }
            //rayHit.transform.GetComponent<Highlight>()?.ToggleHighlight(true);
            Debug.Log("Ray is colliding with the sword");
        }
       
        if (!highlightedObjectInRange)
        {
            highlight.ToggleHighlight(false);
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        highlight = null;
    }
}
