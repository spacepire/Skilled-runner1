using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Move moveScp;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;


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
            moveScp.enabled = false;
            canvas.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && canvas.activeSelf == true)
        {
            canvas.gameObject.SetActive(false);
            moveScp.enabled = true;
        }
    }

    public void ExitButton()
    {
        moveScp.enabled = true;
        canvas.gameObject.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }

    public void SoundSettings()
    {
        audioSource.volume = slider.value;
    }

}
