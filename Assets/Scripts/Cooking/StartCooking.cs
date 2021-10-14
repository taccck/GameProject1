using UnityEngine;

public class StartCooking : MonoBehaviour
{
    [SerializeField] private GameObject resultScreen;
    private Recipes.Recipe recipe;
    private int currIndex;

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
                Result();
        }
    }

    public void Next()
    {
        if (recipe.cookingSections.Length == currIndex)
        {
            Result();
            return;
        }

        CookingController cntl = Instantiate(recipe.cookingSections[currIndex]).GetComponent<CookingController>();
        cntl.transform.position = (Vector2) Camera.main.transform.position;
        cntl.startCooking = this;
        currIndex++;
    }

    private void Result()
    {
        GameObject result = Instantiate(resultScreen);
        result.transform.position = (Vector2) Camera.main.transform.position;
        resultScreen.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = recipe.sprite;
    }
}