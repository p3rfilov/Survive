using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUIController : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;
    public Text clipText;
    public PlayerController playerController;

    private Health health;
    private ItemHolder itemHolder;


    private void Start ()
    {
        health = playerController.transform.GetComponent<Health>();
        itemHolder = playerController.transform.GetComponent<ItemHolder>();
        EventManager.onPlayerHealthChanged += DisplayHealth;
        EventManager.onItemCollected += DisplayAmmo;
        EventManager.onPlayerCurrentItemChanged += DisplayAmmo;
        DisplayHealth();
        DisplayAmmo();
    }

    private void DisplayHealth ()
    {
        if (health != null)
        {
            healthText.text = health.health.ToString();
        }
    }

    private void DisplayAmmo ()
    {
        if (itemHolder != null)
        {
            Ammo ammoComponent = itemHolder.GetItem()?.GetComponent<Ammo>();
            if (ammoComponent != null)
            {
                ammoText.text = ammoComponent.ammo.ToString();
                clipText.text = ammoComponent.clip.ToString();
            }
            else
            {
                ammoText.text = "0";
                clipText.text = "0";
            }
            
        }
    }
}
