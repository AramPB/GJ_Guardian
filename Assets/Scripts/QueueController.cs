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

    [SerializeField] private List<GameObject> points;
    [SerializeField] private GameObject customerContainer;
    [SerializeField] private List<GameObject> customers;

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
    }

    public void AcceptCustomer()
    {
        keeper.sprite = keeperSprite2;
        fence.sprite = fenceSprite2;
    }

    public void DeclineCustomer()
    {
        keeper.sprite = keeperSprite;
        fence.sprite = fenceSprite;
    }

}
