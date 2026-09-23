using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("武器")]
    public GameObject axe;
    public GameObject gun;

    // 0 = 素手, 1 = 斧, 2 = 銃
    private int currentWeapon = 0;

    void Start()
    {
        SetWeapon(0);
    }

    void Update()
    {
        // 左コントローラー Yボタンで武器切り替え
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            currentWeapon++;

            if (currentWeapon > 2)
            {
                currentWeapon = 0;
            }

            SetWeapon(currentWeapon);
        }
    }

    void SetWeapon(int weapon)
    {
        if (axe != null)
        {
            axe.SetActive(false);
        }

        if (gun != null)
        {
            gun.SetActive(false);
        }

        switch (weapon)
        {
            case 0:
                Debug.Log("素手");
                break;

            case 1:
                if (axe != null)
                {
                    axe.SetActive(true);
                }
                Debug.Log("斧");
                break;

            case 2:
                if (gun != null)
                {
                    gun.SetActive(true);
                }
                Debug.Log("銃");
                break;
        }
    }
}