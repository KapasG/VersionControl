Using System;

namespace 2haromgyak.Entities
{
	public class User
	{
		public User()
		{
			public Guid ID { get; set; } = Guid.NewGuid();
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string FullName
			{
				get
				{
				return FirstName + " " + LastName;
				}
			}
	}
	}
}
