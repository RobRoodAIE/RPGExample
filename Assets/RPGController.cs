using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RPGController : MonoBehaviour
{
    public static RPGController Instance;
    [SerializeField] PlayerHud _hud;

    

    private int xp;
    public int Xp { get { return xp; }  set { xp = value; if (_hud) _hud.UpdateXp(xp); } } 

    private int gold;
    public int Gold { get { return gold; } set { gold = value; if (_hud) _hud.UpdateGold(gold); } }

    private float health;
    public float Health { get { return health; } set { health = value; if (_hud) _hud.UpdateHealth(health); } }

    bool isInvincible;

    void Awake()
    {

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

        
    }

    private void Start()
    {
        BeginGame();
    }

    void BeginGame()
    {
        Health = 100f;
        Gold = 5;
        Xp = 0;
    }


    public void AddXP() {
        Xp += 10;
    }

    public void GodMode() {
        isInvincible = true;
    }

    public void AddGold(int ammount) {
        Gold += ammount;
    }

    public void SetGold(int ammount) {
        Gold = ammount;
    }



}
