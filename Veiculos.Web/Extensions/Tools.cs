using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Veiculos.Web.Extensions
{
    public static class Tools
    {
        public static byte[] ImagemParaByte(this HttpPostedFileBase imagem)
        {
            if (imagem != null)
            {
                BinaryReader b = new BinaryReader(imagem.InputStream);
                byte[] binData = b.ReadBytes(imagem.ContentLength);
                return binData;
            }
            return null;
        }
    }
}