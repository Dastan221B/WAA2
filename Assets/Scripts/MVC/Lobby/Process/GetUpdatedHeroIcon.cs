public class GetUpdatedHeroIcon
{
    public SetPlayerHeroResult GetUpdateHeroIconFromMessage(MessageInput messageInput)
    {
        SetPlayerHeroResult setPlayerHeroInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<SetPlayerHeroResult>(messageInput.body);
        return setPlayerHeroInfo;
    }
}