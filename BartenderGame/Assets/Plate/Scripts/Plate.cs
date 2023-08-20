using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConsumableContainer))]
public class Plate : WeaponBase
{
    [SerializeField] float interactionDistance = 3;
    [SerializeField] Transform[] containerDropPoints;
    [SerializeField] Transform dropPoint;

    private ConsumableContainer objectContainer;
    private Vector3 screenMidPoint;
    private bool canAtack = true;
    private bool canSAtack = true;

    void Awake()
    {
        objectContainer = GetComponent<ConsumableContainer>();
    }

    void Start()
    {
        screenMidPoint = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    public override void Atack()
    {
        if (canAtack)
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(screenMidPoint), out hit, interactionDistance);

            ITakeable takeable = hit.transform?.GetComponent<ITakeable>();

            if (takeable != null)
            {
                objectContainer.PutInObject(
                    takeable.TakeObject()
                    );
            }

            canAtack = false;
        }
    }

    public override void ReleaseAtack()
    {
        canAtack = true;
    }

    public override void SecondaryAtack()
    {
        if (canSAtack)
        {
            ConsumableObject objectToDrop = objectContainer.TakeObject();

            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(screenMidPoint), out hit, interactionDistance);

            IContainer container = hit.transform?.GetComponent<IContainer>();

            if (container != null)
            {
                if (!container.PutInObject(objectToDrop))
                    objectToDrop?.Dropped(dropPoint);
            }
            else
                objectToDrop?.Dropped(dropPoint);

            canSAtack = false;
        }
    }

    public override void ReleaseSecondaryAtack()
    {
        canSAtack = true;
    }
}
