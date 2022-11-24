using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetContactInfo();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            ContactInfo conInfo = new ContactInfo();
            conInfo.ContactId = 1;
            
            SetContactInfo(conInfo);
            using (ISession session = SessionFactory.OpenSession)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        if (btnAdd.Content.ToString() == "Add")
                        {
                            session.Save(conInfo);
                            transaction.Commit();
                            MessageBox.Show("New contact added successfully.");
                            GetContactInfo();
                        }
                        else
                        {
                            session.Update(conInfo); //update the new data 
                            transaction.Commit(); //commit the data 
                            MessageBox.Show("Contact updated successfully.");
                            GetContactInfo(); //display the updated record 
                        }
                        clearcontrol();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void GetContactInfo()

        {
            try
            {
                using (ISession session = SessionFactory.OpenSession)

                {
                    var contactDetails = session.CreateCriteria<ContactInfo>().List();
                    dataGridContact.ItemsSource = contactDetails;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }
        private void SetContactInfo(ContactInfo conactInfo)

        {

            conactInfo.FirstName = txtFirstName.Text;
            conactInfo.LastName = txtLastName.Text;
            conactInfo.Mobile = txtMobile.Text;
            conactInfo.Age =Convert.ToInt32(txtAge.Text);
            if (btnAdd.Content.ToString() == "Submit")
            {
                conactInfo.ContactId= Convert.ToInt32(txtContactId.Text);
            }
        }
        private void bindContactInfo(ContactInfo conactInfo)

        {

            txtFirstName.Text = conactInfo.FirstName;
            txtLastName.Text=conactInfo.LastName;
            txtMobile.Text = conactInfo.Mobile;
            txtAge.Text = Convert.ToString(conactInfo.Age);
            txtContactId.Text = Convert.ToString(conactInfo.ContactId);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Are you sure you want to delete?", "Please confirm.", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            {
                using (ISession session = SessionFactory.OpenSession)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            var selectedContact = dataGridContact.SelectedItem as ContactInfo;
                            //ContactInfo contact = session.Get<ContactInfo>(selectedContact.ContactId);
                            session.Delete(selectedContact); //delete the record 
                            transaction.Commit(); //commit it 
                            MessageBox.Show("Contact deleted successfully.");
                            GetContactInfo(); //display the new collection 
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedContact = dataGridContact.SelectedItem;
            bindContactInfo(selectedContact as ContactInfo);
            btnAdd.Content = "Submit";
        }
        private void clearcontrol()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAge.Text = "0";
            txtContactId.Text = string.Empty;
            btnAdd.Content = "Add";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            clearcontrol();
        }

    }
}
