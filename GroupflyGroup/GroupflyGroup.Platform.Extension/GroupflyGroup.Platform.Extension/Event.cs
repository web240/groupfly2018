namespace GroupflyGroup.Platform.Extension
{
	public class Event
	{
		private object object_0;

		public object Parameter => object_0;

		public Event(object parameter)
		{
			object_0 = parameter;
		}
	}
}
