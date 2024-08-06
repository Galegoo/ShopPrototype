using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ItemIcons : MonoBehaviour
{
    public Button itemButton;
    [SerializeField] protected GameObject select;
    public static Color colorstorage;
    public static string gameTag;

    [SerializeField] protected TMP_Text priceText;
    [SerializeField] protected TMP_Text descriptionText;
    [SerializeField] protected TMP_Text nameText;
    [SerializeField] protected GameObject warningText;
    [SerializeField] protected TMP_Text healthText;
    [SerializeField] protected TMP_Text resText;
    [SerializeField] protected TMP_Text atkText;
    [SerializeField] protected TMP_Text velText;
    [SerializeField] protected Button EquipButton;
    protected Image image;

    public virtual void Initialize(GameObject select, TMP_Text priceText, TMP_Text descriptionText, TMP_Text nameText, GameObject warningText, TMP_Text helthText, TMP_Text resText, TMP_Text atkText, TMP_Text velText, Button equipButton = null)
    {
        image = GetComponent<Image>();
        

        this.select = select;
        if (priceText != null)
            this.priceText = priceText;
        if (descriptionText != null)
            this.descriptionText = descriptionText;
        if (nameText != null)
            this.nameText = nameText;
        if (warningText != null)
            this.warningText = warningText;
        if (helthText != null)
            this.healthText = helthText;
        if (resText != null)
            this.resText = resText;
        if (atkText != null)
            this.atkText = atkText;
        if (velText != null)
            this.velText = velText;
        if (equipButton != null)
            this.EquipButton = equipButton;
    }
    protected virtual void Start()
    {
        image = GetComponent<Image>();
        itemButton = GetComponent<Button>();
        itemButton.onClick.AddListener(Highlight);
    }

    protected virtual void Highlight()
    {
        NPCUIController.SelectedIcon = gameObject;
        select.transform.position = transform.position;
        colorstorage = image.color;
        gameTag = gameObject.tag;

        if (warningText != null)
        {
            warningText.SetActive(false);
        }

        descriptionText.text = GetDescription();
        nameText.text = GetName();
        UpdateUI();
    }

    protected abstract string GetDescription();
    public abstract string GetName();

    public abstract int GetPrice();
    protected abstract void UpdateUI();
}