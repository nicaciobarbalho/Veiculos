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
            if (imagem != null && imagem.InputStream != null)
            {
                BinaryReader b = new BinaryReader(imagem.InputStream);
                byte[] binData = b.ReadBytes(imagem.ContentLength);
                return binData;
            }
            return null;
        }
    }

    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            if(fileBytes != null)
                this.InputStream = new MemoryStream(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }
    }

}