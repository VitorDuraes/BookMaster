using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookMaster.Models
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [JsonIgnore]
        public ICollection<LivroModel> Livros { get; set; }

        public static implicit operator AutorModel(List<AutorModel> v)
        {
            throw new NotImplementedException();
        }
    }
}