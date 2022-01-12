using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _gameOver;
    [SerializeField] private GameObject _text;
    [SerializeField] private Button _buttonGameOver;

    private void OnEnable()
    {
        GameManager.Started += GameManager_Started; 
        GameManager.Fineshed += GameManager_Finished;
    }

    private void OnDisable()
    {
        GameManager.Started -= GameManager_Started;
        GameManager.Fineshed -= GameManager_Finished;
    }

    private void Start()
    {
        _buttonGameOver.onClick.AddListener(Reload);
        _gameOver.gameObject.SetActive(false);
        _text.SetActive(true);
        _buttonGameOver.gameObject.SetActive(false);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneController.TutorialLevel);
    }

    private void GameManager_Started()
    {
        _text.SetActive(false);
    }

    private void GameManager_Finished()
    {
        _buttonGameOver.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(true);
    }
}
