using UnityEngine;

public class ElementGun : MonoBehaviour
{
    [Header("カメラ")]
    public Camera playerCamera;

    [Header("射程")]
    public float range = 50f;


    void Update()
    {
        // 左クリック
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    // =========================
    // 発射
    // =========================

    void Shoot()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player Cameraが設定されていません");
            return;
        }


        // =========================
        // 画面中央からRayを飛ばす
        // =========================

        Ray ray =
            playerCamera.ViewportPointToRay(
                new Vector3(0.5f, 0.5f, 0f)
            );


        // SceneビューでRayを確認
        Debug.DrawRay(
            ray.origin,
            ray.direction * range,
            Color.red,
            2f
        );


        Debug.Log("銃を撃った！");


        RaycastHit hit;


        // =========================
        // Raycast
        // =========================

        if (Physics.Raycast(
            ray,
            out hit,
            range))
        {
            Debug.Log(
                "命中：" +
                hit.collider.gameObject.name
            );


            // =========================
            // ElementSourceを探す
            // =========================

            ElementSource elementSource =
                hit.collider.GetComponentInParent<ElementSource>();


            if (elementSource != null)
            {
                Debug.Log(
                    "ElementSourceを発見！"
                );


                // =========================
                // 元素変換
                // =========================

                ConvertElement(
                    elementSource,
                    hit.point
                );
            }
            else
            {
                Debug.Log(
                    "ElementSourceがありません"
                );
            }
        }
        else
        {
            Debug.Log(
                "何にも当たっていません"
            );
        }
    }


    // =========================
    // 元素変換
    // =========================

    void ConvertElement(
        ElementSource elementSource,
        Vector3 hitPosition
    )
    {
        Debug.Log(
            "元素変換：" +
            elementSource.gameObject.name
        );


        // =========================
        // ElementSourceに
        // ヒット位置を渡す
        // =========================

        elementSource.Convert(
            hitPosition
        );
    }
}