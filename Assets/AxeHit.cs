using UnityEngine;

public class AxeHit : MonoBehaviour
{
    [Header("連続ヒット防止の時間（秒）")]
    public float hitCooldown = 0.5f;
    private float lastHitTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        // 最後に当たってから、まだクールダウン（0.5秒）経過していなければ終了
        // （VRで木にめり込んだ時に、1秒間に60回ダメージが入ってしまうのを防ぐため）
        if (Time.time - lastHitTime < hitCooldown)
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
            ore.HitOre();

            // ヒットした時間を記録
            lastHitTime = Time.time;
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
            tree.HitTree();

            // ヒットした時間を記録
            lastHitTime = Time.time;
            return;
        }
    }
}