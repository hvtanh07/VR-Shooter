using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public Player player;
    public SumMoney SumMoney;
    public PlayerAttack GunDamage;
    public TextMesh Money;
    // Start is called before the first frame update
    void Start()
    {
        Money.text = SumMoney.Money.ToString();
    }

    // Update is called once per frame
    public void PlayerBuyHealth()
    {
        if (SumMoney.Money >= 20)
        {
            SumMoney.Money -= 20;
            player.startingHealth += 20;
        }
    }
    public void PlayerBuySpeed()
    {
        if (SumMoney.Money >= 20)
        {
            SumMoney.Money -= 20;
            player.playerSpeed += 2f;
        }
    }
    public void PlayerBuyAttack()
    {
        if (SumMoney.Money >= 20)
        {
            SumMoney.Money -= 20;
            GunDamage.gunDamage += 3;
        }
    }
    public void PlayerBuyBulletRange()
    {
        if (SumMoney.Money >= 10)
        {
            SumMoney.Money -= 10;
            GunDamage.shootingRange += 10;
        }
    }
    void Update()
    {
        Money.text = SumMoney.Money.ToString();
    }
}
