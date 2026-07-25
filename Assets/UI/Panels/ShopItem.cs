using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [field: SerializeField]
    public Item Item { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _itemDescription;

    [SerializeField]
    private Color _disabledColor;

    private Image _image;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _itemDescription.text = Item.ItemData.description;
    }

    public void Disable()
    {
        _image.color = _disabledColor;
    }

    public void Enable()
    {
        _image.color = Color.white;
    }
}
