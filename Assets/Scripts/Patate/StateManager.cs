using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Hammer,
    None
};

public class StateManager : MonoBehaviour
{
    [Header("State")]
    public WeaponType weapon = WeaponType.None;

    WeaponType current = WeaponType.None;

    [Header("Necessities")]
    public Transform hand;

    public GameObject hammerPrefab;
    public GameObject swordPrefab;

    Animator anim;
    GameObject weaponInstance;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (current != weapon)
            ChangeWeapon(weapon);
    }

    public void StartAttack()
    {
        anim.SetBool("Attack", true);
    }

    public void StopAttack()
    {
        anim.SetBool("Attack", false);
    }

    public void ChangeWeapon(WeaponType weapon)
    {
        anim.SetBool("GrabSword", weapon == WeaponType.Sword);
        anim.SetBool("GrabHammer", weapon == WeaponType.Hammer);

        Destroy(weaponInstance);
        switch (weapon)
        {
            case WeaponType.Hammer:
                weaponInstance = Instantiate(hammerPrefab, hand, true);
                weaponInstance.transform.localPosition = new Vector3(0, 0.003f, 0);
                weaponInstance.transform.localRotation = Quaternion.Euler(-96, -90, 90);
                break;
            case WeaponType.Sword:
                weaponInstance = Instantiate(swordPrefab, hand, true);
                weaponInstance.transform.localPosition = new Vector3(0, 0.002f, 0);
                weaponInstance.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                break;
        }

        current = weapon;
    }

    public void Die()
    {
        anim.SetBool("GrabHammer", false);
        anim.SetBool("GrabSword", false);
        anim.SetTrigger("Die");
        anim.SetBool("Attack", false);
    }
}
