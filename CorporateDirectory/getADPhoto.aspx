<%@ Page Language="C#" Debug="true" Codebehind="ippCorporateDirectory,ippCorporateDirectory.dll" Inherits="CorporateDirectory.getDirList"%>

<%
string lName= Request.QueryString["lName"];
%>
<img width="175" height="200" src="getADPhoto_Worker.aspx?lName=<%=lName%>" />
<p>
<hr>
