using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    private Image optionImage;

    [SerializeField]
    private Sprite normalSprite;
    [SerializeField]
    private Sprite highlightedSprite;

    public bool IsHovered { get; set; }

    private void Awake()
    {
        optionImage = GetComponent<Image>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsHovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelected(bool selected)
    {
        optionImage.sprite = selected ? highlightedSprite : normalSprite;
    }
}
