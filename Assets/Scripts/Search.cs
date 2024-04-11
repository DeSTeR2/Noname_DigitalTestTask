using TMPro;
using UnityEngine;

public class Search : MonoBehaviour
{
    public string[] categories;

    [SerializeField]
    GameObject didntFind;

    [SerializeField]
    GameObject TextHolder;

    [SerializeField]
    GameObject postHolder;

    private PostHandler scPostHandler;
    void Start()
    {
        scPostHandler = postHolder.transform.GetComponent<PostHandler>();
    }

    public void SetCategory(string[] categories) {
        this.categories = categories;
    }

    public void SearchCategory() {
        // Get text from search panel
        string text = TextHolder.GetComponent<TextMeshProUGUI>().text.ToString();
        text=text.ToLower();

        foreach(string category in categories) {
            bool ok = false;
            string nCategory = category.ToLower();

            // Find if category containce text
            for (int i=0; i< Mathf.Min(text.Length, nCategory.Length); i++) {
                if (nCategory[i] != text[i]) {
                    ok = false;
                    break;
                }
                ok = true;
            }

            if (ok) {
                didntFind.SetActive(false);
                scPostHandler.GetComponent<PostHandler>().ChangeCategory(category);
                return;
            }
        }
        didntFind.SetActive(true);
    }
}
