using Unity.VisualScripting;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int weaponIndicator;
    public GameObject[] weapons = new GameObject[3];
    void Start()
    {
        switchWeapons(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switchWeapons((weaponIndicator < 2) ? weaponIndicator + 1 : 0);
        }
    }

    public void switchWeapons(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[index].SetActive(true);
        weaponIndicator = index;
    }
}
