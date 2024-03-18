using Explorer.Encounters.API.Enums;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LocationDto Location { get; set; }
        public int Experience { get; set; }
        public EncounterStatus Status { get; set; }
        public int Radius { get; set; }
        public EncounterType Type { get; set; }
        public List<ParticipantDto> Participants { get; set; }
        public List<CompleterDto> Completers { get; set; }
    }
}
