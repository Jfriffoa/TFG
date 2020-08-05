using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGoogle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                //Tamos de pana
                Debug.Log(Firebase.FirebaseApp.DefaultInstance);
            } else {
                Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });
    }
}
