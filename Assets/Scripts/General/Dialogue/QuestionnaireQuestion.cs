using UnityEngine;

namespace General.Dialogue
{
    public class QuestionnaireQuestion : MonoBehaviour
    {
        private QuestionnaireManager _manager;
        
        public void Hide()
        {
            _manager.QuestionAnswered();
            Destroy(gameObject);
        }

        public void SetManager(QuestionnaireManager questionnaireManager)
        {
            _manager = questionnaireManager;
        }
    }
}