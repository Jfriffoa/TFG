using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TFG {
    public class AppData : Data {
        public UserData userData;
        public GameData gameData;
        public PrePollData prePollData;
        public PostPollData postPollData;

        public class UserData : Data {
            public int edad = -1;
            public string genero;
            public string pais;
        }

        public class GameData : Data {
            //TODO: Definir que data guardar y cuando guardarla.
            // Enums de decisiones (Por un tema de orden)
            public enum Decision { Vivir, Morir }
            public enum Pastillas { Tomarlas, Ignorarlas }
            public enum Estudio { Estudiar, Dormir }

            // Decisión inicial
            public Decision decisionInicial;
            public float tiempoDecisionInicial = -1;

            // Ruta viva
            public Pastillas decisionPastillas;
            public float tiempoDecisionPastillas = -1;
            public Estudio decisionEstudio;
            public float tiempoDecisionEstudio = -1;

            // Ruta Muerta

            // Decisión final
            public Decision decisionFinal;
            public float tiempoDecisionFinal = -1;
        }

        public class PrePollData : Data {
            //TODO: Hacer la encuesta previa
        }

        public class PostPollData : Data {
            //TODO: Hacer la encuesta posterior
        }

    }

    // Each class creates its own dictionary with its properties.
    public class Data {
        public Dictionary<string, object> ToDictionary() {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (PropertyInfo property in GetType().GetProperties()) {
                // If it's an enum, save it as string
                if (property.PropertyType == typeof(System.Enum)) {
                    result[property.Name] = ((System.Enum)property.GetValue(this)).ToString();
                // If it's a "Data" type, save its own dictionary.
                } else if (property.PropertyType == typeof(Data)) {
                    result[property.Name] = ((Data)property.GetValue(this)).ToDictionary();
                // If it's anything else, save it normally
                } else {
                    result[property.Name] = property.GetValue(this);
                }
            }
            return result;
        }

        public Dictionary<string, object> GetDifferences(Data lastSave) {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (PropertyInfo property in GetType().GetProperties()) {
                // Check if we are differents
                if (property.GetValue(this) != property.GetValue(lastSave)) {
                    // If it's an enum, save it as string
                    if (property.PropertyType == typeof(System.Enum)) {
                        result[property.Name] = ((System.Enum)property.GetValue(this)).ToString();
                    // If it's a "Data" type, check their differences and save them.
                    } else if (property.PropertyType == typeof(Data)) {
                        result[property.Name] = ((Data)property.GetValue(this)).GetDifferences((Data)property.GetValue(lastSave));
                    // If it's anything else, save it normally
                    } else {
                        result[property.Name] = property.GetValue(this);
                    }
                }
            }
            return result;
        }
    }
}