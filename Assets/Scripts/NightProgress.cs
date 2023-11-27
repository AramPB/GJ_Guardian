using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightProgress : MonoBehaviour
{
    [SerializeField] private int clients;
    [SerializeField] private List<GameObject> clientsList;
    [SerializeField] private GameObject clientImage;
    [SerializeField] private GameObject dialogGameObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //NIGHT LOOP
        if (clientsList.Count != 0) {
            for (int i = 1; i <= clients; i++)
            {
                //Llega el cliente
                //clientsList[i - 1].sprite.setActive(true);?

                //Guardia Pide Identificación

                //Cliente - Guardia diálogo

                //Obtención de DNI(o no)

                //Revisar datos

                //Si tiene antecedentes te tiene que dar el justificante penal

                //Decisión(Dejar entrar o no)

                //Dialogo

            }
        }
        else
        {
            Debug.Log("Clients List Empty");
        }
    }
}
