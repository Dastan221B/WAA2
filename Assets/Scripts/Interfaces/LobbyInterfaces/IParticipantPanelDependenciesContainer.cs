public interface IParticipantPanelDependenciesContainer
{   
        IBroadCastChangedHeroIcon BroadCastChangedHeroIcon {get; }
        IBroadcastChangedPlayerCastleRequest BroadcastChangedPlayerCastleRequest { get; }
        IBroadcastChangeOrdinal BroadcastChangeOrdinal { get; }
        IBroadcastUpdatedFee BroadcastUpdatedFee {get; }
}