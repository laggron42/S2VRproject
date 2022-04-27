using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    [Header("Prefab")]

    public GameObject Enemy;

    public Text Round;

    public List<GameObject> spawnPoints = new List<GameObject>();

    public AudioSource horn;

    [Header("First Wave Enemies numbers")]

    public int noWeapons = 0;
    public int sword = 0;
    public int hammer = 0;
    public float waveRate = 20f;

    private int len = 0;
    private int x = 1;
    private float timeLeft = 0;
    StateManager st;

    void Start()
    {
        len = spawnPoints.Count;
        st = Enemy.GetComponent<StateManager>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            horn.Play();
            if (x > 19) noWeapons += 2;
            else noWeapons += 1;
            if (x % 3 == 0) sword += 1;
            if (x % 5 == 0) hammer += 1;


            st.weapon = WeaponType.None;
            Enemy.GetComponent<StatsPotato>().health = 1;
            Enemy.GetComponent<StatsPotato>().attackPower = 1;
            for (int i = 0; i < noWeapons; i++)
            {
                Instantiate(Enemy, spawnPoints[Random.Range(0, len)].transform);
            }


            st.weapon = WeaponType.Sword;
            Enemy.GetComponent<StatsPotato>().health = 2;
            Enemy.GetComponent<StatsPotato>().attackPower = 3;
            for (int i = 0; i < sword; i++)
            {
                Instantiate(Enemy, spawnPoints[Random.Range(0, len)].transform);
            }

            st.weapon = WeaponType.Hammer;
            Enemy.GetComponent<StatsPotato>().health = 3;
            Enemy.GetComponent<StatsPotato>().attackPower = 6;
            for (int i = 0; i < hammer; i++)
            {
                Instantiate(Enemy, spawnPoints[Random.Range(0, len)].transform);
            }

            Round.text = "Round : " + x.ToString();
            x += 1;
            timeLeft = waveRate;
        }
    }

}