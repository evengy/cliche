using Assets.Scripts.Drawer;
using Assets.Scripts.Interactive;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    [SerializeField] GameObject shelfA;
    [SerializeField] GameObject shelfB;
    [SerializeField] GameObject shelfC;

    [SerializeField] float movementSpeed = 2f;
    [SerializeField] float shelfSize= 0.8f;

    DrawerState state;
    // Start is called before the first frame update
    void Start()
    {
        state = DrawerState.Closed;
    }

    void OpenShelf(GameObject shelf)
    {
        var current = shelf.transform.localPosition;
        var towards = new Vector3(current.x, current.y, current.z - shelfSize);
        if (current.z > -shelfSize)
        {
            shelf.transform.localPosition = Vector3.MoveTowards(current, towards, Time.deltaTime * movementSpeed);
        }
        var pin = shelf.GetComponentInChildren<Pin>();
        if (pin != null)
        {
            if (!pin.Opened)
            {
                Debug.Log($"{pin.name} Opened");
            }
            pin.Opened = true;
        }
    }

    void CloseShelf(GameObject shelf)
    {
        var current = shelf.transform.localPosition;
        var towards = new Vector3(current.x, current.y, 0);
        if (current.z <= 0)
        {
            shelf.transform.localPosition = Vector3.MoveTowards(current, towards, Time.deltaTime * movementSpeed);
        }
    }

    void UpdateState()
    {
        if (GetComponent<UIInteractions>().IsInteractive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                state = DrawerState.ShelfA;
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                state = DrawerState.ShelfB;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                state = DrawerState.ShelfC;
            }
        }
    }

    void UpdateShelvesPositions()
    {
        if (state.Equals(DrawerState.ShelfA))
        {
            CloseShelf(shelfB);
            CloseShelf(shelfC);

            OpenShelf(shelfA);
        }

        if (state.Equals(DrawerState.ShelfB))
        {
            CloseShelf(shelfA);
            CloseShelf(shelfC);

            OpenShelf(shelfB);
        }

        if (state.Equals(DrawerState.ShelfC))
        {
            CloseShelf(shelfA);
            CloseShelf(shelfB);

            OpenShelf(shelfC);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateShelvesPositions();
    }
}
