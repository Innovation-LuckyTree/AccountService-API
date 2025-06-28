using System.Text.Json.Serialization;

namespace AccountService.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountTypes
    {
        ACCOUNT_WALLET,
        AGENT_WALLET
    }
}
