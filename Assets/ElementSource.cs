using UnityEngine;

public class ElementSource : MonoBehaviour
{
    [Header("このアイテムを撃ったときに出現する元素")]
    public GameObject elementPrefab;


    [Header("変換エフェクト")]
    public GameObject conversionEffect;


    [Header("変換時に消えるか")]
    public bool destroyAfterConversion = true;


    public void Convert(Vector3 hitPosition)
    {
        // =========================
        // 変換エフェクト
        // =========================

        if (conversionEffect != null)
        {
            Instantiate(
                conversionEffect,
                hitPosition,
                Quaternion.identity
            );
        }


        // =========================
        // 元素を生成
        // =========================

        Vector3 position = transform.position;

        if (elementPrefab != null)
        {
            Instantiate(
                elementPrefab,
                position,
                Quaternion.identity
            );
        }


        // =========================
        // 元のアイテムを消す
        // =========================

        if (destroyAfterConversion)
        {
            Destroy(gameObject);
        }
    }
}