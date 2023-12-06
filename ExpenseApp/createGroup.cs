using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class createGroup : Form
    {
        private string username = FirebaseData.Instance.Username;
        
        public createGroup()
        {
            InitializeComponent();
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void Create()
        {
            string groupName = groupTB.Text.ToString();
            int numberOfParticipants = int.Parse(participantsTB.Text.Trim());
            string groupCode = generateCode();
            var db = otherFunc.FirestoreConn();

            CollectionReference groupsCollectionRef = db.Collection("Groups");
            DocumentReference groupDocRef = groupsCollectionRef.Document(groupCode);
            List<string> initialMembers = new List<string> { username };
            Dictionary<string, object> groupData = new Dictionary<string, object>()
            {
                {"GroupName", groupName},
                {"MaxParticipants", numberOfParticipants},
                {"GroupCode", groupCode},
                {"Members", initialMembers},
            };
            await groupDocRef.SetAsync(groupData);

            CollectionReference userCollectionRef = db.Collection("Users");
            DocumentReference userDocRef = userCollectionRef.Document(username);

            DocumentSnapshot userDocSnap = await userDocRef.GetSnapshotAsync();
            if (!userDocSnap.Exists)
            {
                await userDocRef.SetAsync(new Dictionary<string, object>
            {
                {"Groups", new List<string> { groupCode }}
            });
            }
            else {
                await userDocRef.UpdateAsync("Groups", FieldValue.ArrayUnion(groupCode));
            }
             Home h = new Home();
            h.checkGroupExists();
        }
        public string generateCode()
        {
            Random random = new Random();
            int length = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            StringBuilder codeBuilder = new StringBuilder();

            for (int i = 0; i < length; i++) {
                int index = random.Next(chars.Length);
                codeBuilder.Append(chars[index]);
            }
            return codeBuilder.ToString();
        }

        bool flag = true;
        private void btnSwitchPanel_Click(object sender, EventArgs e)
        {
            if (flag) {
                Console.WriteLine("Hi");
                joinPanel.Visible = true;
                joinPanel.BringToFront();
                flag = false;
                btnSwitchPanel.Text = "Create Group";
            }
            else
            {
                Console.WriteLine("Hello");
                createPanel.Visible = true;
                createPanel.BringToFront();
                flag = true;
                btnSwitchPanel.Text = "Join with code";
            }
        }
        public async void Join()
        {
            string groupCode = groupCodeTB.Text.ToString();
            var db = otherFunc.FirestoreConn();
            try{
                DocumentReference groupDocRef = db.Collection("Groups").Document(groupCode);
                DocumentSnapshot groupDocSnap = await groupDocRef.GetSnapshotAsync();
                if (groupDocSnap.Exists){
                    string groupName = groupDocSnap.GetValue<string>("GroupName");
                    List<string> groupMembers = groupDocSnap.GetValue<List<string>>("Members");
                    int maxParticipants = groupDocSnap.GetValue<int>("MaxParticipants");
                    if (groupMembers.Count < maxParticipants){
                        if (!groupMembers.Contains(username)){
                            await groupDocRef.UpdateAsync("Members", FieldValue.ArrayUnion(username));
                            CollectionReference userCollectionRef = db.Collection("Users");
                            DocumentReference userDocRef = userCollectionRef.Document(username);

                            DocumentSnapshot userDocSnap = await userDocRef.GetSnapshotAsync();
                            if (!userDocSnap.Exists){
                                await userDocRef.SetAsync(new Dictionary<string, object>
                                {
                                    {"Groups", new List<string> { groupCode }}
                                });
                                MessageBox.Show("Successfully joined group!", "Success", MessageBoxButtons.OK);
                            }
                            else{
                                await userDocRef.UpdateAsync("Groups", FieldValue.ArrayUnion(groupCode));
                                MessageBox.Show("Successfully joined group!", "Success", MessageBoxButtons.OK);
                            }
                        }
                        else{
                            MessageBox.Show($"You are already a member of group: {groupName}");
                        } 
                    }
                    else{
                        MessageBox.Show("Already reached the maximum number of participants","Group is full",MessageBoxButtons.OK);
                    }
                }
                else {
                    MessageBox.Show("Group not found. Please check the group code and try again.");
                }
            }
            catch (Exception ex){
                MessageBox.Show($"Error joining group: {ex.Message}");
            }
        }
        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (otherFunc.internetConn()){
                if (string.IsNullOrEmpty(groupCodeTB.Text)){
                    MessageBox.Show("Please enter a group code", "Invalid Action",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else{
                    Join();
                    this.Close();
                }
            }
            else MessageBox.Show("No Internet Connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            validateAndCreate();
        }
        private void participantsTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void validateAndCreate()
        {
            string groupName = groupTB.Text;

            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(participantsTB.Text)){
                MessageBox.Show("Something is missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (groupName.Length < 3){
                MessageBox.Show("Group Name must be at least 3 characters", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!int.TryParse(participantsTB.Text, out int maxParticipants)){
                MessageBox.Show("Invalid input for participants. Please enter a valid number.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (maxParticipants <= 0){
                MessageBox.Show("Minimum number of participants must be at least 1 user", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else{
                try{
                    Create();
                    MessageBox.Show("Group created successfully", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                catch{
                    MessageBox.Show("Error encountered during saving!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
