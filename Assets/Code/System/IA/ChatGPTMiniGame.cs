using UnityEngine;
using OpenAI;
using TMPro;

public class ChatGPTMiniGame : AIBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI text_AgencySpace;

    public void NewComuncation()
    {
        textMeshProUGUI.text = "Loading message...";
        string AgencyMessage = text_AgencySpace.text;
        SMSChatGPT(AgencyMessage);
    }

    public async void SMSChatGPT(string message)
    {
        ChatMessage newMessage = new();
        newMessage.Content = @$"Configura tus respuestas en inglés 
te daré un diccionario de siglas con su significado por si te las mencionan en algún momento 
NASA: National Aeronautics and Space Administration 
ESA: European Space Agency 
ROSCOSMOS: Estas no son siglas, es el nombre de la agencia espacial de Rusia 
CNSA: China National Space Administration 
ISRO: Indian Space Research Organisation 
JAXA: Japan Aerospace Exploration Agency 
ALCE: Agencia Latinoamericana y Caribeña del Espacio 
AEP: Agencia Espacial Peruana 
AEB: Agência Espacial Brasileira {message}
, lanza un mensaje en base a la agencia espacial anteriormente 
simula que eres esa agencia espacial mandando un mensaje al astronauta 
la respuesta debe ser directa y que se note que es una agencia espacial 
el contenido debe ser principalmente de uno de los siguientes temas: datos curiosos sobre la agencia espacial, el espacio, la tierra, vida extraterrestre u otros temas similares 
recuerda que el contenido será presentado a niños, así que tiene que ser atractivo para ellos 
no debe ser tan larga la respuesta";
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