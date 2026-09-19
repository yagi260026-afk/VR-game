using UnityEngine;

public class AxeHit : MonoBehaviour
{
    [Header("斧の攻撃スクリプト")]
    public AxeAttack axeAttack;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("斧が当たった：" + other.gameObject.name);

        // 斧を振っていないなら終了
        if (axeAttack != null &&
            !axeAttack.isAttacking)
        {
            return;
        }


        // =========================
        // 鉱石
        // =========================

        Ore ore = other.GetComponent<Ore>();

        if (ore == null)
        {
            ore = other.GetComponentInParent<Ore>();
        }

        if (ore != null)
        {
            Debug.Log("★★ 鉱石を発見！ ★★");

            if (axeAttack != null &&
                axeAttack.hasHitOre)
            {
                return;
            }

            if (axeAttack != null)
            {
                axeAttack.hasHitOre = true;
            }

            ore.HitOre();

            return;
        }


        // =========================
        // 木
        // =========================

        TreeFall tree = other.GetComponent<TreeFall>();

        if (tree == null)
        {
            tree = other.GetComponentInParent<TreeFall>();
        }

        if (tree != null)
        {
            Debug.Log("★★ 木を発見！ ★★");

            if (axeAttack != null &&
                axeAttack.hasHitTree)
            {
                return;
            }

            if (axeAttack != null)
            {
                axeAttack.hasHitTree = true;
            }

            tree.HitTree();

            return;
        }
    }
}