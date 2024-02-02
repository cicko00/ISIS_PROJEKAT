namespace ISIS_PROJEKAT.Service
{
    public interface IAppService
    {
        void ReciveData(IFormFile[] File);

        string GetResult(int NoOfDays, DateTime startdate);

        bool TrainWithData(DateTime startDate, DateTime endDate);
    }
}
