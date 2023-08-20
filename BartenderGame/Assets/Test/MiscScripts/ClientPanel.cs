using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientPanel : MonoBehaviour
{
    [SerializeField] Client client;
    [SerializeField] GameObject orderPanelPrefab;
    [SerializeField] Gradient progressionGradient;
    [SerializeField] Image counterImage;
    [SerializeField] PlayerCloseness playerCloseness;


    void OnEnable()
    {
        client.OnNewOrder += SetNewOrderPanel;
    }

    void Update()
    {
        if (client.ActualOrder != null)
        {
            counterImage.enabled = true;
            counterImage.color = progressionGradient.Evaluate(GetCounterProgression());

            if (playerCloseness.player != null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else
            counterImage.enabled = false;
    }

    void OnDisable()
    {
        client.OnNewOrder += SetNewOrderPanel;
    }

    private void SetNewOrderPanel(Order order)
    {
        order.OnOrderStateChange += ResetPanels;

        List<ConsumableType> types = new List<ConsumableType>();

        if (order.Objects?[0] != null)
            types.Add(order.Objects[0]);

        for(int i = 0; i < order.Objects.Length; i++)
        {
            bool add = false;

            for(int e = 0; e < types.Count; e++)
            {
                add = true;

                if (order.Objects[i].name == types[e].name)
                {
                    add = false;
                    break;
                }
            }

            if (add)
                types.Add(order.Objects[i]);
        }

        int[] typesCuantity = new int[types.Count];

        for(int i = 0; i < types.Count; i++)
        {
            for(int e = 0; e < order.Objects.Length; e++)
            {
                if (types[i].name == order.Objects[e].name)
                    typesCuantity[i]++;
            }
        }

        for(int i = 0; i < types.Count; i++)
        {
            OrderPanel actualPanel = Instantiate(orderPanelPrefab, transform).GetComponent<OrderPanel>();

            actualPanel.text.text = typesCuantity[i].ToString();
            actualPanel.image.sprite = types[i].Image;
        }
    }

    private void ResetPanels(OrderState state)
    {
        client.ActualOrder.OnOrderStateChange += ResetPanels;

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private float GetCounterProgression()
    {
        float t = 0;

        t = Mathf.InverseLerp(0, client.ActualOrder.Counter.TimeToComplete, client.ActualOrder.Counter.TimeLeft);

        return t;
    }
}
