using UnityEngine;

public class EndGame : MonoBehaviour
{
    private bool playerIsClose;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsClose = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
