using System.Threading;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Crashlytics;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace TFG {
    public class Persistence {
        // Singleton
        static Persistence _instance;
        public static Persistence Instance {
            get {
                if (_instance == null)
                    _instance = new Persistence();
                return _instance;
            }
        }

        // Data
        AppData _lastSavedData;
        public AppData data;

        // Firebase
        DatabaseReference _root;
        FirebaseAuth _auth;
        const int millisecondsToRetrySignIn = 30 * 1000;

        Persistence() {
            // Set-Up Database
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://the-morals-game.firebaseio.com/");
            _root = FirebaseDatabase.DefaultInstance.RootReference;
            _auth = FirebaseAuth.DefaultInstance;

            var instanceCaller = new Thread(TrySignIn);
            instanceCaller.Start();

            // Initialize Data
            data = new AppData() {
                gameData = new AppData.GameData(),
                postPollData = new AppData.PostPollData(),
                prePollData = new AppData.PrePollData(),
                userData = new AppData.UserData()
            };
            _lastSavedData = data;
        }

        // Try to SignIn Async
        void TrySignIn() {
            _auth.SignInAnonymouslyAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    Debug.LogWarning("Can't create user, retrying in " + (millisecondsToRetrySignIn / 1000f) + " seconds...");
                    Thread.Sleep(millisecondsToRetrySignIn);
                    var instanceCaller = new Thread(TrySignIn);
                    instanceCaller.Start();
                    return;
                }

                Persistence.Log("User created:" + task.Result.UserId);
                Crashlytics.SetUserId(task.Result.UserId);
            });
        }

        public void SaveData() {
            _root.Child("users").Child(_auth.CurrentUser.UserId).SetValueAsync(data.ToDictionary()).ContinueWith(task => {
                if (task.IsCompleted) {
                    _lastSavedData = data;
                    Persistence.Log("Data Saved");
                }
            });
        }

        public void SaveDifferences() {
            var diff = data.GetDifferences(_lastSavedData);
            _root.Child("users").Child(_auth.CurrentUser.UserId).SetValueAsync(diff).ContinueWith(task => {
                if (task.IsCompleted) {
                    _lastSavedData = data;
                    Persistence.Log("Data Saved");
                }
            });
        }
        

        // Persistent Logging (For when a crash ocurrs)
        public enum Severity { Info, Warning, Error }
        public static void Log(string message, Severity severity = Severity.Info) {
            switch (severity) {
                case Severity.Info:     Debug.Log(message);         break;
                case Severity.Error:    Debug.LogError(message);    break;
                case Severity.Warning:  Debug.LogWarning(message);  break;
            }

            Crashlytics.Log(severity.ToString() + ": " + message);
        }
    }
}