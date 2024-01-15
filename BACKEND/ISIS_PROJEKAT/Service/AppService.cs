using ISIS_PROJEKAT.Models;
using ISIS_PROJEKAT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Runtime.Serialization;

namespace ISIS_PROJEKAT.Service
{
    public class AppService : IAppService
    {
        IAppRepository _repository;
        public AppService(IAppRepository appRepository) 
        { 
            _repository = appRepository;
        } 
        public object GetResult(int NoOfDays)
        {
            throw new NotImplementedException();
        }

        public void ReciveData(IFormFile[] Files)
        {
            List<LoadDataHistory> loadData = new List<LoadDataHistory>();
            loadData = _repository.GetAllLoadData();
            List<string[]> DataSetsList = new List<string[]>();
            CultureInfo culture = CultureInfo.InvariantCulture;


            foreach (var file in Files)
            {

                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                using (var parser = new TextFieldParser(reader))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    if (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (!fields.Contains("\"PTID\"") && !fields.Contains("PTID"))
                        {
                            break;
                        }

                    }
                    while (!parser.EndOfData)
                    {
                        try
                        {
                            string[] fields = parser.ReadFields();
                            DataSetsList.Add(fields);

                        }
                        catch (MalformedLineException ex)
                        {
                            throw new Exception("Wrong files selected");
                        }
                    }
                }
            }
                    for(int i = 0; i < DataSetsList.Count; i++)
                    {
                        string[] fields = DataSetsList[i];

                        if (!(fields[0].Split(":")[1] == "00") || (fields[2] != "N.Y.C."))
                        {
                            continue;
                        }
                        LoadDataHistory newInstance = new LoadDataHistory();
                        
                        List<string> datetimeSplits = fields[0].Split("/").ToList();
                        string datetime = datetimeSplits[1]+"/" + datetimeSplits[0] + "/" + datetimeSplits[2];
                        

                        newInstance.DateTime = Convert.ToDateTime(datetime);
                        newInstance.TimeZone = fields[1];
                        newInstance.District = fields[2];
                        newInstance.PTID = fields[3];
                if (newInstance.DateTime.DayOfWeek == DayOfWeek.Friday || newInstance.DateTime.DayOfWeek == DayOfWeek.Saturday || newInstance.DateTime.DayOfWeek == DayOfWeek.Sunday)
                    newInstance.isWeekend = true;
                        double.TryParse(fields[4], NumberStyles.Any, culture, out var value);
                        
                        if(value == null)
                        {
                            
                            for(int j=1;j<50 ;j++)
                            {
                                if(DataSetsList.Count > (i + j) )
                                {
                                    if (double.TryParse(DataSetsList[i + j][4], NumberStyles.Any, culture, out var valueRes))
                                    {
                                        value = valueRes;
                                        break;
                                    }
                                    
                                }
                                else if((i-j) > 0)
                                {
                                    if(double.TryParse(DataSetsList[i - j][4], NumberStyles.Any, culture, out var valueRes))
                                    {
                                        value = valueRes;
                                        break;
                                    }
                                }
                                else { value = 0; }
                            }
                        }
                        newInstance.Load = value;
                        
                        LoadDataHistory? existingData = loadData.FirstOrDefault(x=>(x.DateTime == newInstance.DateTime && x.PTID == newInstance.PTID));
                        if(existingData == null)
                        {
                            existingData = loadData.FirstOrDefault(x => (x.DateTime.Date == newInstance.DateTime.Date && x.DateTime.Hour == newInstance.DateTime.Hour&& x.PTID == null));
                            if(existingData == null )
                            {
                                loadData.Add(newInstance);
                            }   
                        }
                        else
                        {
                            existingData = newInstance;
                        }
                    }
                
            

            DataSetsList = new List<string[]>();

            foreach (var file in Files)
            {
                
                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                using (var parser = new TextFieldParser(reader))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    if (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (!fields.Contains("\"temp\"") && !fields.Contains("temp"))
                        {
                            break;
                        }

                    }
                    while (!parser.EndOfData)
                    {
                        try
                        {
                            string[] fields = parser.ReadFields();
                            DataSetsList.Add(fields);

                        }
                        catch (MalformedLineException ex)
                        {
                            throw new Exception("Wrong files selected");
                        }
                    }


                }
            }

