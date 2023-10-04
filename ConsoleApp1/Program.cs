using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var personne = new Personne
            {
                Nom = "DELIGANS",
                Prenom = "Fabien"
            };

            var durations = new List<TimeSpan>();

            for (var i = 0; i < 10_000; i++)
            {
                var start = DateTime.Now;
                var personneSerialized = JsonSerializer.Serialize(personne);

                var end = DateTime.Now;
                var duration = end - start;
                durations.Add(end - start);
            }
            var durationAverage = durations.Average(span => span.Ticks);
            
            var duration1s = new List<TimeSpan>();

            for (var i = 0; i < 10_000; i++)
            {
                var start = DateTime.Now;
                var personneSerialized2 = JsonSerializer.Serialize(personne, PersonneContext.Default.Personne);

                var end = DateTime.Now;
                var duration = end - start;
                duration1s.Add(end - start);
            }
            var duration2Average = duration1s.Average(span => span.Ticks);

            Console.WriteLine($"nb ticks moyen serialization normale : {durationAverage}"); 
            Console.WriteLine($"nb ticks moyen serialization avancée : {duration2Average}");
            
        }
    }

    public class Personne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }

    [JsonSerializable(typeof(Personne))]
    public partial class PersonneContext : JsonSerializerContext
    {
    }
}