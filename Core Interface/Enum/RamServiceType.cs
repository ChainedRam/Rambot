namespace KLDev.Rambot.Core.Enum
{
    public enum RamServiceType
    {
        /// <summary>
        /// Able to locate available clips.
        /// </summary>
        ClipCollection = 1,

        /// <summary>
        /// Able to ply clips 
        /// </summary>
        ClipPlayer = 2,

        /// <summary>
        /// Able to connect and disconnect bot client. 
        /// </summary>
       Connection = 4,

       /// <summary>
       /// Able to listen to server and channel events  
       /// </summary>
       EventListener = 8,

       /// <summary>
       /// Able to get and store data to a database 
       /// </summary>
       DataSource = 16,
       
       /// <summary>
       /// Able to convert text to sound 
       /// </summary>
       TextToSpeech = 32,
        Identity = 64,
        Logger = 128,
    }; 

}
