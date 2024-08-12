using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace Phi3;

public class DensoPlugin
{
    [KernelFunction, Description("Send an specific command to the robot")]
    public string SendCommand([Description("The command we'll send to the robot")] CommandType command)
    {
        Console.WriteLine($"Send command {command} to the robot");

        //<< WORK >> -esta función pone al robot a trabajar
        //<< DANCE >> -esta función pone al robot a bailar
        //<< SALUTE >> -esta función le pide al robot saludar
        //<< FASTER >> -esta función pone al robot a trabajar más rápido
        //<< BYE >> -esta función le pide al robot que se despida
        //<< STOP >> -esta función detiene el robot
        //<< GYM >> -esta función pone al robot a hacer gimnasia
        //<< NOTIFY >> -esta función envía una notificación al responsable
        switch (command)
        {
            case CommandType.work:
                Console.WriteLine("WORK");
                break;
            case CommandType.dance:
                Console.WriteLine("DANCE");
                break;
            case CommandType.greet:
                Console.WriteLine("SALUTE");
                break;
            case CommandType.stop:
                Console.WriteLine("STOP");
                break;
            case CommandType.work_faster:
                Console.WriteLine("FASTER");
                break;
            case CommandType.do_gym:
                Console.WriteLine("GYM");
                break;
            case CommandType.nofity:
                Console.WriteLine("NOTIFY");
                break;
            default:
                Console.WriteLine("Command not found!");
                return "Command not found!";
        }

        return "Command sent successfully!";
    }

    [KernelFunction, Description("Read the telemetry and the properties from the robot")]
    public string ReadTelemetry([Description("The attribute to read from telemetry or property")] AttributeType attribute)
    {
        Console.WriteLine($"Read attribute {attribute} from the robot");

        switch (attribute)
        {
            case AttributeType.status:
                return "TRABAJANDO";
            case AttributeType.current_speed:
                return "55";
            case AttributeType.number_of_work_executions:
                return "11";
            case AttributeType.number_of_faster_executions:
                return "111";
            case AttributeType.number_of_bye_executions:
                return "12";
            case AttributeType.number_of_gym_executions:
                return "13";
            case AttributeType.number_of_dance_executions:
                return "14";
            case AttributeType.number_of_salute_executions:
                return "15";
            case AttributeType.number_of_processed_pieces:
                return "16";
            case AttributeType.current_job:
                return "current.CurrentJob";
            default:
                return "NOT ENOUTGH INFORMATION";
        }
    }
}

    public enum CommandType
    {
        work,
        dance,
        greet,
        stop, 
        work_faster,
        do_gym,
        nofity
    }

    public enum AttributeType
    {
        status,
        current_speed,
        number_of_work_executions,
        number_of_faster_executions,
        number_of_bye_executions,
        number_of_gym_executions,
        number_of_dance_executions,
        number_of_salute_executions,
        number_of_processed_pieces,
        current_job
    }