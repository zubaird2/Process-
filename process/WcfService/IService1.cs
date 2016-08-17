using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BLL;
using Entity;
using System.Drawing;
namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(string value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here


        [OperationContract]
        bool InsertToDB(ref ProcessObject pD);

        [OperationContract]
        void StopApplication(ref ProcessObject p);


        [OperationContract]
        List<ProcessObject> GetAllLogs();

        [OperationContract]
        void SaveScreenShot(Bitmap scrnShot, ImgObject imgObj);


        [OperationContract]
        List<ProcessInfo> GetLogsWithPercentageCal(DateTime dt1, DateTime dt2,int UId);

        [OperationContract]
        List<ImgObject> getImages(int Uid);

        [OperationContract]
        List<ProcessObject> getAllProecss();

        [OperationContract]
        List<ProcessObject> getDataByDate(DateTime dt1,DateTime dt2);

        [OperationContract]
        List<ProcessObject> getDataById(int id);

        //[OperationContract]
        //List<ImgObject> getAllImages();

        [OperationContract]
        List<ImgObject> GetScreenShotsByDate_Id(DateTime dt1, DateTime dt2,int uid);

        [OperationContract]
        List<ApplicationObject> GetApplication();

        [OperationContract]
        bool UpdateApplicationData(List<string> s);
        [OperationContract]
        List<KeyValuePair<string, double>> GetDataForChart(DateTime dt1, DateTime dt2, int uid);

        [OperationContract]
        bool Login(string u, string p);

        [OperationContract]
        bool UpdatePassword(string pp, string np);

        [OperationContract]
        bool InsertData(List<string> lst,Bitmap b,string img);

       

        }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }



    }
}
