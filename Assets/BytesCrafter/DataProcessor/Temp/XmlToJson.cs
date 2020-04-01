
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BytesCrafter.DataProcessor;
/* 
public enum XtJAction
{
	Actions, 
	AdminLoadXml, AdminSaveJson, AdminTryLoadJson,
	PersonnelLoadXml, PersonnelSaveJson, PersonnelTryLoadJson,
	ProductLoadXml, ProductSaveJson, ProductTryLoadJson,
	CustomerLoadXml, CustomerSaveJson, CustomerTryLoadJson,
	OperationLoadXml, OperationSaveJson, OperationTryLoadJson,
	SessionLoadXml, SessionSaveJson, SessionTryLoadJson,
	AttendanceLoadXml, AttendanceSaveJson, AttendanceTryLoadJson
}

public class XmlToJson : MonoBehaviour
{
	public XtJAction actions = new XtJAction();

	[Header("DEBUGGER INFORMATION")]
	public StoreInfo storeInfo = new StoreInfo ();
	public PersonnelList personnels = new PersonnelList ();
	public ProductList products = new ProductList ();
	public CustomerList customers = new CustomerList ();

	public OperationList operations = new OperationList ();
	public SessionList sessions = new SessionList ();
	public AttendanceList attendances = new AttendanceList ();

	void OnValidate()
	{
		if(actions == XtJAction.AdminLoadXml)
		{
			storeInfo = FileSQL.XML.Select<StoreInfo> ("Admin@MobilePOS");
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.AdminSaveJson)
		{
			bool success = FileSQL.XML.Insert<StoreInfo> ("Admin@MobilePOS", storeInfo);
			Debug.Log ("Result: " + success);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.AdminTryLoadJson)
		{
			storeInfo = FileSQL.XML.Select<StoreInfo> ("Admin@MobilePOS");
			actions = XtJAction.Actions;
		}



		if(actions == XtJAction.PersonnelLoadXml)
		{
			personnels = FileSQL.XML.Select<PersonnelList> ("Personnels@" + storeInfo.username);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.PersonnelSaveJson)
		{
			bool success = FileSQL.XML.Insert<PersonnelList> ("Personnel@" + storeInfo.username, personnels);
			Debug.Log ("Result: " + success);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.PersonnelTryLoadJson)
		{
			personnels = FileSQL.XML.Select<PersonnelList> ("Personnel@" + storeInfo.username);
			actions = XtJAction.Actions;
		}



		if(actions == XtJAction.CustomerLoadXml)
		{
			customers = FileSQL.XML.Select<CustomerList> ("Customer@" + storeInfo.username);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.CustomerSaveJson)
		{
			bool success = FileSQL.XML.Insert<CustomerList> ("Customer@" + storeInfo.username, customers);
			Debug.Log ("Result: " + success);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.CustomerTryLoadJson)
		{
			customers = FileSQL.XML.Select<CustomerList> ("Customer@" + storeInfo.username);
			actions = XtJAction.Actions;
		}




		if(actions == XtJAction.ProductLoadXml)
		{
			products = FileSQL.XML.Select<ProductList> ("Product@" + storeInfo.username);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.ProductSaveJson)
		{
			bool success = FileSQL.XML.Insert<ProductList> ("Product@" + storeInfo.username, products);
			Debug.Log ("Result: " + success);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.ProductTryLoadJson)
		{
			products = FileSQL.XML.Select<ProductList> ("Product@" + storeInfo.username);
			actions = XtJAction.Actions;
		}



		if(actions == XtJAction.OperationLoadXml)
		{
			operations = FileSQL.XML.Select<OperationList> ("Operation@" + storeInfo.username);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.OperationSaveJson)
		{
			bool success = FileSQL.XML.Insert<OperationList> ("Operation@" + storeInfo.username, operations);
			Debug.Log ("Result: " + success);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.OperationTryLoadJson)
		{
			operations = FileSQL.XML.Select<OperationList> ("Operation@" + storeInfo.username);
			actions = XtJAction.Actions;
		}



		if(actions == XtJAction.SessionLoadXml)
		{
			sessions = FileSQL.XML.Select<SessionList> ("Attendance@" + storeInfo.username);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.SessionSaveJson)
		{
			bool success = FileSQL.XML.Insert<SessionList> ("Session@" + storeInfo.username, sessions);
			Debug.Log ("Result: " + success);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.SessionTryLoadJson)
		{
			sessions = FileSQL.XML.Select<SessionList> ("Session@" + storeInfo.username);
			actions = XtJAction.Actions;
		}



		if(actions == XtJAction.AttendanceLoadXml)
		{
			attendances = FileSQL.XML.Select<AttendanceList> ("Attendance@" + storeInfo.username);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.AttendanceSaveJson)
		{
			bool success = FileSQL.XML.Insert<AttendanceList> ("Attendance@" + storeInfo.username, attendances);
			Debug.Log ("Result: " + success);
			actions = XtJAction.Actions;
		}

		if(actions == XtJAction.AttendanceTryLoadJson)
		{
			attendances = FileSQL.XML.Select<AttendanceList> ("Attendance@" + storeInfo.username);
			actions = XtJAction.Actions;
		}
	}
}
*/