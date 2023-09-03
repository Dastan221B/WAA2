using System.Collections.Generic;

public interface IChoosebaleHeroForParticipantSlot
{
    void OpenChoosepanelForParticipant(ParticipantSlot participantSlot, IReadOnlyCollection<Hero> heroes);

}