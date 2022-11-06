using Assets.Scripts.Protagonist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    [SerializeField] GameObject shelfA;
    [SerializeField] GameObject shelfB;
    [SerializeField] GameObject shelfC;
    [SerializeField] float movementSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MoveShelf()
    {
        if (Input.GetKey(KeyCode.A))
        {
            shelfA.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.B))
        {
            shelfB.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.C))
        {
            shelfC.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.X))
        {
            shelfA.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
            shelfB.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
            shelfC.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);

        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveShelf();
    }
}
