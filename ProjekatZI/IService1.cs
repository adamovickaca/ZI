using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProjekatZI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        [OperationContract]
        byte[] CryptRC6(string source, string duzinaK, string kljuc);

        [OperationContract]
        byte[] DecryptRC6(byte[] source);

        [OperationContract]
        string KodirajJavni(int[] priv, int n, int m);

        [OperationContract]
        string EncryptKS(int[] javni, int[] vrNiz);

        [OperationContract]
        string DecryptKS(int[] result, int TC);

        [OperationContract]
        string CryptBifid(string source, string key);
        //string CryptBifid(string source);

        [OperationContract]
        string DecryptBifid(string source, string key);
        //string DecryptBifid(string source);

        [OperationContract]
        byte[] EncryptCTR(byte[] source);

        [OperationContract]
        byte[] DecryptCTR(byte[] source);

        [OperationContract]
        byte[] TH(string fInfo1);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ProjekatZI.ContractType".
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
