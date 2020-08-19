using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TFG {
    public class AppData : Data {
        public UserData userData;
        public GameData gameData;
        public QuestionarieData prePollData;
        public QuestionarieData postPollData;

        public class UserData : Data {
            public int edad = -1;
            public string genero;
            public string pais;
        }

        public class GameData : Data {
            //TODO: Definir que data guardar y cuando guardarla.
            // Enums de decisiones (Por un tema de orden)
            public enum Decision { Vivir, Morir, NoDecide }
            public enum Pastillas { Tomarlas, Ignorarlas, NoDecide }
            public enum Estudio { Estudiar, Dormir, NoDecide }

            // Decisión inicial
            public Decision decisionInicial = Decision.NoDecide;
            public float tiempoDecisionInicial = -1;

            // Ruta viva
            public Pastillas decisionPastillas = Pastillas.NoDecide;
            public float tiempoDecisionPastillas = -1;
            public Estudio decisionEstudio = Estudio.NoDecide;
            public float tiempoDecisionEstudio = -1;

            // Ruta Muerta

            // Decisión final
            public Decision decisionFinal = Decision.NoDecide;
            public float tiempoDecisionFinal = -1;
            public float ganasDeVivir = 0;
        }

        public class QuestionarieData : Data {
            public List<int> questions = new List<int>();
        }
    }

    // Each class creates its own dictionary with its properties.
    public abstract class Data {
        public Dictionary<string, object> ToDictionary() {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (FieldInfo field in GetType().GetFields()) {
                // If it's an enum, save it as string
                if (field.FieldType.IsEnum) {
                    result[field.Name] = ((System.Enum)field.GetValue(this)).ToString();
                // If it's a "Data" type, save its own dictionary.
                } else if (field.FieldType.IsSubclassOf(typeof(Data))) {
                    result[field.Name] = ((Data)field.GetValue(this)).ToDictionary();
                // If it's anything else, save it normally
                } else {
                    result[field.Name] = field.GetValue(this);
                }
            }
            return result;
        }

        public Dictionary<string, object> GetDifferences(Data lastSave) {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (FieldInfo field in GetType().GetFields()) {
                // Check if we are differents
                if (field.GetValue(this) != field.GetValue(lastSave)) {
                    // If it's an enum, save it as string
                    if (field.FieldType.IsEnum) {
                        result[field.Name] = ((System.Enum)field.GetValue(this)).ToString();
                    // If it's a "Data" type, check their differences and save them.
                    } else if (field.FieldType.IsSubclassOf(typeof(Data))) {
                        result[field.Name] = ((Data)field.GetValue(this)).GetDifferences((Data)field.GetValue(lastSave));
                    // If it's anything else, save it normally
                    } else {
                        result[field.Name] = field.GetValue(this);
                    }
                }
            }
            return result;
        }
    }
}