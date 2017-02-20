//
//
//
//
//
using System;
using System.IO;
using System.Data;
using System.DirectoryServices;

namespace CorporateDirectory
{
	public class getDirList : System.Web.UI.Page
	{

		//
		//
		//  Define variables used in this class
		string _cn, _sn, _givenName, _title, _telephoneNumber, _otherTelephone, _streetAddress, _email, _homePhone, _bbPIN, lName, fName, extN, ldpQ;
		Int32 _nReqNext, _nReqMax;



		//
		//
		//
		//
		// This method is responsible for handling the results to be returned to the phone
		protected string m_formulateCD(SearchResultCollection src_DEs, Int32 n_startingRecord, Int32 n_maxRecords) {
			Int32 i = 0;

			string str_xml = "";


			if(n_maxRecords <= 32) {
				foreach(SearchResult src_DEs_r in src_DEs) {


				   	System.DirectoryServices.PropertyCollection myProps = src_DEs_r.GetDirectoryEntry().Properties;
					//
					// set local variables to the contents of user fields in AD
					//
					if(src_DEs_r.GetDirectoryEntry().Properties["cn"].Value != null)
						_cn = src_DEs_r.GetDirectoryEntry().Properties["cn"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["sn"].Value != null)
						_sn = src_DEs_r.GetDirectoryEntry().Properties["sn"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["givenName"].Value != null)
						_givenName = src_DEs_r.GetDirectoryEntry().Properties["givenName"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["title"].Value != null)
						_title = src_DEs_r.GetDirectoryEntry().Properties["title"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["telephoneNumber"].Value != null)
						_telephoneNumber = src_DEs_r.GetDirectoryEntry().Properties["telephoneNumber"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["otherTelephone"].Value != null)
						_otherTelephone = src_DEs_r.GetDirectoryEntry().Properties["otherTelephone"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["streetAddress"].Value != null)
						_streetAddress = src_DEs_r.GetDirectoryEntry().Properties["streetAddress"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["mail"].Value != null)
						_email = src_DEs_r.GetDirectoryEntry().Properties["mail"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["homePhone"].Value != null)
						_homePhone = src_DEs_r.GetDirectoryEntry().Properties["homePhone"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["extensionAttribute6"].Value != null)
						_bbPIN = src_DEs_r.GetDirectoryEntry().Properties["extensionAttribute6"].Value.ToString();

					
					str_xml += "<DirectoryEntry>";
					str_xml += "<Name>";
					str_xml += _sn;
					str_xml += ", ";
					str_xml += _givenName;
					str_xml += "</Name>";
					str_xml += "<Telephone>";
					str_xml += _otherTelephone;
					str_xml += "</Telephone>";
					str_xml += "</DirectoryEntry>";

				}
				
			}
			else {

				if(n_startingRecord == 0) {
					i++;
				}
				else {
					i = n_startingRecord;
				}

				foreach(SearchResult src_DEs_r in src_DEs) {


				   	System.DirectoryServices.PropertyCollection myProps = src_DEs_r.GetDirectoryEntry().Properties;
					//
					// set local variables to the contents of user fields in AD
					//
					if(src_DEs_r.GetDirectoryEntry().Properties["cn"].Value != null)
						_cn = src_DEs_r.GetDirectoryEntry().Properties["cn"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["sn"].Value != null)
						_sn = src_DEs_r.GetDirectoryEntry().Properties["sn"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["givenName"].Value != null)
						_givenName = src_DEs_r.GetDirectoryEntry().Properties["givenName"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["title"].Value != null)
						_title = src_DEs_r.GetDirectoryEntry().Properties["title"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["telephoneNumber"].Value != null)
						_telephoneNumber = src_DEs_r.GetDirectoryEntry().Properties["telephoneNumber"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["otherTelephone"].Value != null)
						_otherTelephone = src_DEs_r.GetDirectoryEntry().Properties["otherTelephone"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["streetAddress"].Value != null)
						_streetAddress = src_DEs_r.GetDirectoryEntry().Properties["streetAddress"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["mail"].Value != null)
						_email = src_DEs_r.GetDirectoryEntry().Properties["mail"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["homePhone"].Value != null)
						_homePhone = src_DEs_r.GetDirectoryEntry().Properties["homePhone"].Value.ToString();

					if(src_DEs_r.GetDirectoryEntry().Properties["extensionAttribute6"].Value != null)
						_bbPIN = src_DEs_r.GetDirectoryEntry().Properties["extensionAttribute6"].Value.ToString();


					if( ((i <= n_maxRecords) && ((i % 32) != 0)) ) {
						str_xml += "<DirectoryEntry>";
						str_xml += "<Name>";
						str_xml += _sn;
						str_xml += ", ";
						str_xml += _givenName;
						str_xml += "</Name>";
						str_xml += "<Telephone>";
						str_xml += _otherTelephone;
						str_xml += "</Telephone>";
						str_xml += "</DirectoryEntry>";
					}

					i++;
				}

				Response.AppendHeader("Refresh", "0; http://cbivws06dc/IPPWS/CorporateDirectory/?qReqNext="+i);

			}

			return str_xml;
		}

