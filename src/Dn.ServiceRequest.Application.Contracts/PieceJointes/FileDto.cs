using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dn.ServiceRequest.PieceJointes
{
    public class FileDto
    {
    public string FileName { get; set; }
    public string Type { get; set; }
    public long Size { get; set; }
    public string Base64 { get; set; }
    }
}