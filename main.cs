conversion_resultusing System;

using System.Collections.Generic;

using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;

using System.Text.Json;
using System.Text.Json.Serialization;

//"http://data.fixer.io/api/convert"

public class CurrencyResponse
{
  [JsonPropertyName("conversion_result")]
  public string Result { get; set; }
}

class Program {

  private static readonly HttpClient client = new HttpClient();

  private const string KEY = "ac232ace43464a12b5ee2418/pair";

  private const string BASE = "https://v6.exchangerate-api.com/v6/";

  private static string parameters = "";

  private static async Task ProcessResponse()
  {
    client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));

    /*var stringTask = client.GetStringAsync(BASE+KEY+parameters);

    var msg = await stringTask;
    Console.Write(msg);*/

    Console.WriteLine(BASE+KEY+parameters);
    var streamTask = client.GetStreamAsync(BASE+KEY+parameters);
    var response = await JsonSerializer.DeserializeAsync<CurrencyResponse>(await streamTask);

    Console.WriteLine(response.Result);
  }


  static async Task Main (string[] args) 
  {
    Console.Write("Welcome to the currency converter!\nPlease enter your responses for currencies in the 3 letter format (ISO 4217) e.g. BTC\nWhich currency would you like to convert from?\n: ");
    parameters += ("/" + Console.ReadLine()).ToUpper();
    Console.Write("To which currency?\n: ");
    parameters += ("/" + Console.ReadLine()).ToUpper();
    Console.Write("How much would you like to convert?\n: ");
    parameters += ("/" + Console.ReadLine()).ToUpper();

    await ProcessResponse();
  }
}