using TMPro;
using UnityEngine;

namespace CatJam
{
    public class GameUI : MonoBehaviour, IGameUI
    {
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private TMP_Text _score;

        private void Awake()
        {
            _loseScreen.SetActive(false);
        }

        public void SetScore(string score)
        {
            _score.text = $"Score: {score}";
        }

        public void GameOver(bool win)
        {
            _loseScreen.SetActive(!win);
        }
    }
}