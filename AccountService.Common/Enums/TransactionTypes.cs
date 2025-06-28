using System.Text.Json.Serialization;

namespace AccountService.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionTypes
    {
        CASH_IN,
        ACCOUNT_BET,
        ACCOUNT_WINNER,
        WITHDRAW,
        COMMISSION
    }
}
