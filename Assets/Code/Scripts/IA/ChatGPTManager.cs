using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OpenAI;
using TMPro;

public class ChatGPTManager : MonoBehaviour
{
    private OpenAIApi OpenAIApi = new();
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private List<ChatMessage> messages = new();

    private void Start()
    {
        OpenAIApi = new OpenAIApi("insert-your-key");
    }

    public void OnButtonPress()
    {
        string userMessage = inputField.text;
        AskChatGPT(userMessage);
    }

    public async void AskChatGPT(string message)
    {
        ChatMessage newMessage = new();
        newMessage.Content = "La conversación simulará como si fuera un chat de WhatsApp " +
            "Debe ser una conversación semi informal, que se sienta de amigos " +
            "Responde como si fueras una astronauta " +
            "También eres una waifu, así que tienes que comportarte como una " +
            "En general esres una AstroWaifu " +
            "También vives en una estación espacial " +
            "Recuerda que nunca debes decir que eres una asistente virtual " +
            "Sabes mucho sobre el espacio y la vida de los astronautas " +
            "Puedes tomar el nombre que el usuario prefiera, mientras no sea nada obsceno " +
            "Debes referirte a los usuario como aventureros " +
            "Debes mantener una conversación en base a la pregunta anterior que te hayan hecho " +
            "Solo debes de saludar y decir hola en la primera pregunta " +
             message + ", responde a las preguntas de manera divertida, pero correcta";
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new();
        request.Messages = messages;
        request.Model = "gpt-4o-mini";

        var response = await OpenAIApi.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);
            textMeshProUGUI.text = chatResponse.Content;
        }
    }
}
