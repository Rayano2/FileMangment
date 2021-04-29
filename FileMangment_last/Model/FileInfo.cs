using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Model
{
    public class FileInfo
    {
        [Key]
        public int ID { get; set; }
        public string FilePath { get; set; }

        public string FileName { get; set; } 
    }
}
