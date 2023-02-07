namespace BMS.Web.Api.Controllers
{
    public partial class Command
    {
        public Command() { }

        public Command(CommandType type, string payload, int? chargeControllerId, int? chargePointId,  object additionalValue)
        {
            Type = type;
            Payload = payload;
            ChargeControllerId = chargeControllerId;
            ChargePointId = chargePointId;
            AdditionalValue = additionalValue;
        }

        public static string GetPayloadForRestartAppTypes(CommandType type)
        {
            if (type == CommandType.RestartSM)
                return "{\"app\":\"sm\"}";
            else if (type == CommandType.RestartCA)
                return "{\"app\":\"ca\"}";
            else if (type == CommandType.RestartOCPP)
                return "{\"app\":\"ocpp\"}";
            else if (type == CommandType.RestartMB)
                return "{\"app\":\"mb\"}";
            else if (type == CommandType.RestartMS)
                return "{\"app\":\"ms\"}";
            else if (type == CommandType.RestartJC)
                return "{\"app\":\"js\"}";
            else if (type == CommandType.RestartLM)
                return "{\"app\":\"llm\"}";
            else if (type == CommandType.RestartWEB)
                return "{\"app\":\"website\"}";
            return "{}";
        }

        public static string GetNameForRestartAppTypes(CommandType type)
        {
            if (type == CommandType.RestartSM)
                return "restart-sm";
            else if (type == CommandType.RestartCA)
                return "restart-ca";
            else if (type == CommandType.RestartOCPP)
                return "restart-ocpp";
            else if (type == CommandType.RestartMB)
                return "restart-mb";
            else if (type == CommandType.RestartMS)
                return "restart-ms";
            else if (type == CommandType.RestartJC)
                return "restart-jc";
            else if (type == CommandType.RestartLM)
                return "restart-lm";
            else if (type == CommandType.RestartWEB)
                return "restart-web";
            return "restart-?";
        }

        public static bool IsRebootCommand(string? commandType)
        {
            if (commandType != null  && (RestartAppList.ConvertAll(x => x.ToString()).Contains(commandType) || CommandType.RestartApp.ToString() == commandType))
                return true;
            return false;
        }

        public static readonly List<CommandType> RestartAppList = new List<CommandType>()
        {
            CommandType.RestartMB,
            CommandType.RestartLM,
            CommandType.RestartOCPP,
            CommandType.RestartCA,
            CommandType.RestartWEB,
            CommandType.RestartJC,
            CommandType.RestartMS,
            CommandType.RestartSM
        };
    }
}
