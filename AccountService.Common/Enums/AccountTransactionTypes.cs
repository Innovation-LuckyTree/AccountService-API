using System.Text.Json.Serialization;

namespace AccountService.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccountTransactionTypes
{
    Debit,
    Credit
}
