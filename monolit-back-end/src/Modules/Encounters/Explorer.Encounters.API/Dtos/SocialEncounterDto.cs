namespace Explorer.Encounters.API.Dtos
{
    public class SocialEncounterDto: EncounterDto
    {
        public int RequiredParticipants { get; set; }
        public List<ParticipantDto> CurrentlyInRange { get; set; }
        public List<SocialEncounterEventDto> Events { get; set; }
    }
}
