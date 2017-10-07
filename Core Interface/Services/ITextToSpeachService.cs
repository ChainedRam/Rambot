namespace KLDev.Rambot.Interface
{
    public interface ITextToSpeechService : IRamService
    {
        void Say(string text, string voice = "0");

        string[] GetVoices(); 
    }
}
