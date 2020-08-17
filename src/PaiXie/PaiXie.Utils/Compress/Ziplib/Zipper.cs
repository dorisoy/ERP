using System;
using System.IO;
using System.Collections;
using PaiXie.Utils.ZipLib.Zip;
using PaiXie.Utils.ZipLib.Checksums;
using PaiXie.Utils.ZipLib.GZip;

namespace PaiXie.Utils
{
    public class Zipper : IDisposable
    {
        #region  Ù–‘
        private string m_FolderToZIP;
        private ArrayList m_FileNamesToZIP;
        private string m_FileNameZipped;

        private ZipOutputStream m_ZipStream = null;
        private Crc32 m_Crc;

        #region Begin for Public Properties
        /**/
        /// <summary>
        /// Folder Name to ZIP
        /// Like "C:Test"
        /// </summary>
        public string FolderToZIP
        {
            get { return m_FolderToZIP; }
            set { m_FolderToZIP = value; }
        }

        /**/
        /// <summary>
        /// File Name to ZIP
        /// Like "C:TestTest.txt"
        /// </summary>
        public ArrayList FileNamesToZIP
        {
            get { return m_FileNamesToZIP; }
            set { m_FileNamesToZIP = value; }
        }

        /**/
        /// <summary>
        /// Zipped File Name 
        /// Like "C:TestMyZipFile.ZIP"
        /// </summary>
        public string FileNameZipped
        {
            get { return m_FileNameZipped; }
            set { m_FileNameZipped = value; }
        }
        #endregion

        /**/
        /// <summary>
        /// The construct
        /// </summary>
        public Zipper()
        {
            this.m_FolderToZIP = "";
            this.m_FileNamesToZIP = new ArrayList();
            this.m_FileNameZipped = "";
        }
        #endregion

        #region ZipFolder
        /**/
        /// <summary>
        /// Zip one folder : single level
        /// Before doing this event, you must set the Folder and the ZIP file name you want
        /// </summary>
        public void ZipFolder()
        {
            if (this.m_FolderToZIP.Trim().Length == 0)
            {
                throw new Exception("You must setup the folder name you want to zip!");
            }

            if (Directory.Exists(this.m_FolderToZIP) == false)
            {
                throw new Exception("The folder you input does not exist! Please check it!");
            }

            if (this.m_FileNameZipped.Trim().Length == 0)
            {
                throw new Exception("You must setup the zipped file name!");
            }

            string[] fileNames = Directory.GetFiles(this.m_FolderToZIP.Trim());

            if (fileNames.Length == 0)
            {
                throw new Exception("Can not find any file in this folder(" + this.m_FolderToZIP + ")!");
            }

            // Create the Zip File
            this.CreateZipFile(this.m_FileNameZipped);

            // Zip all files
            foreach (string file in fileNames)
            {
                this.ZipSingleFile(file);
            }

            // Close the Zip File
            this.CloseZipFile();
        }
        #endregion

        #region ZipFiles
        /**/
        /// <summary>
        /// Zip files
        /// Before doing this event, you must set the Files name and the ZIP file name you want
        /// </summary>
        public void ZipFiles()
        {
            if (this.m_FileNamesToZIP.Count == 0)
            {
                throw new Exception("You must setup the files name you want to zip!");
            }

            foreach (object file in this.m_FileNamesToZIP)
            {
                if (File.Exists(((string)file).Trim()) == false)
                {
                    throw new Exception("The file(" + (string)file + ") you input does not exist! Please check it!");
                }
            }

            if (this.m_FileNameZipped.Trim().Length == 0)
            {
                throw new Exception("You must input the zipped file name!");
            }

            // Create the Zip File
            this.CreateZipFile(this.m_FileNameZipped);

            // Zip this File
            foreach (object file in this.m_FileNamesToZIP)
            {
                this.ZipSingleFile((string)file);
            }

            // Close the Zip File
            this.CloseZipFile();
        }
        #endregion

        #region CreateZipFile
        /**/
        /// <summary>
        /// Create Zip File by FileNameZipped
        /// </summary>
        /// <param name="fileNameZipped">zipped file name like "C:TestMyZipFile.ZIP"</param>
        private void CreateZipFile(string fileNameZipped)
        {
            this.m_Crc = new Crc32();
            this.m_ZipStream = new ZipOutputStream(File.Create(fileNameZipped));
            this.m_ZipStream.SetLevel(6); // 0 - store only to 9 - means best compression
        }
        #endregion

        #region CloseZipFile
        /**/
        /// <summary>
        /// Close the Zip file
        /// </summary>
        private void CloseZipFile()
        {
            this.m_ZipStream.Finish();
            this.m_ZipStream.Close();
            this.m_ZipStream = null;
        }
        #endregion

        #region ZipSingleFile
        /**/
        /// <summary>
        /// Zip single file 
        /// </summary>
        /// <param name="fileName">file name like "C:TestTest.txt"</param>
        private void ZipSingleFile(string fileNameToZip)
        {
            // Open and read this file
            FileStream fso = File.OpenRead(fileNameToZip);

            // Read this file to Buffer
            byte[] buffer = new byte[fso.Length];
            fso.Read(buffer, 0, buffer.Length);

            // Create a new ZipEntry
            ZipEntry zipEntry = new ZipEntry(fileNameToZip.Split('\\')[fileNameToZip.Split('\\').Length - 1]);
            zipEntry.DateTime = DateTime.Now;
            // set Size and the crc, because the information
            // about the size and crc should be stored in the header
            // if it is not set it is automatically written in the footer.
            // (in this case size == crc == -1 in the header)
            // Some ZIP programs have problems with zip files that don't store
            // the size and crc in the header.
            zipEntry.Size = fso.Length;

            fso.Close();
            fso = null;

            // Using CRC to format the buffer
            this.m_Crc.Reset();
            this.m_Crc.Update(buffer);
            zipEntry.Crc = this.m_Crc.Value;

            // Add this ZipEntry to the ZipStream
            this.m_ZipStream.PutNextEntry(zipEntry);
            this.m_ZipStream.Write(buffer, 0, buffer.Length);
        }
        #endregion

        #region IDisposable member

        /**/
        /// <summary>
        /// Release all objects
        /// </summary>
        public void Dispose()
        {
            if (this.m_ZipStream != null)
            {
                this.m_ZipStream.Close();
                this.m_ZipStream = null;
            }
        }

        #endregion
    }
}