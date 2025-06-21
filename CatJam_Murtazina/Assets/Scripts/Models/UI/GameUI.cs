using TMPro;
using UnityEngine;

namespace CatJam
{
    public class GameUI : MonoBehaviour, IGameUI
    {
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _timerText;

        private void Awake()
        {
            _loseScreen.SetActive(false);
        }

        public void SetScore(string score)
        {
            _score.text = $"Score: {score}";
        }

        public void GameOver()
        {
            _loseScreen.SetActive(true);
        }

        public void SetTimer(int timer)
        {
            _timerText.text = $"Secondes: {timer}";
        }
    }
}