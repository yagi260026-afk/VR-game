using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("武器")]
    public GameObject axe;
    public GameObject gun;

    // 0 = 素手
    // 1 = 斧
    // 2 = 銃
    private int currentWeapon = 0;

    void Start()
    {
        SetWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
        // まず全部OFF
        if (axe != null)
        {
            axe.SetActive(false);
        }

        if (gun != null)
        {
            gun.SetActive(false);
        }

        // 選択された武器だけON
        switch (weapon)
        {
            case 0:
                // 素手
                Debug.Log("素手");
                break;

            case 1:
                // 斧
                if (axe != null)
                {
                    axe.SetActive(true);
                }

                Debug.Log("斧");
                break;

            case 2:
                // 銃
                if (gun != null)
                {
                    gun.SetActive(true);
                }

                Debug.Log("銃");
                break;
        }
    }
}