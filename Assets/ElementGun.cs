using UnityEngine;

public class ElementGun : MonoBehaviour
{
    [Header("射程距離")]
    public float range = 50f;

    void Update()
    {
        // Questの右トリガー（人差し指）を押した瞬間
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 銃の向き（transform.forward）へRayを飛ばす
        Ray ray = new Ray(transform.position, transform.forward);

        // シーンビューで赤色の線を2秒間表示
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 2f);
        Debug.Log("🔫 銃を撃った！");

        RaycastHit hit;

        // Raycastを飛ばす
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("🎯 命中したオブジェクト：" + hit.collider.gameObject.name);

            // 当たったオブジェクト、またはその親から ElementSource を探す
            ElementSource elementSource = hit.collider.GetComponentInParent<ElementSource>();

            if (elementSource != null)
            {
                Debug.Log("✨ ElementSourceを発見！元素変換を実行します！");
                elementSource.Convert(hit.point);
            }
            else
            {
                Debug.Log("⚠️ 当たりましたが、ElementSourceコンポーネントがありません");
            }
        }
        else
        {
            Debug.Log("💨 何にも当たっていません");
        }
    }
}