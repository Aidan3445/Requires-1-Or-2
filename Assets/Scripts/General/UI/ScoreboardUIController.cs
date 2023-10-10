using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace General.UI
{
    public class ScoreboardUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text scoreChangeText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text bodyText;
        [SerializeField] private GameObject panel;

        private static readonly int MakeChange = Animator.StringToHash("makeChange");

        public void UpdateTimer(int seconds)
        {
            timerText.text = $"Timer: {seconds / 60:00}:{seconds % 60:00}";
        }

        public void UpdateScore(int total, int pointChange)
        {
            scoreText.text = $"Score: {total}";
            
            if (pointChange == 0) return;
            StartCoroutine(ScoreChangeUI(pointChange));
        }

        private IEnumerator ScoreChangeUI(int pointChange)
        {
            TMP_Text changeText = Instantiate(scoreChangeText, panel.transform);
            if (pointChange > 0)
            {
                changeText.color = Color.green;
                changeText.text = $"+{pointChange}";
            }
            else
            {
                changeText.color = Color.red;
                changeText.text = $"{pointChange}";
            }
            Animator textAnimator = changeText.GetComponent<Animator>();
            textAnimator.SetBool(MakeChange, true);
            yield return new WaitForSeconds(1.5f);
            Destroy(changeText.gameObject);
        }
        
        public void SetHighScore(string text)
        {
            highScoreText.text = text;
        }
        
        public void UpdateHighScore(int points)
        {
            highScoreText.text = $"High Score: {points}";
        }
        
        public void SetBody(string text)
        {
            bodyText.text = text;
        }
    }
}
