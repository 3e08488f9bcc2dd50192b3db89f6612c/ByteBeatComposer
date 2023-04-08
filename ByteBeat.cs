using System;
using System.IO;
using System.Media;

namespace ByteBeatinterpreter
{
	public class ByteBeat
	{
		/// Private-member variables.
		private bool isRunning;
		private readonly byte[] _data;
		private readonly MemoryStream stream;

		/// Private-member functions
		private void Load(int channel, int rate, int bps)
		{
			/// Credits to https://gist.github.com/dharmatech/6cb8fb83e46d56a1cc37f32e42559714
			this.isRunning = true;
			var writer = new BinaryWriter(stream);
			writer.Write("RIFF".ToCharArray());
			writer.Write(0);					// chunk size
			writer.Write("WAVE".ToCharArray());
			writer.Write("fmt ".ToCharArray());
			writer.Write(16);					// chunk size
			writer.Write((UInt16)1);				// audio format
			writer.Write((UInt16)channel);
			writer.Write(rate);
			writer.Write(rate * channel * bps / 8);			// byte rate
			writer.Write((UInt16)(channel * bps / 8));		// block align
			writer.Write((UInt16)bps);
			writer.Write("data".ToCharArray());
			writer.Write(_data.Length * channel * bps / 8);
			foreach (var elt in _data) writer.Write(elt);
			writer.Seek(4, SeekOrigin.Begin);			// seek to header chunk size field
			writer.Write((UInt32)(writer.BaseStream.Length - 8));	// chunk size
			stream.Seek(0, SeekOrigin.Begin);
			try
			{
                		new SoundPlayer(stream).PlaySync();
			}
			catch (Exception)
			{
				this.Stop(stream);
			}
		}

		private void Stop(MemoryStream stream)
		{
			if (stream != null && this.isRunning)
			{
				this.isRunning = false;
				stream.Dispose();
			}
		}

		/// Public-member functions
		public ByteBeat(byte[] data)
        	{
			this._data = data;
			this.stream = new MemoryStream();
		}

		public void CreateByteBeat(int channelID=1, int rate=8000, int bits_per_sample=8)
		{
			if(!this.isRunning) this.Load(channelID, rate, bits_per_sample);
		}

		public void Stop()
		{
			this.Stop(this.stream);
        	}
	}
}
