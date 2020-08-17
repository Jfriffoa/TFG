using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class InitializeFirebase : MonoBehaviour {
        void Start() {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    
                    //Tamos de pana, inicializar persistencia
                    var persistence = Persistence.Instance;
                    
                } else {
                    Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
                }
            });
        }
    }
}