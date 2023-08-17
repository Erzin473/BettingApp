using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace BettingWebApp.Models
{
    [XmlRoot(ElementName = "Match")]
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [XmlElement(ElementName = "Bet")]
        public List<Bet> Bet { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "StartDate")]
        public string StartDate { get; set; }
        [XmlAttribute(AttributeName = "MatchType")]
        public string MatchType { get; set; }
    }
}
