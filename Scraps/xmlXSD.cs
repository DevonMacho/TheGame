using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
public class xmlXSD : MonoBehaviour
{
		
	static bool error;
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
		XmlReaderSettings test = new XmlReaderSettings();
		test.Schemas.Add(genSchema(baseXml));
		test.ValidationType = ValidationType.Schema;
		test.ValidationEventHandler += new ValidationEventHandler(vHandle);

		try
		{
			XmlReader doc = XmlReader.Create(@xmlName,test);
			while (doc.Read()) { }
			return true;
		}
		catch 
		{
			return false;
		}
			
		}

		void Start ()
		{
		Debug.Log( checkSchema ("yourxml.xml","BaseGame.xml")); //have to create stricter rules
		}

	static void vHandle(object sender, ValidationEventArgs e)
	{
		if (e.Severity == XmlSeverityType.Warning)
		{
			//Debug.LogWarning("WARNING: " + e.Message);
			throw new System.Exception();
		}
		else if (e.Severity == XmlSeverityType.Error)
		{
			//Debug.LogError("ERROR: " + e.Message);
			throw new System.Exception();
		}
	}
}


