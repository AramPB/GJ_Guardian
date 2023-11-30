using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueController : MonoBehaviour
{
    [SerializeField] private Image keeper;
    [SerializeField] private Image fence;

    [SerializeField] private Sprite keeperSprite;
    [SerializeField] private Sprite keeperSprite2;
    [SerializeField] private Sprite fenceSprite;
    [SerializeField] private Sprite fenceSprite2;

    [SerializeField] private List<Transform> points;
    [SerializeField] private GameObject customerContainer;
    [SerializeField] private List<GameObject> customers;
    [SerializeField] private GameObject customerGO;

    public static QueueController Instance { get; private set; }

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ResetAll();
    }

    public void ResetAll()
    {
        keeper.sprite = keeperSprite;
        fence.sprite = fenceSprite;

        foreach (GameObject customer in customers)
        {
            Destroy(customer);
        }
        customers.Clear();
    }

    public void ResetCLient()
    {
        keeper.sprite = keeperSprite;
        fence.sprite = fenceSprite;
    }

    public void NewQueue(int numCustomers)
    {
        for (int i = 0; i < numCustomers; i++) {
            GameObject aux = Instantiate(customerGO, customerContainer.transform);
            customers.Add(aux);
            customers[i].transform.position = points[i + 1].position;
        }
    }

    public void AcceptCustomer(int position, float time)
    {
        keeper.sprite = keeperSprite2;
        fence.sprite = fenceSprite2;
        for (int i = position; i < customers.Count; i++)
        {
            customers[i].SetActive(true);
        }
        customers[position].GetComponent<CustomerQueueMovement>().ImFirst(true);
        customers[position].GetComponent<CustomerQueueMovement>().MoveAccept(points[1], points[0], time);
        for (int i = position + 1; i < customers.Count; i++)
        {
            customers[i].GetComponent<CustomerQueueMovement>().ImFirst(false);
            customers[i].GetComponent<CustomerQueueMovement>().MoveAccept(points[i - position + 1], points[i - position], time);
        }

    }

    public void DeclineCustomer(int position, float time)
    {
        keeper.sprite = keeperSprite;
        fence.sprite = fenceSprite;
        for (int i = position; i < customers.Count; i++)
        {
            customers[i].SetActive(true);
        }
        customers[position].GetComponent<CustomerQueueMovement>().ImFirst(true);
        customers[position].GetComponent<CustomerQueueMovement>().MoveDecline(points[1], points[6], time);
        for (int i = position + 1; i < customers.Count; i++)
        {
            customers[i].GetComponent<CustomerQueueMovement>().ImFirst(false);
            customers[i].GetComponent<CustomerQueueMovement>().MoveAccept(points[i - position + 1], points[i - position], time);
        }
    }
    public void StartMove()
    {
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<CustomerQueueMovement>().StartMove();
        }
    }
}
