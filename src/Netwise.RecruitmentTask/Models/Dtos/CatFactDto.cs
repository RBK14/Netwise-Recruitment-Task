using System.Text.Json.Serialization;

namespace Netwise.RecruitmentTask.Models.Dtos;

public record CatFactDto(
    [property: JsonPropertyName("fact")] string Fact,
    [property: JsonPropertyName("length")] int Length);
