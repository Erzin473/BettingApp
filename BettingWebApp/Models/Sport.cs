using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace BettingWebApp.Models
{
    [XmlRoot(ElementName = "Sport")]
    public class Sport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [XmlElement(ElementName = "Event")]
        public List<Event> Event { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }
}
