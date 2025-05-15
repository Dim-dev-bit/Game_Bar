using System;
using UnityEngine;
using UnityEngine.UI;

public class FruitButton : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Text nameText;
    [SerializeField] private Text countText;

    [Header("Settings")]
    [SerializeField] private int minHeight = 60;

    private GameObject fruitPrefab;
    private FruitMenu fruitMenu;

    public void Initialize(GameObject fruitPrefab, int count)
    {
        nameText.text = fruitPrefab.name.Replace("(Clone)", "");
        GetComponent<LayoutElement>().minHeight = minHeight;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void UpdateCount(int count)
    {
        countText.text = $"x{count}";
        GetComponent<Button>().interactable = count > 0;
    }

    private void OnClick()
    {
        // Теперь кнопка явно вызывает SelectFruit у FruitMenu
        fruitMenu.SelectFruit(fruitPrefab);
    }
}