using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class TimeDecision : MonoBehaviour {
        float _startTime;
        bool _isFirstPlaythrough;

        public void StartTimer() {
            _startTime = Time.time;
            _isFirstPlaythrough = PlayerPrefs.GetInt(Questionaries.Questionarie.GAME_DONE, 0) == 0;
        }
        
        public void StartAlive() {
            Persistence.Log("Start Alive");

            if (!_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionInicial = AppData.GameData.Decision.Vivir;
            Persistence.Instance.data.gameData.tiempoDecisionInicial = dt;
            Persistence.Instance.SaveData();
        }

        public void StartDead() {
            Persistence.Log("Start Dead");

            if (!_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionInicial = AppData.GameData.Decision.Morir;
            Persistence.Instance.data.gameData.tiempoDecisionInicial = dt;
            Persistence.Instance.SaveData();
        }

        public void EndAlive() {
            Persistence.Log("End Alive");

            if (!_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionFinal = AppData.GameData.Decision.Vivir;
            Persistence.Instance.data.gameData.tiempoDecisionFinal = dt;
            Persistence.Instance.data.gameData.ganasDeVivir = WillingnessController.Instance.willingness;
            Persistence.Instance.SaveData();
            PlayerPrefs.SetInt(Questionaries.Questionarie.GAME_DONE, 1);
            PlayerPrefs.Save();
        }

        public void EndDead() {
            Persistence.Log("End Dead");

            if (!_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionFinal = AppData.GameData.Decision.Morir;
            Persistence.Instance.data.gameData.tiempoDecisionFinal = dt;
            Persistence.Instance.data.gameData.ganasDeVivir = WillingnessController.Instance.willingness;
            Persistence.Instance.SaveData();
            PlayerPrefs.SetInt(Questionaries.Questionarie.GAME_DONE, 1);
            PlayerPrefs.Save();
        }

        public void ChoosePills() {
            Persistence.Log("Take Pills");

            if (!_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionPastillas = AppData.GameData.Pastillas.Tomarlas;
            Persistence.Instance.data.gameData.tiempoDecisionPastillas = dt;
            Persistence.Instance.SaveData();
        }

        public void IgnorePills() {
            Persistence.Log("Ignore Pills");

            if (_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionPastillas = AppData.GameData.Pastillas.Ignorarlas;
            Persistence.Instance.data.gameData.tiempoDecisionPastillas = dt;
            Persistence.Instance.SaveData();
        }

        public void ChooseStudy() {
            Persistence.Log("Study Night");

            if (!_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionEstudio = AppData.GameData.Estudio.Estudiar;
            Persistence.Instance.data.gameData.tiempoDecisionEstudio = dt;
            Persistence.Instance.SaveData();
        }

        public void ChooseSleep() {
            Persistence.Log("Sleep Night");

            if (!_isFirstPlaythrough)
                return;

            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionEstudio = AppData.GameData.Estudio.Dormir;
            Persistence.Instance.data.gameData.tiempoDecisionEstudio = dt;
            Persistence.Instance.SaveData();
        }
    }
}