using FluentResults;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalPersonService
    {
        Result RewardWithXp(List<string> usernames, int xp);
    }
}
