using FG;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CookingManager : MonoBehaviour
{
    [SerializeField] private GameObject resultScreen;
    private Recipes.Recipe recipe;
    private int currIndex;
    [HideInInspector] public int successfulEvents;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            recipe = Recipes.current.GetRecipe(other.GetComponent<PlayerInventory>().inventory);
            other.GetComponent<PlayerInput>().enabled = false;
            Destroy(other.transform.GetChild(1).gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).position = (Vector2) Camera.main.transform.position;

            if (recipe.ingredients != null)
                Next();
            else
                Result(false);
        }
    }

    public void Next()
    {
        if (recipe.cookingSections.Length == currIndex)
        {
            Result(true);
            return;
        }

        CookingController cntl = Instantiate(recipe.cookingSections[currIndex]).GetComponent<CookingController>();
        cntl.transform.position = (Vector2) Camera.main.transform.position;
        cntl.cookingManager = this;
        currIndex++;
    }

    private void Result(bool canCook)
    {
        int score = 0;
        if (canCook)
            score = (int) ((successfulEvents / (float) recipe.cookingSections.Length) * recipe.ingredients.Length * 20);

        GameObject result = Instantiate(resultScreen);
        result.transform.position = (Vector2) Camera.main.transform.position;
        SpriteRenderer spRenderer = result.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spRenderer.sprite = recipe.sprite;
        result.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = $"Score: {score}";
    }
}