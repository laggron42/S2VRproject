using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Sword,
    Hammer,
    None
};

public class StateManager : MonoBehaviour
{
    [Header("State")]
    public Weapon weapon = Weapon.None;
    Weapon lastWeapon = Weapon.None;

    public bool attack = false;
    public bool die = false;

    [Header("Necessities")]
    public Transform hand;

    public GameObject hammerPrefab;
    public GameObject swordPrefab;

    bool withSword = false;
    bool withHammer = false;

    Animator anim;
    GameObject weaponPrefab;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon != lastWeapon)
            OnWeaponChange();
        if (die)
            OnDeath();
    }

    void FixedUpdate()
    {
        anim.SetBool("Attack", attack);
    }

    void OnWeaponChange()
    {
        withSword = weapon == Weapon.Sword;
        withHammer = weapon == Weapon.Hammer;

        anim.SetBool("GrabSword", withSword);
        anim.SetBool("GrabHammer", withHammer);

        Destroy(weaponPrefab);
        switch (weapon)
        {
            case Weapon.Hammer:
                weaponPrefab = Instantiate(hammerPrefab, hand, true);
                weaponPrefab.transform.localPosition = new Vector3(0, 0.003f, 0);
                weaponPrefab.transform.localRotation = Quaternion.Euler(-96, -90, 90);
                break;
            case Weapon.Sword:
                weaponPrefab = Instantiate(swordPrefab, hand, true);
                weaponPrefab.transform.localPosition = new Vector3(0, 0.002f, 0);
                weaponPrefab.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                break;
        }

        lastWeapon = weapon;
    }

    void OnDeath()
    {
        anim.SetBool("GrabHammer", false);
        anim.SetBool("GrabSword", false);
        anim.SetTrigger("Die");
        anim.SetBool("Attack", false);
        die = false;
    }
}
