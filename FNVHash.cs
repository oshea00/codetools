using System;
using System.Text;

class FNVHash {
	ulong FNV_64_OFFSET = 0xCBF29CE484222325;
	ulong FNV_PRIME = 0x100000001B3;

	public long FNV1(string value)
	{
		ulong hash = FNV_64_OFFSET;
		foreach (byte b in Encoding.ASCII.GetBytes(value)) {
			hash = hash * FNV_PRIME;
			hash = hash ^ b;
		}
		return (long) hash;
	}
	
	public string ToString(long hash, int radix = 16) {
		return Convert.ToString(hash,radix).ToUpper();
	} 
}
