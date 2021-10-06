using System;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour
{
    [NonSerialized] public static Recipes current;

    [SerializeField] private Recipe[] recipes;

    private Recipe failedRecipe;

    [Serializable]
    public struct Recipe
    {
        public string name;
        public Item[] ingredients;
        public Sprite sprite;
    }

    public Recipe GetRecipe(Item[] items)
    {
        foreach (Recipe recipe in recipes)
        {
            int match = 0;
            List<Item> tempInvItems = new List<Item>(items);

            foreach (Item recItem in recipe.ingredients)
            {
                bool matched = false;

                foreach (Item invItem in tempInvItems)
                {
                    if (recItem == invItem)
                    {
                        match++;
                        matched = true;
                        break;
                    }
                }

                if (matched) tempInvItems.Remove(recItem);
            }

            if (match == recipe.ingredients.Length)
                return recipe;
        }

        return failedRecipe;
    }

    private void Sort()
    {
        List<Recipe> tempRecipes = new List<Recipe>(recipes);
        int filled = 0;

        AddRecipesWithLengthOf(5);
        AddRecipesWithLengthOf(4);
        AddRecipesWithLengthOf(3);
        AddRecipesWithLengthOf(2);
        AddRecipesWithLengthOf(1);
        AddRecipesWithLengthOf(0);

        void AddRecipesWithLengthOf(int length)
        {
            Stack<int> toRemove = new Stack<int>();
            for (int i = 0; i < tempRecipes.Count; i++)
            {
                if (tempRecipes[i].ingredients.Length == length)
                {
                    recipes[filled] = tempRecipes[i];
                    toRemove.Push(i);
                    filled++;
                }
            }

            foreach (int i in toRemove)
            {
                tempRecipes.RemoveAt(i);
            }
        }
    }

    private void Awake()
    {
        current = this;
        failedRecipe = new Recipe()
        {
            ingredients = null,
            name = "WTF have you done!!",
            sprite = null
        };

        Sort();
    }
}