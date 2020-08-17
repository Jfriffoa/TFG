using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class TimeDecision : MonoBehaviour {
        float _startTime;

        public void StartTimer() {
            _startTime = Time.time;
        }
        
        public void StartAlive() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionInicial = AppData.GameData.Decision.Vivir;
            Persistence.Instance.data.gameData.tiempoDecisionInicial = dt;
            Persistence.Log("Start Alive");
            Persistence.Instance.SaveData();
        }

        public void StartDead() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionInicial = AppData.GameData.Decision.Morir;
            Persistence.Instance.data.gameData.tiempoDecisionInicial = dt;
            Persistence.Log("Start Dead");
            Persistence.Instance.SaveData();
        }

        public void EndAlive() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionFinal = AppData.GameData.Decision.Vivir;
            Persistence.Instance.data.gameData.tiempoDecisionFinal = dt;
            Persistence.Log("End Alive");
            Persistence.Instance.SaveData();
        }

        public void EndDead() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionFinal = AppData.GameData.Decision.Morir;
            Persistence.Instance.data.gameData.tiempoDecisionFinal = dt;
            Persistence.Log("End Dead");
            Persistence.Instance.SaveData();
        }

        public void ChoosePills() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionPastillas = AppData.GameData.Pastillas.Tomarlas;
            Persistence.Instance.data.gameData.tiempoDecisionPastillas = dt;
            Persistence.Log("Take Pills");
            Persistence.Instance.SaveData();
        }

        public void IgnorePills() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionPastillas = AppData.GameData.Pastillas.Ignorarlas;
            Persistence.Instance.data.gameData.tiempoDecisionPastillas = dt;
            Persistence.Log("Ignore Pills");
            Persistence.Instance.SaveData();
        }

        public void ChooseStudy() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionEstudio = AppData.GameData.Estudio.Estudiar;
            Persistence.Instance.data.gameData.tiempoDecisionEstudio = dt;
            Persistence.Log("Study Night");
            Persistence.Instance.SaveData();
        }

        public void ChooseSleep() {
            var dt = Time.time - _startTime;
            Persistence.Instance.data.gameData.decisionEstudio = AppData.GameData.Estudio.Dormir;
            Persistence.Instance.data.gameData.tiempoDecisionEstudio = dt;
            Persistence.Log("Sleep Night");
            Persistence.Instance.SaveData();
        }
    }
}