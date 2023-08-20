using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public Collider[] interestingTargets;
    [SerializeField] Transform sightPoint;
    [SerializeField] float angle = 120;
    [SerializeField] Vector3 extents = new Vector3(10f, 30f, 10f);
    [SerializeField] float refreshFrequency = 10;   // Times per second
    [SerializeField] LayerMask interestingLayers = Physics.DefaultRaycastLayers;
    [SerializeField] LayerMask occludingLayers = Physics.DefaultRaycastLayers;

    private IEnumerator coroutine;

    void Awake()
    {
        coroutine = UpdateSight();
    }

    void Start()
    {
        if (sightPoint == null)
            sightPoint = transform;
    }

    void OnEnable()
    {
        StartCoroutine(coroutine);
    }

    void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator UpdateSight()
    {
        while (true)
        {
            Collider[] collidersInRange =
                Physics.OverlapBox(
                    sightPoint.position + (sightPoint.forward * (extents.z / 2f)),
                    extents / 2f,
                    sightPoint.rotation,
                    interestingLayers);

            List<Collider> interestingTargetsList = new List<Collider>();
            foreach (Collider c in collidersInRange)
            {
                if (
                    (Vector3.Angle(sightPoint.forward, c.transform.position - sightPoint.position) < (angle / 2f)) &&
                    !Physics.Linecast(sightPoint.position, c.transform.position, occludingLayers)
                    )
                {
                    interestingTargetsList.Add(c);
                }
            }

            interestingTargets = interestingTargetsList.ToArray();

            yield return new WaitForSeconds(1f / refreshFrequency);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //if (sightPoint != null)
        //{
        //    Vector3 cubePosition = sightPoint.position + sightPoint.forward * extents.z / 2f;
        //    Gizmos.DrawWireCube(cubePosition, transform.TransformDirection(extents));
        //}
    }
}
