using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour {
    public TextMeshProUGUI lifeAmount, levelNumber;
    private Player _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        lifeAmount.text = "Lives: " + _player.playerLifes.ToString();
        levelNumber.text = "Level: " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }
}
