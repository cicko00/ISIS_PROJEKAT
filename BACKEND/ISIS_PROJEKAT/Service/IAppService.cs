namespace ISIS_PROJEKAT.Service
{
    public interface IAppService
    {
        void ReciveData(IFormFile[] File);

        object GetResult(int NoOfDays);

        byte[] TrainWithData(DateTime startDate, DateTime endDate);
    }
}
