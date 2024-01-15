using ISIS_PROJEKAT.DB;
using ISIS_PROJEKAT.Models;

namespace ISIS_PROJEKAT.Repository
{
    public class AppRepository: IAppRepository
    {
        AppDbContext _context;
       public AppRepository(AppDbContext contex)
       {
            _context = contex;
       }

        public List<LoadDataHistory> GetAllLoadData()
        {
            return _context.LoadDatasHistory.ToList();
        }

        public void SaveLoadDataToDatabase(List<LoadDataHistory> DataToSave)
        {
            
            _context.LoadDatasHistory.AddRange(DataToSave.OrderBy(x=>x.DateTime));
            _context.SaveChanges();
        }

        public void ClearLoadData()
        {
            _context.LoadDatasHistory.RemoveRange(_context.LoadDatasHistory);
            _context.SaveChanges();
        }

        public LoadDataHistory? FindEntity(DateTime dateTime, string ptid)
        {
           return _context.LoadDatasHistory.FirstOrDefault(x => (x.PTID == ptid && x.DateTime == dateTime) );
        }
        public void SaveResult()
        {
            _context.SaveChanges();
        }
    }
}
