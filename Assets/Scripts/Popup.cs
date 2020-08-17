using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

public class Popup : MonoBehaviour {
    // Singleton
    static Popup _instance;
    public static Popup Instance { get => _instance; }


    [Header("Textos Default")]
    public string exitMessage = "¿Estás seguro que deseas salir?";
    public string defaultCancelName = "Cancelar";
    public string defaultButtonName = "Ok";
    public string defaultTitle = "Alerta";
    
    [Header("Componentes del Popup")]
    public GameObject layout;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Button[] buttons;

    void Awake() {
        _instance = this;
        layout.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToExit();
        }
    }

    void ToExit() {
        ShowAlert(new Alert {
            title = "Cerrar Aplicacion",
            message = exitMessage,
            buttonName = new string[] { "Si", "No"},
            call = new UnityAction[] { Application.Quit, Instance.ClosePopup }
        });
    }

    public static void ShowAlert(string message) {
        ShowAlert(new Alert {
            title = Instance.defaultTitle,
            message = message,
            buttonName = new string[] { Instance.defaultButtonName },
            call = new UnityAction[] { Instance.ClosePopup }
        });
    }

    public static void ShowAlert(string message, string[] btnName) {
        ShowAlert(new Alert {
            title = Instance.defaultTitle,
            message = message,
            buttonName = btnName,
            call = new UnityAction[] { Instance.ClosePopup }
        });
    }

    public static void ShowAlert(string message, UnityAction[] call) {
        ShowAlert(new Alert {
            title = Instance.defaultTitle,
            message = message,
            buttonName = new string[] { Instance.defaultButtonName },
            call = call
        });
    }

    public static void ShowAlert(string message, string title, string[] btnName, UnityAction[] call) {
        ShowAlert(new Alert {
            title = title,
            message = message,
            buttonName = btnName,
            call = call
        });
    }

    public static void ShowAlert(Alert alert) {
        Instance.layout.SetActive(true);
        
        // Mensajes
        Instance.title.text = alert.title;
        Instance.description.text = alert.message;

        for (int i = 0; i < Instance.buttons.Length; i++) {
            var btn = Instance.buttons[i];

            // Texto
            if (i < alert.buttonName.Length)
                btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = alert.buttonName[i];

            // Listeners
            btn.onClick.RemoveAllListeners();
            if (i < alert.call.Length)
                btn.onClick.AddListener(alert.call[i]);
            btn.onClick.AddListener(Instance.ClosePopup);
        }
    }

    public void ClosePopup() {
        layout.SetActive(false);
    }

    public struct Alert {
        public string title;
        public string message;
        public string[] buttonName;
        public UnityAction[] call;
    }

}
