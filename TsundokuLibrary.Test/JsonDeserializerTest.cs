using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TsundokuLibrary.Test;

[TestClass]
public class JsonDeserializerTest
{
   [TestMethod]
   public void TestSimpelDeserialize()
   {
      var bogJson = @"
         [
            { ""Titel"": ""Sprint"", ""Pris"": 225, ""Format"": ""Ebog""},
            { ""Titel"": ""Årene"", ""Pris"": ""178"", ""Format"": ""Papirbog""}
         ]
         ";

      using var reader = new StringReader(bogJson);
      var bogStream = reader.ReadToEnd();
      Assert.IsNotNull(bogStream, "bogStream er tom");

      var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

      List<Bog> bøger = JsonSerializer.Deserialize<List<Bog?>>(bogStream, options);

      Assert.IsNotNull(bøger, "Bøger er tom");

      Assert.AreEqual(bøger.Count, 2);
      Assert.IsTrue(bøger.Any(b => b != null && b.Pris == 225 && b.Titel == "Sprint" && b.Format == BogType.Ebog), "Sprint mangler");
      Assert.IsTrue(bøger.Any(b => b != null && b.Pris == 178 && b.Titel == "Årene" && b.Format == BogType.Papirbog), "Årene mangler");
   }

   enum BogType
   {
      Papirbog,
      Ebog
   }

   class Bog
   {
      public string Titel { get; set; } = "";
      public int Pris { get; set; } = 0;
      [JsonConverter(typeof(JsonStringEnumConverter))]
      public BogType Format { get; set; }
   }
}

