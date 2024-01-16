using ISIS_PROJEKAT.Models;

namespace ISIS_PROJEKAT.Repository
{
    public interface IAppRepository
    {
        void SaveLoadDataToDatabase(List<LoadDataHistory> DataToSave);
        List<LoadDataHistory> GetAllLoadData();

        
        public LoadDataHistory? FindEntity(DateTime dateTime, string ptid);
        public void ClearLoadData();
        public void SaveResult();
        public void SaveWheatherForecast(List<WheatherForecast> wheatherForecastList);
        public List<WheatherForecast> GetWheatherForecast();

        public void ClearForcastData();

        public void DeleteDataPrediction();

        public void SaveLoadDataPredictions(List<LoadDataPrediction>l);



    }
}
