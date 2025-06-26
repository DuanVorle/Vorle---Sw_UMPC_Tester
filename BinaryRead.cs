using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascci2BinBootloaderInterface
{
    internal class BinaryRead
    {

        public BinaryRead() { } 

        public byte[] ReadBinaryFile(string filePath)
        {
            byte[] vetor;
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    vetor = reader.ReadBytes((int)reader.BaseStream.Length);
                }
            }
            catch {

                vetor = new byte[0];

            }


            return vetor;
        }

    }


}