            for (int i = 0; i < DataSetsList.Count; i++)
            {
                string[] fields = DataSetsList[i];
                LoadDataHistory newInstance = new LoadDataHistory();
                DateTime dateTime =DateTime.Parse(fields[1]);

                double.TryParse(fields[2], NumberStyles.Any, culture, out var tempres);
                if (tempres == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][2], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][2], NumberStyles.Any, culture, out var tempRes2))
                            {
                                tempres = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }





                    }
                }

                if (tempres == null)
                {
                    for (int j = 24; j < 400; j=j+24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][2], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][2], NumberStyles.Any, culture, out var tempRes2))
                            {
                                tempres = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if(tempres == null)
                {
                    tempres = 0;
                }

                ///////////////////////////////////////////////////////////////
                double.TryParse(fields[3], NumberStyles.Any, culture, out var feelslike);
                if (feelslike == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][3], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][3], NumberStyles.Any, culture, out var tempRes2))
                            {
                                feelslike = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (feelslike == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][3], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][3], NumberStyles.Any, culture, out var tempRes2))
                            {
                                feelslike = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if (feelslike == null)
                {
                    tempres = 0;
                }
                //////////////////////////////////////////////////
                double.TryParse(fields[4], NumberStyles.Any, culture, out var dew);
                if (dew == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][4], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][4], NumberStyles.Any, culture, out var tempRes2))
                            {
                                dew = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (dew == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][4], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][4], NumberStyles.Any, culture, out var tempRes2))
                            {
                                dew = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if (dew == null)
                {
                    dew = 0;
                }

                //////////////////////////////////////////////
                double.TryParse(fields[5], NumberStyles.Any, culture, out var humidity);
                if (humidity == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][5], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][5], NumberStyles.Any, culture, out var tempRes2))
                            {
                                humidity = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (humidity == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][5], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][5], NumberStyles.Any, culture, out var tempRes2))
                            {
                                humidity = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if (humidity == null)
                {
                    humidity = 0;
                }
                ///////////////////////////////////////////
                double.TryParse(fields[6], NumberStyles.Any, culture, out var precip);
                if (precip == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][6], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][6], NumberStyles.Any, culture, out var tempRes2))
                            {
                                precip = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (precip == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][6], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][6], NumberStyles.Any, culture, out var tempRes2))
                            {
                                precip = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if (precip == null)
                {
                    precip = 0;
                }
                //////////////////////////////////////////
                double.TryParse(fields[9], NumberStyles.Any, culture, out var snow);
                if (snow == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][9], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][9], NumberStyles.Any, culture, out var tempRes2))
                            {
                                snow = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (snow == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][9], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][9], NumberStyles.Any, culture, out var tempRes2))
                            {
                                snow = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if (snow == null)
                {
                    snow = 0;
                }
                //////////////////////////////////////
                double.TryParse(fields[10], NumberStyles.Any, culture, out var snowdepth);
                if (snowdepth == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][10], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][10], NumberStyles.Any, culture, out var tempRes2))
                            {
                                snowdepth = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (snowdepth == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][10], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][10], NumberStyles.Any, culture, out var tempRes2))
                            {
                                snowdepth = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if (snowdepth == null)
                {
                    snowdepth = 0;
                }
                //////////////////////////////
                double.TryParse(fields[11], NumberStyles.Any, culture, out var windgust);
                if (windgust == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][11], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][11], NumberStyles.Any, culture, out var tempRes2))
                            {
                                windgust = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (windgust == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][11], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][11], NumberStyles.Any, culture, out var tempRes2))
                            {
                                windgust = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }
                if (windgust == null)
                {
                    tempres = 0;
                }
                ///////////////////////////
                double.TryParse(fields[12], NumberStyles.Any, culture, out var windspeed);
                if (windspeed == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][12], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][12], NumberStyles.Any, culture, out var tempRes2))
                            {
                                windspeed = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (windspeed == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][12], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][12], NumberStyles.Any, culture, out var tempRes2))
                            {
                                windspeed = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                /////////////////////////
                double.TryParse(fields[13], NumberStyles.Any, culture, out var winddir);
                if (winddir == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][13], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][13], NumberStyles.Any, culture, out var tempRes2))
                            {
                                winddir = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (winddir == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][13], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][13], NumberStyles.Any, culture, out var tempRes2))
                            {
                                winddir = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                ///////////////////////
                double.TryParse(fields[14], NumberStyles.Any, culture, out var sealevelpressure);
                if (sealevelpressure == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][14], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][14], NumberStyles.Any, culture, out var tempRes2))
                            {
                                sealevelpressure = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (sealevelpressure == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][14], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][14], NumberStyles.Any, culture, out var tempRes2))
                            {
                                sealevelpressure = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                ///////////////////////////////////////
                double.TryParse(fields[15], NumberStyles.Any, culture, out var cloudcover);
                if (cloudcover == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][15], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][15], NumberStyles.Any, culture, out var tempRes2))
                            {
                                cloudcover = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (cloudcover == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][15], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][15], NumberStyles.Any, culture, out var tempRes2))
                            {
                                cloudcover = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                ///////////////////////////////////
                double.TryParse(fields[16], NumberStyles.Any, culture, out var visibility);
                if (visibility == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][16], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][16], NumberStyles.Any, culture, out var tempRes2))
                            {
                                visibility = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (visibility == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][16], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][16], NumberStyles.Any, culture, out var tempRes2))
                            {
                                visibility = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                //////////////////////////////////
                double.TryParse(fields[19], NumberStyles.Any, culture, out var uvindex);
                if (uvindex == null)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][19], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][19], NumberStyles.Any, culture, out var tempRes2))
                            {
                                uvindex = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                if (uvindex == null)
                {
                    for (int j = 24; j < 400; j = j + 24)
                    {
                        if (DataSetsList.Count > (i + j) && (i - j) > 0)
                        {
                            if (double.TryParse(DataSetsList[i + j][19], NumberStyles.Any, culture, out var tempRes1) && double.TryParse(DataSetsList[i - j][19], NumberStyles.Any, culture, out var tempRes2))
                            {
                                uvindex = (tempRes1 + tempRes2) / 2;
                                break;
                            }

                        }
                    }
                }

                /////////////////////////////////
                string conditions = fields[21].Replace("\"","");


                LoadDataHistory ld = new LoadDataHistory();
                ld.DateTime = dateTime;
                ld.Temperature = tempres;
                ld.FeelsLike = feelslike;
                ld.Dew = dew;
                ld.Humidity = humidity;
                ld.Precip = precip;
                ld.Snow = snow;
                ld.SnowDepth = snowdepth;
                ld.WindGust = windgust;
                ld.WindSpeed = windspeed;
                ld.WindDir = winddir;
                ld.SeaLevelPressure = sealevelpressure;
                ld.CloudCover = cloudcover;
                ld.Visibilty = visibility;
                ld.UVIndex = uvindex;
                ld.Conditions = conditions;


                int cnt =0;
                foreach (LoadDataHistory loaddata in loadData.Where(x => (x.DateTime.Day == dateTime.Day && x.DateTime.Hour == dateTime.Hour && x.DateTime.Month == dateTime.Month && x.DateTime.Year == dateTime.Year)))
                {
                    cnt++;
                    loaddata.Temperature = tempres;
                    loaddata.FeelsLike = feelslike;
                    loaddata.Dew = dew;
                    loaddata.Humidity= humidity;
                    loaddata.Precip = precip;
                    loaddata.Snow= snow;
                    loaddata.SnowDepth = snowdepth;
                    loaddata.WindGust= windgust;
                    loaddata.WindSpeed= windspeed;
                    loaddata.WindDir = winddir;
                    loaddata.SeaLevelPressure= sealevelpressure;
                    loaddata.CloudCover= cloudcover;
                    loaddata.Visibilty = visibility;
                    loaddata.UVIndex= uvindex;
                    loaddata.Conditions= conditions;
                }
                //if (cnt < 66)
                //{
                //    for(int ii=0; ii<(66 - cnt); ii++)
                //    {
                //        LoadDataHistory instance = new LoadDataHistory();
                //        Guid tmp = instance.Id;
                //        instance = ld;
                //        instance.Id = tmp;
                //        loadData.Add(instance);
                //    }
                //}
                
            }

                _repository.ClearLoadData();
            _repository.SaveLoadDataToDatabase(loadData);
           
        }




       
    }
}
