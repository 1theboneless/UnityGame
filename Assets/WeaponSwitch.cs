using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DisableAllWeapons();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    void DisableAllWeapons()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
