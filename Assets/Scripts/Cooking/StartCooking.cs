using FG;
using UnityEngine;

public class StartCooking : MonoBehaviour
{
    private Recipes.Recipe recipe;
    private int currIndex = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            recipe = Recipes.current.GetRecipe(other.GetComponent<PlayerInventory>().inventory);
            other.gameObject.SetActive(false); 
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).position = (Vector2) Camera.main.transform.position;

            if (recipe.ingredients != null)
                Next();
            else
            {
                print(recipe.name);
            }
        }
    }

    public void Next()
    {
        if (recipe.cookingSections.Length == currIndex)
        {
            print(recipe.name);
            return;
        }

        CookingController cntl = Instantiate(recipe.cookingSections[currIndex]).GetComponent<CookingController>();
        cntl.transform.position = (Vector2) Camera.main.transform.position;
        cntl.startCooking = this;
        currIndex++;
    }
}