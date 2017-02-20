using System;
using System.Data;
using System.DirectoryServices;

namespace myDirSearcher
{
	public class RonPage : System.Web.UI.Page
	{

		//
		//
		//  Define variables used in this class
		string _cn, _sn, _givenName, _title, _telephoneNumber, _otherTelephone, _streetAddress, _email, _homePhone, _bbPIN;
		System.Data.DataTable _dtUserInfo;
		System.Data.DataSet _dsUserInfo;
		// DataAdapter _daUserInfo;
		System.Data.DataColumn _dc_cn, _dc_sn, _dc_givenName, _dc_title, _dc_telephoneNumber, _dc_otherTelephone, _dc_streetAddress, _dc_email, _dc_homePhone, _dc_bbPIN;
		System.Data.DataRow _drUserInfo;




		//
		//	
		//  This method will execute the process of searching the directory
		protected void m_searchDirectory() {
			DirectoryEntry entry = new DirectoryEntry("LDAP://OU=DC,DC=cov,DC=com");
			DirectorySearcher mySearcher = new DirectorySearcher(entry);


			//
			// Create a dataSet in which we can store dataTable
			//
			_dsUserInfo = new System.Data.DataSet("cbUserInfo");

			//
			// Create a dataTable in which we can store attributes from AD
			//
			_dtUserInfo = new System.Data.DataTable("cbUserInfo");


			//
			// Define datacolumns and stamp them with an appropriate datatype
			//
			_dc_cn = new System.Data.DataColumn("u_cn");
			_dc_sn = new System.Data.DataColumn("u_sn");
			_dc_givenName = new System.Data.DataColumn("u_givenName");
			_dc_title = new System.Data.DataColumn("u_title");
			_dc_telephoneNumber = new System.Data.DataColumn("u_telephoneNumber");
			_dc_otherTelephone = new System.Data.DataColumn("u_otherTelephone");
			_dc_streetAddress = new System.Data.DataColumn("u_streetAddress");
			_dc_email = new System.Data.DataColumn("u_email");
			_dc_homePhone = new System.Data.DataColumn("u_homePhone");
			_dc_bbPIN = new System.Data.DataColumn("u_bbPIN");



			_dc_cn.DataType = System.Type.GetType("System.String");
			_dc_sn.DataType = System.Type.GetType("System.String");
			_dc_givenName.DataType = System.Type.GetType("System.String");
			_dc_title.DataType = System.Type.GetType("System.String");
			_dc_telephoneNumber.DataType = System.Type.GetType("System.String");
			_dc_otherTelephone.DataType = System.Type.GetType("System.String");
			_dc_streetAddress.DataType = System.Type.GetType("System.String");
			_dc_email.DataType = System.Type.GetType("System.String");
			_dc_homePhone.DataType = System.Type.GetType("System.String");
			_dc_bbPIN.DataType = System.Type.GetType("System.String");



			//
			// Add the required columns to the datatable
			//
			_dtUserInfo.Columns.Add(_dc_cn);
			_dtUserInfo.Columns.Add(_dc_sn);
			_dtUserInfo.Columns.Add(_dc_givenName);
			_dtUserInfo.Columns.Add(_dc_title);
			_dtUserInfo.Columns.Add(_dc_telephoneNumber);
			_dtUserInfo.Columns.Add(_dc_otherTelephone);
			_dtUserInfo.Columns.Add(_dc_streetAddress);
			_dtUserInfo.Columns.Add(_dc_email);
			_dtUserInfo.Columns.Add(_dc_homePhone);
			_dtUserInfo.Columns.Add(_dc_bbPIN);

			//
			// Add the datatable to the dataset
			//
			_dsUserInfo.Tables.Add(_dtUserInfo);




			

			mySearcher.Filter = ("(anr= Jo)");
			foreach(SearchResult result in mySearcher.FindAll()) 
			{
			   System.DirectoryServices.PropertyCollection myProps = result.GetDirectoryEntry().Properties;


				//
				// Define a new datarow
				//
				_drUserInfo = _dtUserInfo.NewRow();


				//
				// set local variables to the contents of user fields in AD
				//
				if(result.GetDirectoryEntry().Properties["cn"].Value != null)
					_cn = result.GetDirectoryEntry().Properties["cn"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["sn"].Value != null)
					_sn = result.GetDirectoryEntry().Properties["sn"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["givenName"].Value != null)
					_givenName = result.GetDirectoryEntry().Properties["givenName"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["title"].Value != null)
					_title = result.GetDirectoryEntry().Properties["title"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["telephoneNumber"].Value != null)
					_telephoneNumber = result.GetDirectoryEntry().Properties["telephoneNumber"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["otherTelephone"].Value != null)
					_otherTelephone = result.GetDirectoryEntry().Properties["otherTelephone"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["streetAddress"].Value != null)
					_streetAddress = result.GetDirectoryEntry().Properties["streetAddress"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["mail"].Value != null)
					_email = result.GetDirectoryEntry().Properties["mail"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["homePhone"].Value != null)
					_homePhone = result.GetDirectoryEntry().Properties["homePhone"].Value.ToString();

				if(result.GetDirectoryEntry().Properties["extensionAttribute6"].Value != null)
					_bbPIN = result.GetDirectoryEntry().Properties["extensionAttribute6"].Value.ToString();


				//
				// Populate the dataTable with the contents of local variables
				//
				_drUserInfo["U_cn"] = _cn;
				_drUserInfo["U_sn"] = _sn;
				_drUserInfo["U_givenName"] = _givenName;
				_drUserInfo["u_title"] = _title;
				_drUserInfo["u_telephoneNumber"] = _telephoneNumber;
				_drUserInfo["u_otherTelephone"] = _otherTelephone;
				_drUserInfo["u_streetAddress"] = _streetAddress;
				_drUserInfo["u_email"] = _email;
				_drUserInfo["u_homePhone"] = _homePhone;
				_drUserInfo["u_bbPIN"] = _bbPIN;

				//
				// Add the new row to the underlying table
				//
				_dtUserInfo.Rows.Add(_drUserInfo);

				//
				// Output the values of the local variables to the page
				//
				Response.Output.Write( "Record Count is: " + _dtUserInfo.Rows.Count + "<br><br>");
				Response.Output.Write( _cn + "<br>");
				Response.Output.Write( _sn + "<br>");
				Response.Output.Write( _givenName + "<br>");
				Response.Output.Write( _title + "<br>");
				Response.Output.Write( _telephoneNumber + "<br>");
				Response.Output.Write( _otherTelephone + "<br>");
				Response.Output.Write( _streetAddress + "<br>");
				Response.Output.Write( "e-mail address: " + _email + "<br>");
				Response.Output.Write( "Home Phone: " + _homePhone + "<br>");
				Response.Output.Write( "Blackberry PIN: " + _bbPIN + "<br><br><br><br>");

			}
		}
		
	}

}