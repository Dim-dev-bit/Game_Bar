using System.Collections.Generic;

public class RecipesManager {
    public Dictionary<string, List<string>> Recipes = new Dictionary<string, List<string>>
    {
        // shaking indicates start and end of shaking, that's why there are two of these
        // Not because I cannot code, bruh
        //{ "lemonade", new List <string> {"shaking", "Ice", "AppleJuice", "shaking"} }, // lemonade
        //{"valera", new List <string> { "shaking", "Wine", "AppleJuice", "shaking", "stirring" } }, // valera
        //// More to go

        {"margarita", new List<string> {"shaking", "Tequila", "LimeJuice", "OrangeJuice",
        "shaking", "Lime", "Chalet" } },

        {"ginFizz", new List<string> { "shaking", "Ice", "Jin", "LimeJuice", "shaking", "Lime", "Highball"} },

        {"gimlet", new List<string> {"shaking", "Jin", "LimeJuice", "shaking", "Lime", "Chalet"}},

        {"wine", new List<string> {"Wine", "WineGlass"} },

        {"longIslandIcedTea", new List<string> { "shaking", "Vodka", "Tequila", "Jin", "LimeJuice",
        "shaking", "Lime", "Highball"} }
    };
}