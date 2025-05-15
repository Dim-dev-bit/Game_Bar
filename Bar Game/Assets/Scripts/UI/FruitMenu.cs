using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using BarGame.Player;

public class FruitMenu : MonoBehaviour {
    [Header("UI Settings")]
    [SerializeField] private Transform buttonParent;
    [SerializeField] private GameObject buttonPrefab;

    private Dictionary<string, int> fruitCounts = new Dictionary<string, int>();
    private Dictionary<string, FruitButton> fruitButtons = new Dictionary<string, FruitButton>();
    private Dictionary<string, GameObject> fruitPrefabs = new Dictionary<string, GameObject>();

    public void UpdateAllButtons(Dictionary<string, int> newCounts)
    {
        // 1. Удаляем старые кнопки
        CleanupMissingFruits(newCounts);

        // 2. Обновляем данные
        foreach (var pair in newCounts)
        {
            if (!fruitButtons.ContainsKey(pair.Key))
            {
                // Ищем префаб по имени (если еще не загружен)
                if (!fruitPrefabs.ContainsKey(pair.Key))
                {
                    var prefab = Resources.Load<GameObject>($"Fruits/{pair.Key}");
                    fruitPrefabs.Add(pair.Key, prefab);
                }

                CreateNewButton(fruitPrefabs[pair.Key], pair.Value);
            }
            else
            {
                fruitButtons[pair.Key].UpdateCount(pair.Value);
            }

            // Обновляем счетчик
            fruitCounts[pair.Key] = pair.Value;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(buttonParent as RectTransform);
    }

    private void CreateNewButton(GameObject fruitPrefab, int initialCount)
    {
        GameObject newButton = Instantiate(buttonPrefab, buttonParent);
        FruitButton buttonComponent = newButton.GetComponent<FruitButton>();

        buttonComponent.Initialize(
            fruitPrefab: fruitPrefab,
            count: initialCount
        );

        fruitButtons.Add(GetCleanName(fruitPrefab), buttonComponent);
    }

    // Удаление кнопок для несуществующих фруктов
    private void CleanupMissingFruits(Dictionary<string, int> currentCounts)
    {
        List<string> toRemove = new List<string>();

        foreach (var key in fruitButtons.Keys)
        {
            if (!currentCounts.ContainsKey(key))
            {
                Destroy(fruitButtons[key].gameObject);
                toRemove.Add(key);
            }
        }

        foreach (var key in toRemove)
        {
            fruitButtons.Remove(key);
        }
    }

    // Остальные методы (SelectFruit, AddOrUpdateFruit) остаются без изменений

public void AddOrUpdateFruit(GameObject fruitPrefab, int count)
    {
        string fruitName = GetCleanName(fruitPrefab);

        // Обновляем счетчик
        fruitCounts[fruitName] = count;

        // Создаем или обновляем кнопку
        if (!fruitButtons.ContainsKey(fruitName))
        {
            CreateNewButton(fruitPrefab, count);
        }

        fruitButtons[fruitName].UpdateCount(count);
    }

    public void SelectFruit(GameObject fruitPrefab)
    {
        string fruitName = GetCleanName(fruitPrefab);

        if (fruitCounts.TryGetValue(fruitName, out int count) && count > 0)
        {
            // 1. Создаем экземпляр фрукта
            GameObject newFruit = Instantiate(fruitPrefab);
            PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
            player.PickUpHandler.SetCurrentPickUp(newFruit);

            // 2. Обновляем счетчик
            fruitCounts[fruitName]--;

            // 3. Обновляем UI
            fruitButtons[fruitName].UpdateCount(fruitCounts[fruitName]);
        }
    }


    private string GetCleanName(GameObject fruit) => fruit.name.Replace("(Clone)", "");
}