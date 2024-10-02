using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.DTOs;
using ExpenseTracker.Business.Client.Helpers;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Business.Client.Services;

public class ReceiptsService(HttpClient httpClient, IConfiguration configuration) : IReceiptsService
{
    public async Task<Result<ReceiptDTO>> GetReceiptByCode(string code)
    {
        var receiptEndpoint = configuration["ReceiptsApi:Endpoint"];

        try
        {
            var response = await httpClient.PostAsJsonAsync(receiptEndpoint, new { receiptId = code });

            if (!response.IsSuccessStatusCode)
            {
                return Result<ReceiptDTO>.Error("Failed to get receipt.");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonRoot = JsonDocument.Parse(jsonString).RootElement;

            var createDate = jsonRoot.GetProperty("receipt").GetProperty("createDate").GetString();
            var totalPrice = jsonRoot.GetProperty("receipt").GetProperty("totalPrice").GetDecimal();

            var items = jsonRoot.GetProperty("receipt").GetProperty("items").EnumerateArray()
                .Select(item => new ReceiptItemDTO
                {
                    Name = item.GetProperty("name").GetString(),
                    Price = item.GetProperty("price").GetDecimal()
                }).ToList();

            var receiptDTO = new ReceiptDTO
            {
                CreateDate = createDate != null ? DateTime.ParseExact(createDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture) : null,
                TotalPrice = totalPrice,
                Items = items
            };

            return Result<ReceiptDTO>.Success(receiptDTO);
        }
        catch (HttpRequestException)
        {
            return Result<ReceiptDTO>.Error("Failed to get receipt.");
        }
        catch (Exception)
        {
            return Result<ReceiptDTO>.Error("Failed to parse receipt.");
        }
    }
}