		//
		//	
		//  This method will execute the process of searching the directory
		protected void m_searchDirectory() {
			DirectoryEntry entry = new DirectoryEntry("LDAP://DC=cov,DC=com");
			DirectorySearcher mySearcher = new DirectorySearcher(entry);


			Response.ContentType = "text/xml";


			//
			// Begin Framing the directory entries returned to the phone
			Response.Output.Write("<CiscoIPPhoneDirectory>");
			Response.Output.Write("<Title>Corpoate Directory</Title>");
			Response.Output.Write("<Prompt> </Prompt>");

			//
			// Get query param and build ldap query
			lName = Request.QueryString["lName"];
			fName = Request.QueryString["fName"];
			extN = Request.QueryString["extension"];
			_nReqNext = Convert.ToInt32(Request.QueryString["qReqNext"]);
			ldpQ = String.Format("(&(sn={0}*)(givenName={1}*)(otherTelephone={2}*))", lName, fName, extN);
			

			mySearcher.Filter = (ldpQ);
			SearchResultCollection qResults = mySearcher.FindAll();

			_nReqMax = qResults.Count;

			Response.Output.Write("<ReturnedDirectoryEntries>"+qResults.Count+"</ReturnedDirectoryEntries>");
			Response.Output.Write("<ReturnedNextStatus>"+_nReqNext+"</ReturnedNextStatus>");

				Response.Output.Write(m_formulateCD(qResults, _nReqNext, _nReqMax));

			Response.Output.Write("</CiscoIPPhoneDirectory>");
		}



		//
		//
		//
		//
		// Get a thumbnailphoto
		protected void m_getthumbnailPhoto() {
			DirectoryEntry entry = new DirectoryEntry("LDAP://DC=cov,DC=com");
			DirectorySearcher mySearcher = new DirectorySearcher(entry);


			//
			// Get query param and build ldap query
			lName = Request.QueryString["lName"];
			fName = Request.QueryString["fName"];
			extN = Request.QueryString["extension"];
			_nReqNext = Convert.ToInt32(Request.QueryString["qReqNext"]);

			ldpQ = String.Format("(&(samAccountName={0})(givenName={1}*)(otherTelephone={2}*))", lName, fName, extN);

			mySearcher.Filter = (ldpQ);
			SearchResultCollection qResults = mySearcher.FindAll();
			

			SearchResult myResult = qResults[0];


			MemoryStream mBinReader = new MemoryStream((Byte [])myResult.GetDirectoryEntry().Properties["thumbnailPhoto"].Value);

			Byte[] imageOfYou = myResult.GetDirectoryEntry().Properties["thumbnailPhoto"].Value as Byte[];

			Context.Response.ContentType = "image/jpeg";
			Context.Response.BinaryWrite(imageOfYou);
		}
		
	}

}