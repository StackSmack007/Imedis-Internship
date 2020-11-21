using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class EmployeeSearchQuery
    {
        private string phrase;
        public string Phrase
        {
            get
            {
                return this.phrase;
            }
            set
            {
                this.phrase = value.ToLower();
            }
        }
        public ICollection<string> Options { get; set; } = new List<string>();

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Phrase))
            {
                return "No phrase was inputed!";
            }
            if (!Options.Any())
            {
                return "No chriteria was chosen!";
            }
            return $"\"{Phrase}\" in {string.Join(", ", Options)}";
        }
    }
}