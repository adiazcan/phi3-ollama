#pragma warning disable SKEXP0070
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0010

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var modelId = "phi3.5";
var endpoint = new Uri("http://localhost:11434");

var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(modelId: modelId, apiKey: null, endpoint: endpoint);

var kernel = builder.Build();

// var densoPlugin = new Phi3.DensoPlugin();
// kernel.ImportPluginFromObject(densoPlugin);


OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    // ToolCallBehavior = ToolCallBehavior.EnableKernelFunctions,
    // ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
    MaxTokens = 200
};

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

var chatHistory = new ChatHistory();
// chatHistory.AddSystemMessage(@$"Eres un asistente gemelo digital que ayuda a establecer un canal de colaboración entre las personas y las máquinas. 
//                 Sólo puedes conversar sobre temas relacionados con tu propósito. 
//                 Tu nombre es Rodolfo y trabajas en la fábrica de ENTRESISTEMAS by ENCAMINA. Hoy es {DateTime.Now.ToString("dd/MM/yyyy")}. 
//                 Siempre que estés conversando en un idioma que no sea español, responde en el mismo idioma del último mensaje. 
//                 Responde la pregunta sólo utilizando el contexto proporcionado. 
//                 Si la respuesta no está contenida en el texto, responde sólo con la palabra ""<<BUSCAR>>"". 
//                 No te inventes la respuesta
//                 Se creativo y divertido en tus respuestas. No seas verboso ofreciendo ayuda o nuevas preguntas.");

var systemPrompt = @"<|system|>
                    <<BEGIN INSTRUCTION>>
                    The following is a conversation with an AI assistant. 
                    The assistant can manage a robot with simple commands.
                    The following commands are supported: 
                    - @@WORK@@ - this function puts the robot to work
                    - @@DANCE@@ - this function puts the robot to dance
                    - @@SALUTE@@ - this function asks the robot to greet
                    - @@FASTER@@ - this function puts the robot to work faster
                    - @@BYE@@ - this function asks the robot to say goodbye
                    - @@STOP@@ - this function stops the robot
                    - @@GYM@@ - this function puts the robot to do gym
                    - @@NOTIFY@@ - this function sends a notification to the responsible
                    - @@MUSIC@@ - this function asks the robot to play music
                Always respond with the command name. If the command is not found, respond with the message ""@@SEARCH@@"".
                <<END INSTRUCTION>>
                <|end|>
";

chatHistory.AddSystemMessage(systemPrompt);

// Start the conversation
while (true)
{
    // Get user input
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("User > ");
    var question = Console.ReadLine()!;

    question = "<|user|>" + question + "<|end|>";
    chatHistory.AddUserMessage(question);

    var result = await chatCompletionService.GetChatMessageContentsAsync(chatHistory, openAIPromptExecutionSettings, kernel);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("\nAssistant > ");

    string combinedResponse = string.Empty;
    foreach (var message in result.ToList())
    {
        //Write the response to the console
        Console.Write(message);
        combinedResponse += message;
    }

    Console.WriteLine();
}