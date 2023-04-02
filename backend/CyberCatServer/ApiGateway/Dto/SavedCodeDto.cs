using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class SavedCodeDto
{
    [JsonPropertyName("text")] public string Text { get; set; } = null!;

    public SavedCodeDto(string savedCode)
    {
        Text = savedCode;
    }

    // Конструктор без параметров всегда должен в классах учавствующих в десериализации.
    // Потому что десериализатор сначала создает объект по этому конструктору, а потом наполняет его поля.
    public SavedCodeDto()
    {
    }
}