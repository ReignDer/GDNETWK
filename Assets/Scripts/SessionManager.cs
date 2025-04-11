using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    ISession activeSession;

    ISession ActiveSession
    {
        get => activeSession;
        set
        {
            activeSession = value;
            Debug.Log($"Active Session: {activeSession}");
        }
    }
    const string playerNamePropertyKey = "playerName";
    
    async UniTask<Dictionary<string, PlayerProperty>> GetPlayerProperties()
    {
        var playerName = await AuthenticationService.Instance.GetPlayerNameAsync();
        var playerNameProperty =new PlayerProperty(playerName, VisibilityPropertyOptions.Member);
        return new Dictionary<string, PlayerProperty> { { playerNamePropertyKey, playerNameProperty } };
    }
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Sign in anonymously succeeded! Player ID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    // void RegisterSessionEvents()
    // {
    //     ActiveSession.Changed += OnSessionChanged;
    // }
    //
    // void UnregisterSessionEvents()
    // {
    //     ActiveSession.Changed -= OnSessionChanged;
    // }

    public async void StartSessionAsHost()
    {
        var playerProperties = await GetPlayerProperties();
        var options = new SessionOptions
        {
            MaxPlayers = 2,
            IsLocked = false,
            IsPrivate = false,
            PlayerProperties = playerProperties
        }.WithRelayNetwork();
        Debug.Log(playerProperties.ContainsKey("playerName"));
        ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
        SessionUI.Instance.ShowSessionCode(ActiveSession.Code);
        Debug.Log($"Session {ActiveSession.Id} created. Join code: {ActiveSession.Code}");
    }

    async UniTaskVoid JoinSessionByID(string sessionID)
    {
        ActiveSession = await MultiplayerService.Instance.JoinSessionByIdAsync(sessionID);
        Debug.Log($"Session {ActiveSession.Id} joined.");
    }

    public async UniTaskVoid JoinSessionByCode(string sessionCode)
    {
        ActiveSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(sessionCode);
        Debug.Log($"Session {ActiveSession.Id} joined.");
    }

    async UniTaskVoid KickPlayer(string playerID)
    {
        if (!ActiveSession.IsHost) return;
        await ActiveSession.AsHost().RemovePlayerAsync(playerID);
    }

    async UniTask<IList<ISessionInfo>> QuerySessions()
    {
        var sessionQueryOptions = new QuerySessionsOptions();
        QuerySessionsResults result = await MultiplayerService.Instance.QuerySessionsAsync(sessionQueryOptions);
        return result.Sessions;
    }

    async UniTaskVoid LeaveSession()
    {
        if (ActiveSession != null)
        {
            try
            {
                //UnregisterSessionEvents();
                await ActiveSession.LeaveAsync();
            }
            catch
            {

            }
            finally
            {
                ActiveSession = null;
            }
        }
    }
    
}
