using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public abstract class AIBehaviour : MonoBehaviour
{
    protected OpenAIApi OpenAIApi = new();
    protected List<ChatMessage> messages = new();

    private const string _key = "";

    protected virtual void Start() => OpenAIApi = new OpenAIApi(_key);
}