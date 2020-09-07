using UnityEngine;
using UnityEngine.UI;

public class PlayerGUIController : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;
    public Text clipText;
    public Text relodingMessage;

    public Image leftIcon;
    public Image centerIcon;
    public Image rightIcon;
    public Sprite emptySprite;

    public PlayerController playerController;

    private Health health;
    private ItemHolder itemHolder;
    private Inventory inventory;


    private void Start ()
    {
        relodingMessage.enabled = false;
        health = playerController.transform.GetComponent<Health>();
        itemHolder = playerController.transform.GetComponent<ItemHolder>();
        inventory = playerController.transform.GetComponent<Inventory>();
        
        EventManager.OnPlayerHealthChanged += DisplayHealth;
        EventManager.OnItemCollected += DisplayAmmo;
        EventManager.OnItemCollected += DisplayInventory;
        EventManager.OnPlayerCurrentItemChanged += DisplayAmmo;
        EventManager.OnPlayerCurrentItemChanged += DisplayInventory;
        EventManager.OnPlayerWeaponReloadingStateChanged += DisplayReloadingMessage;

        DisplayHealth();
        DisplayAmmo();
        DisplayInventory();
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

    private void DisplayInventory ()
    {
        if (inventory != null)
        {
            Sprite current = inventory.GetItem(itemHolder.Index)?.icon;
            Sprite next = inventory.GetItem(itemHolder.Index + 1)?.icon;
            Sprite previous = inventory.GetItem(itemHolder.Index - 1)?.icon;

            Sprite[] sprites = { previous, current, next };
            Image[] icons = { leftIcon, centerIcon, rightIcon };

            for (int i = 0; i < 3; i++)
            {
                var sprite = sprites[i];
                if (sprite != null)
                {
                    icons[i].sprite = sprite;
                }
                else
                {
                    icons[i].sprite = emptySprite;
                }
            }
        }
    }

    private void DisplayReloadingMessage (bool reloading)
    {
        if (relodingMessage != null)
        {
            relodingMessage.enabled = reloading;
        }
    }
}
