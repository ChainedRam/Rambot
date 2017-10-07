namespace KLDev.Rambot.Interface
{
    public interface IClipCollectionService : IRamService
    {
        string[] GetAvailableClips();

        bool HasClip(string clipName);

        string GetClipPath(string clipName); 
    }
}
