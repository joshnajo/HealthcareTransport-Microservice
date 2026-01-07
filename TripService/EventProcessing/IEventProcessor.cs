namespace TripService.EventProcessing
{
    public interface IEventProcessor
    {
        //we will get this from event listening service we get string 
        // deserialize it and see in this case Member-Publisher
        void ProcessEvent(string message);
    }

}