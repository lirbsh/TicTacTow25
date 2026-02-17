using Plugin.CloudFirestore;

namespace TicTacTow25.Interfaces
{
    internal interface IFbData
    {
        public string DisplayName { get; }
        public string UserId { get; }
        public string GetErrorMessage(string errMessage);
        public void CreateUserWithEmailAndPasswordAsync(string email, string password, string name, Action<System.Threading.Tasks.Task> OnComplete);
        public void SignInWithEmailAndPasswordAsync(string email, string password, Action<System.Threading.Tasks.Task> OnComplete);
        public string SetDocument(object obj, string collectonName, string id, Action<System.Threading.Tasks.Task> OnComplete);
        public void UpdateField(string collectonName, string id, string fieldName, object fieldValue, Action<System.Threading.Tasks.Task> OnComplete);
        public void UpdateFields(string collectonName, string id, Dictionary<string, object> dict, Action<System.Threading.Tasks.Task> OnComplete);
        public void DeleteDocument(string collectonName, string id, Action<System.Threading.Tasks.Task> OnComplete);
        public void GetDocumentsWhereEqualTo(string collectonName, string fName, object fValue, Action<IQuerySnapshot> OnComplete);
        public void GetDocumentsWhereLessThan(string collectonName, string fName, object fValue, Action<IQuerySnapshot> OnComplete);
        public IListenerRegistration AddSnapshotListener(string collectonName, Plugin.CloudFirestore.QuerySnapshotHandler OnChange);
        public IListenerRegistration AddSnapshotListener(string collectonName, string id, Plugin.CloudFirestore.DocumentSnapshotHandler OnChange);
        public void StartBatch();
        public void BatchUpdateField(string collectonName, string id, string fName, object fValue);
        public void BatchIncrementField(string collectonName, string id, string fName, long incrementBy);
        public void CommitBatch(Action<System.Threading.Tasks.Task> OnComplete);
    }
}
