using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CCS
{
    public class LogInfo: IEntity
    {
        public string LogFolderPath { get; set; }
        public string LogFileName { get; set; }
        public string LogFilePath { get; set; }
    }
}
