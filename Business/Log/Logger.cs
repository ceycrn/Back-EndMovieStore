using Business.Abstract;
using Business.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CCS
{
    public class Logger: ILoggerService
    {
        private LogInfo _logInfo;

        public Logger(LogInfo logInfo) 
        {
            _logInfo =logInfo;
        } 
        public void DoLog<T>(T variable)
        {

             _logInfo.LogFolderPath = "Logs"; // "Logs" klasörünün adını kullanın
             _logInfo.LogFileName = "myapplog.json"; // Log dosyasının adı
             _logInfo.LogFilePath = Path.Combine(_logInfo.LogFolderPath, _logInfo.LogFileName);

            //method yaz bu arkadas de enum alsin bir de t obje gondersin 
            //enum da type alsiin (file mi yazacagim yoksa online bir db ye mi yacagim sekilden en az 2 tane deger alsin )
            //her yaptign is ayri bir method olacak 
            // bu enum degerinin kontrolu icin de configden cekeceksin 
         

            var jsonLog = JsonConvert.SerializeObject(variable);

            System.IO.File.AppendAllText(_logInfo.LogFilePath, jsonLog + Environment.NewLine);
        }

      

        //public void PreLog<T>(Type type,T variable)
        //{

        //}

        //public enum Type
        //{
        //    WriteFile=1,
        //    WriteDB=2
        //}

        //public void WriteFile()
        //{

        //}

        //public void WriteDatabase()
        //{

        //}

        //public void DoLog<T>(System.Type type, T variable)
        //{
        //    throw new NotImplementedException();
        //}

        //public void PreLog<T>(System.Type type, T variable)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
