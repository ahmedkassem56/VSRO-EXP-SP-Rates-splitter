using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace Rates_Splitter
{
    class FileWriter
    {
        public FileStream file_stream;
        public BinaryWriter m_writer;
        public FileWriter(string filePath)
        {
            file_stream = new FileStream(filePath, FileMode.Open);
            m_writer = new BinaryWriter(file_stream);
        }
        public void Write(long offset,byte[] buffer)
        {
            m_writer.BaseStream.Seek(offset,SeekOrigin.Begin);
            m_writer.Write(buffer,0,buffer.Count());
        }
        public void Write(long offset, byte[] buffer,int startIndex,int size)
        {
            m_writer.BaseStream.Seek(offset, SeekOrigin.Begin);
            m_writer.Write(buffer, startIndex, size);
        }
        public void Close()
        {
            file_stream.Close();
            m_writer.Close();
        }
    }
}
