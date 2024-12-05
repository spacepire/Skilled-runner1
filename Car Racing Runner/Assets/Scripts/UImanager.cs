using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] PlayerController playerScp;
    //[SerializeField] AudioSource audioSource;
    //[SerializeField] Slider slider;


    private void Start()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        UIInput();
    }

    private void UIInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canvas.activeSelf == false)
        {
            playerScp.enabled = false;
            canvas.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && canvas.activeSelf == true)
        {
            canvas.gameObject.SetActive(false);
            playerScp.enabled = true;
        }
    }

    public void ExitButton()
    {
        playerScp.enabled = true;
        canvas.gameObject.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }

    public void SoundSettings()
    {
        //audioSource.volume = slider.value;
    }

}
