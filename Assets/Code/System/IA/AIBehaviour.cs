using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public abstract class AIBehaviour : MonoBehaviour
{
    protected OpenAIApi OpenAIApi = new();
    protected List<ChatMessage> messages = new();

    private const string _key = "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";

    protected virtual void Start() => OpenAIApi = new OpenAIApi(_key);
}