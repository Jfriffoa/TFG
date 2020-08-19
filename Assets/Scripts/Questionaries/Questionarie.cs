using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace TFG.Questionaries {
    public class Questionarie : MonoBehaviour {

        // Player Prefs Keys
        internal static readonly string PRE_QUESTIONARIE = "Prequestionarie";
        internal static readonly string POST_QUESTIONARIE = "Postquestionarie";
        internal static readonly string GAME_DONE = "GameDone";

        // Default scene to go once the questionaries has been done
        [Scene] public string mainMenu;

        [Header("UI Elements")]
        public TextMeshProUGUI title;
        public TextMeshProUGUI introText;
        public TextMeshProUGUI endText;

        [Header("Questionarie")]
        public GameObject demographicsPanel;
        public TMP_Dropdown gender;
        public TMP_InputField age;
        public TMP_Dropdown country;
        public Transform[] questionContainers;
        List<Question> _questions;

        [Header("Custom stuff")]
        public CustomValues preQuestionarie;
        public CustomValues postQuestionarie;

        // Data stuff
        bool _isPostQuestionarie = false;

        // Check which questionarie to load
        void Start() {
            bool isPreQuestionarieDone = PlayerPrefs.GetInt(PRE_QUESTIONARIE, 0) == 1;
            bool isGameDone = PlayerPrefs.GetInt(GAME_DONE, 0) == 1;
            bool isPostQuestionarieDone = PlayerPrefs.GetInt(POST_QUESTIONARIE, 0) == 1;

            if (!isPreQuestionarieDone)
                LoadPrequestionarie();
            else if (isGameDone && !isPostQuestionarieDone)
                LoadPostquestionarie();
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);

            _questions = new List<Question>();
            foreach(var container in questionContainers) {
                _questions.AddRange(container.GetComponentsInChildren<Question>(true));
            }
        }

        void LoadPrequestionarie() {
            title.text = preQuestionarie.title;
            introText.text = preQuestionarie.introText;
            endText.text = preQuestionarie.endText;
        }

        void LoadPostquestionarie() {
            title.text = postQuestionarie.title;
            introText.text = postQuestionarie.introText;
            endText.text = postQuestionarie.endText;
            // We won't need to ask for demographics again
            Destroy(demographicsPanel);
            _isPostQuestionarie = true;
        }

        public void SaveData() {
            if (!_isPostQuestionarie) {
                int ageInt = -1;
                if (int.TryParse(age.text, out int result)) {
                    ageInt = result;
                }

                Persistence.Instance.data.userData = new AppData.UserData() {
                    edad = ageInt,
                    genero = gender.options[gender.value].text,
                    pais = country.options[country.value].text
                };
            }

            int[] temp = new int[_questions.Count];
            foreach(var question in _questions) {
                temp[question.id - 1] = question.value;
            }

            AppData.QuestionarieData data = new AppData.QuestionarieData {
                questions = new List<int>(temp)
            };

            // Pre questionarie
            if (!_isPostQuestionarie) {
                Persistence.Instance.data.prePollData = data;
                PlayerPrefs.SetInt(PRE_QUESTIONARIE, 1);
                Persistence.Log("Prequestionarie done");

            // Post questionarie
            } else {
                Persistence.Instance.data.postPollData = data;
                PlayerPrefs.SetInt(POST_QUESTIONARIE, 1);
                Persistence.Log("Postquestionarie done");
            }

            Persistence.Instance.SaveData();
            PlayerPrefs.Save();
        }

        [System.Serializable]
        public struct CustomValues {
            public string title;
            [TextArea] public string introText;
            [TextArea] public string endText;
        }
    }
}