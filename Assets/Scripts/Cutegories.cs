using Post;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cutegories : MonoBehaviour
{
    [SerializeField]
    private string curCategory;

    [SerializeField]
    private List<GameObject> categories; // Two buttons at one
    public GameObject prefabCategory;

    [SerializeField]
    private GameObject postHandler;
    private PostHandler scPostHandler;

    // Start is called before the first frame update
    void Start()
    {
        scPostHandler = postHandler.transform.GetComponent<PostHandler>();
        categories = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update current category
        ChangeCategory(scPostHandler.curCategory);
    }

    private void ChangeCategory(string category) {
        curCategory = category;
        for (int i = 0; i < categories.Count; i++) {
            categories[i].transform?.GetComponent<CategoryActive>()?.SetInctive();

            if (categories[i].transform?.GetComponent<CategoryActive>()?.GetName() == category) {
                categories[i].transform?.GetComponent<CategoryActive>()?.SetActive();
            }

        }
    }

    // Change category by click
    public void CategotyClick() {
        GameObject curButton = EventSystem.current.currentSelectedGameObject.gameObject;
        curCategory = curButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        // Set al buttons as inactive
        for (int i = 0; i < categories.Count; i++) {
            categories[i].transform?.GetComponent<CategoryActive>()?.SetInctive();
        }
        
        // Set pressed button as asctive
        curButton.transform?.parent.transform?.GetComponent<CategoryActive>()?.SetActive();

        // Change overall category
        scPostHandler.ChangeCategory(curCategory);
    }

    public void CreateCategory(string name) {

        RectTransform rv = (RectTransform)(prefabCategory.transform);

        // Set position on X-axes
        float xPosition = 150 + categories.Count * (Screen.width/4 );

        Vector3 position = new Vector3(xPosition, this.transform.position.y, this.transform.position.z);

        // Initiate prefabCategory
        GameObject category = Instantiate(prefabCategory, position, Quaternion.identity) as GameObject;
        category.transform?.GetComponent<CategoryActive>()?.SetCategory(name, CategotyClick);
        category.transform.SetParent(this.transform);

        // The app starts with category1
        if (categories.Count == 0) {
            category.transform?.GetComponent<CategoryActive>()?.SetActive();
        } else category.transform?.GetComponent<CategoryActive>()?.SetInctive();

        categories.Add(category);
    }
}
