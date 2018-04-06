using System;
using System.Xml;    
using System.Xml.Schema;

namespace ValidateCS
{
   class Class1
   {
      static void Main(string[] args)
      {
         // Create a cache of schemas, and add two schemas
         XmlSchemaCollection sc = new XmlSchemaCollection();
         sc.Add("urn:MyUri", "../../CompanySummaryNS.xsd");
         sc.Add("", "../../CompanySummary.xsd");

         // Create a validating reader object
         XmlTextReader tr = new XmlTextReader("../../CompanySummaryWithXSDAndNS.xml");
         XmlValidatingReader vr = new XmlValidatingReader(tr);

         // Specify the type of validation required
         vr.ValidationType = ValidationType.Schema;

         // Tell the validating reader to use the schema collection
         vr.Schemas.Add(sc);

         // Register a validation event handler method
         vr.ValidationEventHandler += 
            new ValidationEventHandler(MyHandler);

         // Read and validate the XML document
         try
         {
            while (vr.Read())
            {
               if (vr.NodeType == XmlNodeType.Element && 
                  vr.LocalName == "NumEmps")
               {
                  int num;
                  num = XmlConvert.ToInt32(vr.ReadElementString());
                  Console.WriteLine("Number of employees: " + num);
               }
            }
         }
         catch (XmlException ex)
         {
            Console.WriteLine("XMLException occurred: " + ex.Message);
         }
         finally
         {
            vr.Close();
         }
      }

      // Validation event handler method
      public static void MyHandler(object sender, ValidationEventArgs e) 
      {
         Console.WriteLine("Validation Error: " + e.Message);
      }
   }
}
