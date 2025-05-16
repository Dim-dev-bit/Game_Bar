using System.Collections.Generic;
using UnityEngine;

public class RecipesManager {
    public Dictionary<string, List<string>> Recipes = new Dictionary<string, List<string>>
    {
        // shaking indicates start and end of shaking, that's why there are two of these
        // Not because I cannot code, bruh
        { "lemonade", new List <string> {"shaking", "Ice", "AppleJuice", "shaking"} }, // lemonade
        {"valera", new List <string> { "shaking", "Wine", "AppleJuice", "shaking", "stirring" } } // valera
        // More to go
    };
}