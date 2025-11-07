namespace jerseyShoppingCartMvcUI.Models.DTOs;

public record TopNSoldjerseyModel(string jerseyName, string teamName, int TotalUnitSold);
public record TopNSoldjerseysVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldjerseyModel> TopNSoldjerseys);