using UnityEngine;

public class Ore : MonoBehaviour
{
    [Header("鉱石設定")]
    public int hitPoints = 3;

    [Header("ドロップ設定")]
    public GameObject elementPrefab;

    public int dropAmount = 1;

    [Header("ドロップ位置")]
    public Transform dropPoint;

    [Header("エフェクト")]
    public GameObject hitEffect;

    [Header("斧の設定")]
    public string axeTag = "Axe";

    private int currentHP;

    void Start()
    {
        currentHP = hitPoints;
    }

    public void HitOre()
    {
        // =========================
        // アイテムを落とす
        // =========================

        DropElement();

        // =========================
        // HPを減らす
        // =========================

        currentHP--;

        Debug.Log(
            gameObject.name +
            " に斧が当たった！ 残りHP：" +
            currentHP
        );

        // =========================
        // ヒットエフェクト
        // =========================

        PlayHitEffect();

        // =========================
        // HP0
        // =========================

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    // =========================
    // 元素アイテムをドロップ
    // =========================

    void DropElement()
    {
        if (elementPrefab == null)
        {
            Debug.LogWarning(
                "elementPrefabが設定されていません！"
            );

            return;
        }

        Vector3 spawnPosition;

        if (dropPoint != null)
        {
            spawnPosition =
                dropPoint.position;
        }
        else
        {
            spawnPosition =
                transform.position +
                Vector3.up * 0.5f;
        }

        for (
            int i = 0;
            i < dropAmount;
            i++
        )
        {
            Instantiate(
                elementPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }

    // =========================
    // ヒットエフェクト
    // =========================

    void PlayHitEffect()
    {
        if (hitEffect == null)
        {
            return;
        }

        Vector3 effectPosition;

        if (dropPoint != null)
        {
            effectPosition =
                dropPoint.position;
        }
        else
        {
            effectPosition =
                transform.position;
        }

        GameObject effect =
            Instantiate(
                hitEffect,
                effectPosition,
                Quaternion.identity
            );

        Destroy(
            effect,
            3f
        );
    }
}
