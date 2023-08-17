using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace BettingWebApp.Models
{
    [XmlRoot(ElementName = "Event")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [XmlElement(ElementName = "Match")]
        public List<Match> Match { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "IsLive")]
        public string IsLive { get; set; }
        [XmlAttribute(AttributeName = "CategoryID")]
        public string CategoryID { get; set; }
    }
}
