using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenFMB.Adapters.Core.Models.ICCP
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(TASE2));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (TASE2)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "Tase2DataAttribute")]
	public class Tase2DataAttribute
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2Version")]
	public class Tase2Version
	{

		[XmlElement(ElementName = "Tase2DataAttribute")]
		public List<Tase2DataAttribute> Tase2DataAttribute { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public DateTime Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2Features")]
	public class Tase2Features
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2BilateralTableID")]
	public class Tase2BilateralTableID
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2ControlCommand")]
	public class Tase2ControlCommand
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2SBO")]
	public class Tase2SBO
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2DiscreteSetPoint")]
	public class Tase2DiscreteSetPoint
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2RealSetPoint")]
	public class Tase2RealSetPoint
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public double Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2LogicalDevice")]
	public class Tase2LogicalDevice
	{

		[XmlElement(ElementName = "Tase2BilateralTableID")]
		public Tase2BilateralTableID Tase2BilateralTableID { get; set; }

		[XmlElement(ElementName = "Tase2ControlCommand")]
		public List<Tase2ControlCommand> Tase2ControlCommand { get; set; }

		[XmlElement(ElementName = "Tase2SBO")]
		public List<Tase2SBO> Tase2SBO { get; set; }

		[XmlElement(ElementName = "Tase2DiscreteSetPoint")]
		public List<Tase2DiscreteSetPoint> Tase2DiscreteSetPoint { get; set; }

		[XmlElement(ElementName = "Tase2RealSetPoint")]
		public List<Tase2RealSetPoint> Tase2RealSetPoint { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "Tase2Discrete")]
		public Tase2Discrete Tase2Discrete { get; set; }

		[XmlElement(ElementName = "Tase2DiscreteQ")]
		public Tase2DiscreteQ Tase2DiscreteQ { get; set; }

		[XmlElement(ElementName = "Tase2DiscreteQTimeTag")]
		public Tase2DiscreteQTimeTag Tase2DiscreteQTimeTag { get; set; }

		[XmlElement(ElementName = "Tase2DiscreteQTimeTagExtended")]
		public Tase2DiscreteQTimeTagExtended Tase2DiscreteQTimeTagExtended { get; set; }

		[XmlElement(ElementName = "Tase2DiscreteExtended")]
		public Tase2DiscreteExtended Tase2DiscreteExtended { get; set; }

		[XmlElement(ElementName = "Tase2Real")]
		public Tase2Real Tase2Real { get; set; }

		[XmlElement(ElementName = "Tase2RealQ")]
		public Tase2RealQ Tase2RealQ { get; set; }

		[XmlElement(ElementName = "Tase2RealQTimeTag")]
		public Tase2RealQTimeTag Tase2RealQTimeTag { get; set; }

		[XmlElement(ElementName = "Tase2RealQTimeTagExtended")]
		public Tase2RealQTimeTagExtended Tase2RealQTimeTagExtended { get; set; }

		[XmlElement(ElementName = "Tase2RealExtended")]
		public Tase2RealExtended Tase2RealExtended { get; set; }

		[XmlElement(ElementName = "Tase2State")]
		public Tase2State Tase2State { get; set; }

		[XmlElement(ElementName = "Tase2StateQ")]
		public Tase2StateQ Tase2StateQ { get; set; }

		[XmlElement(ElementName = "Tase2StateQTimeTag")]
		public Tase2StateQTimeTag Tase2StateQTimeTag { get; set; }

		[XmlElement(ElementName = "Tase2StateQTimeTagExtended")]
		public Tase2StateQTimeTagExtended Tase2StateQTimeTagExtended { get; set; }

		[XmlElement(ElementName = "Tase2StateExtended")]
		public Tase2StateExtended Tase2StateExtended { get; set; }
	}

	[XmlRoot(ElementName = "Tase2Discrete")]
	public class Tase2Discrete
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2Flags")]
	public class Tase2Flags
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2DiscreteQ")]
	public class Tase2DiscreteQ
	{

		[XmlElement(ElementName = "Tase2Discrete")]
		public Tase2Discrete Tase2Discrete { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2TimeStamp")]
	public class Tase2TimeStamp
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }

		[XmlElement(ElementName = "Tase2GMTBasedS")]
		public Tase2GMTBasedS Tase2GMTBasedS { get; set; }

		[XmlElement(ElementName = "Tase2TimeIntervalL16")]
		public Tase2TimeIntervalL16 Tase2TimeIntervalL16 { get; set; }
	}

	[XmlRoot(ElementName = "Tase2DiscreteQTimeTag")]
	public class Tase2DiscreteQTimeTag
	{

		[XmlElement(ElementName = "Tase2Discrete")]
		public Tase2Discrete Tase2Discrete { get; set; }

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2GMTBasedS")]
	public class Tase2GMTBasedS
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2TimeIntervalL16")]
	public class Tase2TimeIntervalL16
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2DiscreteQTimeTagExtended")]
	public class Tase2DiscreteQTimeTagExtended
	{

		[XmlElement(ElementName = "Tase2Discrete")]
		public Tase2Discrete Tase2Discrete { get; set; }

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2COVCounter")]
	public class Tase2COVCounter
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2DiscreteExtended")]
	public class Tase2DiscreteExtended
	{

		[XmlElement(ElementName = "Tase2Discrete")]
		public Tase2Discrete Tase2Discrete { get; set; }

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlElement(ElementName = "Tase2COVCounter")]
		public Tase2COVCounter Tase2COVCounter { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2Real")]
	public class Tase2Real
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public double Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2RealQ")]
	public class Tase2RealQ
	{

		[XmlElement(ElementName = "Tase2Real")]
		public Tase2Real Tase2Real { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public double Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2RealQTimeTag")]
	public class Tase2RealQTimeTag
	{

		[XmlElement(ElementName = "Tase2Real")]
		public Tase2Real Tase2Real { get; set; }

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public double Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2RealQTimeTagExtended")]
	public class Tase2RealQTimeTagExtended
	{

		[XmlElement(ElementName = "Tase2Real")]
		public Tase2Real Tase2Real { get; set; }

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public double Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2RealExtended")]
	public class Tase2RealExtended
	{

		[XmlElement(ElementName = "Tase2Real")]
		public Tase2Real Tase2Real { get; set; }

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2Flags")]
		public Tase2Flags Tase2Flags { get; set; }

		[XmlElement(ElementName = "Tase2COVCounter")]
		public Tase2COVCounter Tase2COVCounter { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public double Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2State")]
	public class Tase2State
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2StateQ")]
	public class Tase2StateQ
	{

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2StateQTimeTag")]
	public class Tase2StateQTimeTag
	{

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2State")]
		public Tase2State Tase2State { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2StateQTimeTagExtended")]
	public class Tase2StateQTimeTagExtended
	{

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2State")]
		public Tase2State Tase2State { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Tase2StateExtended")]
	public class Tase2StateExtended
	{

		[XmlElement(ElementName = "Tase2TimeStamp")]
		public Tase2TimeStamp Tase2TimeStamp { get; set; }

		[XmlElement(ElementName = "Tase2State")]
		public Tase2State Tase2State { get; set; }

		[XmlElement(ElementName = "Tase2COVCounter")]
		public Tase2COVCounter Tase2COVCounter { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TASE2")]
	public class TASE2
	{

		[XmlElement(ElementName = "Tase2Version")]
		public Tase2Version Tase2Version { get; set; }

		[XmlElement(ElementName = "Tase2Features")]
		public Tase2Features Tase2Features { get; set; }

		[XmlElement(ElementName = "Tase2LogicalDevice")]
		public List<Tase2LogicalDevice> Tase2LogicalDevice { get; set; }

		[XmlAttribute(AttributeName = "xsi")]
		public string Xsi { get; set; }

		[XmlAttribute(AttributeName = "xsd")]
		public string Xsd { get; set; }

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}


}
