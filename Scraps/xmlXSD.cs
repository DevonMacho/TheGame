using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.IO;

public class xmlXSD : MonoBehaviour
{
		
		// Use this for initialization

		XmlSchemaSet genSchema (string xmlName)
		{
				XmlReader reader = XmlReader.Create (@xmlName);
				XmlSchemaSet schemaSet = new XmlSchemaSet ();
				XmlSchemaInference schema = new XmlSchemaInference ();
				schemaSet = schema.InferSchema (reader);
				return schemaSet;
		}


		bool checkSchema (string baseXml, string xmlName)
		{
			XmlSchemaSet schema = genSchema (baseXml);
			//XmlDocument doc = XmlDocument.Load(xmlName);
			//XDocument doc = XDocument.Load(@xmlName);
			
		try{
			doc.Validate(schema, new ValidationEventHandler());
			return true;
		}
		catch
		{
			return false;
		}
			
		}

		void Start ()
		{
		Debug.Log( checkSchema ("yourxml.xml","yourxml.xml"));
		}

		void Update ()
		{
	
		}
}
