using UnityEngine;

public class WoodPickup : MonoBehaviour
{
    public Camera cam;

    public Transform holdPoint;

    public float grabDistance = 3f;

    private GameObject heldObject;

    private Rigidbody heldRB;

    private Collider heldCollider;

    private AxeToggle axeToggle;


    void Start()
    {
        axeToggle =
            GetComponent<AxeToggle>();
    }


    void Update()
    {
        // =========================
        // 斧装備中
        // =========================

        if (
            axeToggle != null &&
            axeToggle.axeEquipped
        )
        {
            return;
        }


        // =========================
        // 左クリック
        // =========================

        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                PickUp();
            }
            else
            {
                Drop();
            }
        }


        // =========================
        // 持っている物を追従
        // =========================

        if (heldObject != null)
        {
            heldObject.transform.position =
                holdPoint.position;

            heldObject.transform.rotation =
                holdPoint.rotation;
        }
    }


    // =========================
    // 拾う
    // =========================

    void PickUp()
    {
        Ray ray =
            cam.ScreenPointToRay(
                Input.mousePosition
            );

        if (
            Physics.Raycast(
                ray,
                out RaycastHit hit,
                grabDistance
            )
        )
        {
            if (
                hit.collider.CompareTag(
                    "Wood"
                )
            )
            {
                heldObject =
                    hit.collider.gameObject;

                heldRB =
                    heldObject.GetComponent<Rigidbody>();

                heldCollider =
                    heldObject.GetComponent<Collider>();


                if (heldRB != null)
                {
                    heldRB.isKinematic =
                        true;
                }


                if (heldCollider != null)
                {
                    heldCollider.enabled =
                        false;
                }
            }
        }
    }


    // =========================
    // 落とす
    // =========================

    void Drop()
    {
        if (heldRB != null)
        {
            heldRB.isKinematic =
                false;
        }

        if (heldCollider != null)
        {
            heldCollider.enabled =
                true;
        }

        heldObject = null;

        heldRB = null;

        heldCollider = null;
    }
}