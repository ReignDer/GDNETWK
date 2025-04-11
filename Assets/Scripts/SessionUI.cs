using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionUI : Singleton<SessionUI>
{
    [SerializeField] Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_Text sessionCodeText;
    TMP_InputField inputField;
    void OnEnable()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        createButton.onClick.AddListener(StartHost);
        joinButton.onClick.AddListener(StartClient);
    }
    
    private void StartClient()
    {
        SessionManager.Instance.JoinSessionByCode(inputField.text);
        DeactivateButton();
    }

    private void StartHost()
    {
        SessionManager.Instance.StartSessionAsHost();
        DeactivateButton();
    }

    private void DeactivateButton()
    {
        createButton.interactable = false;
        joinButton.interactable = false;
    }

    public void ShowSessionCode(string sessionCode)
    {
        sessionCodeText.text = sessionCode;
    }
    
}
