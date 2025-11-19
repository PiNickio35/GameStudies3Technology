using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HungerTimer : MonoBehaviour
{
    private Image _timerBar;
    [SerializeField] private float maxTime = 60f;
    private float _currentTime;

    private void Awake()
    {
        _timerBar = GetComponent<Image>();
        _currentTime = maxTime;
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            _timerBar.fillAmount = _currentTime / maxTime;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AddTime()
    {
        _currentTime += 15f;
        _timerBar.fillAmount = _currentTime / maxTime;
    }
}
