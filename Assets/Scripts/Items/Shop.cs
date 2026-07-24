using Items;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public delegate void ShopOpened();
    public event ShopOpened OnShopOpened;

    public void Interact()
    {
        OnShopOpened?.Invoke();
    }
}
